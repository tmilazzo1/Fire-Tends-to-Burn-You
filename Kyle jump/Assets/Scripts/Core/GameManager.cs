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

    [Header("Unity Setup")]

    [SerializeField] string gameManagerName;
    EscapeFunctions escapeFunctions;
    public SceneTransition sceneTransition;
    GameObject cam;

    [Header("Player Respawn")]

    [SerializeField] GameObject playerRespawnPrefab;
    [SerializeField] float respawnTime;
    [HideInInspector] public Vector3 tempPlayerPosition;
    [HideInInspector] public LevelData currentLevelData;

    [Header("Player Stats")]

    [HideInInspector] public float timeElapsed;
    [HideInInspector] public int playerDeaths;

    private void Awake()
    {
        gameObject.name = gameManagerName;
        if (GameObject.Find(gameManagerName) != gameObject) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        escapeFunctions = GetComponent<EscapeFunctions>();
    }

    private void FixedUpdate()
    {
        if (inGame) timeElapsed += Time.deltaTime;
    }

    public void changeTimeScale(float newTime)
    {
        Time.timeScale = newTime;
    }

    //called from Player
    public void playerDeath()
    {
        playerDeaths++;
        tempPlayerPosition = Player.Instance.transform.position;
        Destroy(Player.Instance.gameObject);
        StartCoroutine(playerRespawn());
    }

    IEnumerator playerRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        if (!inGame)
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
        changeTimeScale(1);
        inGame = newInGame;
        escapeFunctions.resetPauseMenu(newInGame);
        if(newInGame)
        {
            timeElapsed = 0;
            playerDeaths = 0;
        }
    }
}
