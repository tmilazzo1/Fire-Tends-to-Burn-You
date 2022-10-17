using UnityEngine;

public class FadeLight : MonoBehaviour
{
    public void startFade(Transform newParent)
    {
        GetComponent<Animator>().SetTrigger("fade");
        transform.parent = newParent;
    }

    public void endFade()
    {
        Destroy(gameObject);
    }
}
