using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton instatiation
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }

    bool inGame = false;
    [HideInInspector] public float timeElapsed;
    [HideInInspector] public int playerDeaths;

    [Header("Win Variables")]

    [SerializeField] int victorySceneIndex;
    [SerializeField] float slowTimeOut;
    [HideInInspector] public bool gameIsWon;

    [Header("Unity Setup")]

    [SerializeField] string gameManagerName;
    public SceneTransition sceneTransition;
    PauseFunction escapeFunctions;
    AnimatorFunctions animatorFunctions;
    AudioManager audioManager;
    GameObject cam;

    [Header("Player Respawn")]

    [SerializeField] GameObject playerRespawnPrefab;
    [SerializeField] float respawnTime;
    [HideInInspector] public Vector3 tempPlayerPosition;
    [HideInInspector] public LevelData currentLevelData;

    [Header("Audio")]

    [SerializeField] int victoryIndex;
    [SerializeField] AudioClip victorySong;

    private void Awake()
    {
        gameObject.name = gameManagerName;
        if (GameObject.Find(gameManagerName) != gameObject) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        escapeFunctions = GetComponent<PauseFunction>();
        animatorFunctions = GetComponent<AnimatorFunctions>();
        audioManager = GetComponent<AudioManager>();
    }

    private void FixedUpdate()
    {
        if (gameIsWon) return;
        if (inGame) timeElapsed += Time.deltaTime;
    }

    public void changeTimeScale(float newTime)
    {
        Time.timeScale = newTime;
    }

    //called from player
    public IEnumerator winGame()
    {
        float newTimeScale = 1;
        float timeCounter = 0;
        gameIsWon = true;
        animatorFunctions.PlaySound(victoryIndex);
        audioManager.StartCoroutine(audioManager.changeSong(slowTimeOut, victorySong));

        while (newTimeScale > 0)
        {
            timeCounter += Time.unscaledDeltaTime / slowTimeOut;
            newTimeScale = Mathf.Lerp(1, 0, timeCounter);
            changeTimeScale(newTimeScale);
            yield return new WaitForSeconds(0);
        }

        sceneTransition.loadScene(victorySceneIndex, null);
    }

    //called from Player
    public void playerDeath()
    {
        if (gameIsWon) return;
        playerDeaths++;
        tempPlayerPosition = Player.Instance.transform.position;
        Destroy(Player.Instance.gameObject);
        StartCoroutine(playerRespawn());
    }

    IEnumerator playerRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        if (inGame)
        {
            Instantiate(playerRespawnPrefab, currentLevelData.respawnPoint.position, Quaternion.identity, null);
        }
    }

    GameObject getClosestLevelData()
    {
        GameObject[] levelDatas;
        levelDatas = GameObject.FindGameObjectsWithTag("LevelData");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach(GameObject levelData in levelDatas)
        {
            Vector3 diff = levelData.transform.position - cam.transform.position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closest = levelData;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void changeLevelData()
    {
        if(currentLevelData) currentLevelData.changeActive(false);
        currentLevelData = getClosestLevelData().GetComponent<LevelData>();
        currentLevelData.changeActive(true);
    }

    public void setCamera(GameObject newCam)
    {
        cam = newCam;
    }

    public void changeScene(bool newInGame)
    {
        if (!inGame)
        {
            gameIsWon = false;
            timeElapsed = 0;
            playerDeaths = 0;
        }
        changeTimeScale(1);
        inGame = newInGame;
        escapeFunctions.resetPauseMenu(newInGame);
    }
}
