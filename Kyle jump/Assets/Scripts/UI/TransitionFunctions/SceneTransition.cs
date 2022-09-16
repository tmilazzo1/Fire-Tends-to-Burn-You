using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    //Singleton instatiation
    private static SceneTransition instance;
    public static SceneTransition Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<SceneTransition>();
            return instance;
        }
    }


    [SerializeField] string newName;
    string sceneName;
    Animator animator;

    private void Awake()
    {
        name = newName;
        if (GameObject.Find(newName) != gameObject) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void loadScene(string newSceneName)
    {
        animator.SetTrigger("start");
        sceneName = newSceneName;
    }

    public void animationFinished()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}
