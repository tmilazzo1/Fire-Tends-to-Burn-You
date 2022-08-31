using System.Collections;
using System.Collections.Generic;
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

    float timeElapsed = 0;

    [Header("Unity Setup")]

    [SerializeField] string gameManagerName;
    [SerializeField] GameObject cam;

    [Header("Player Respawn")]

    [SerializeField] GameObject playerRespawnPrefab;
    [SerializeField] float respawnTime;
    Vector3 tempPlayerPosition;

    private void Awake()
    {
        gameObject.name = gameManagerName;
        if (GameObject.Find(gameManagerName) != gameObject) Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if(Input.GetKeyDown("p"))
        {
            Debug.Log("Pause the game");
            Debug.Log("timeElapsed: " + timeElapsed);
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }else
            {
                Time.timeScale = 0;
            }
        }
    }

    //called from Player
    public void playerDeath()
    {
        tempPlayerPosition = Player.Instance.transform.position;
        Destroy(Player.Instance.gameObject);
        StartCoroutine(playerRespawn());
    }

    IEnumerator playerRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        Instantiate(playerRespawnPrefab, getRespawnPoint(), Quaternion.identity, null);
    }

    Vector3 getRespawnPoint()
    {
        GameObject[] respawnPoints;
        respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach(GameObject respawnPoint in respawnPoints)
        {
            Vector3 diff = respawnPoint.transform.position - cam.transform.position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closest = respawnPoint;
                distance = curDistance;
            }
        }
        return closest.transform.position;
    }

    //called from CameraMovement
    public Vector3 getTempPlayerPosition()
    {
        return tempPlayerPosition;
    }
}
