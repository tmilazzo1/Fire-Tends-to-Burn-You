using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    Animator animator;
    int currentIndex;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        setSceneIndex();
    }

    public void loadScene(int newSceneIndex)
    {
        animator.SetTrigger("start");
        currentIndex = newSceneIndex;
    }

    public void animationFinished()
    {
        SceneManager.LoadScene(currentIndex);
        setSceneIndex();
    }

    void setSceneIndex()
    {
        if (currentIndex > 0)
        {
            GameManager.Instance.changeScene(true);
        }
        else
        {
            GameManager.Instance.changeScene(false);
        }
    }
}
