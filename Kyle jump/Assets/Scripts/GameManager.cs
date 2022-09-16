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

    [Header("Unity Setup")]

    [SerializeField] string gameManagerName;
    [SerializeField] GameObject cam;

    [Header("Player Respawn")]

    [SerializeField] GameObject playerRespawnPrefab;
    [SerializeField] float respawnTime;
    Vector3 tempPlayerPosition;
    LevelData currentLevelData;

    [Header("Player Stats")]

    float timeElapsed;
    int playerDeaths;

    private void Awake()
    {
        gameObject.name = gameManagerName;
        if (GameObject.Find(gameManagerName) != gameObject) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
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
        Instantiate(playerRespawnPrefab, currentLevelData.respawnPoint.position, Quaternion.identity, null);
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

    //called from CameraMovement
    public Vector3 getTempPlayerPosition()
    {
        return tempPlayerPosition;
    }

    public void changeLevelData()
    {
        currentLevelData = getClosestLevelData().GetComponent<LevelData>();
    }

    public int getCurrentLevel()
    {
        return currentLevelData.levelNum;
    }

    public float getTimeElapsed()
    {
        return timeElapsed;
    }
}
