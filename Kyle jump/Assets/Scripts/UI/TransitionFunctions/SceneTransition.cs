using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    AudioManager audioManager;
    Animator animator;
    int currentIndex;
    AudioClip songToLoad;

    private void Start()
    {
        audioManager = GameManager.Instance.GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        setSceneIndex();
    }

    public void loadScene(int newSceneIndex, AudioClip newSong)
    {
        if(newSong)
        {
            songToLoad = newSong;
        }else
        {
            songToLoad = null;
        }
        animator.SetTrigger("start");
        currentIndex = newSceneIndex;
        audioManager.StartCoroutine(audioManager.fadeSongOut(1));
    }

    public void animationFinished()
    {
        audioManager.StartCoroutine(audioManager.fadeSongIn(1, songToLoad));
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
