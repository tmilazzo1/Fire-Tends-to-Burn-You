using UnityEngine;

public class RandomStartAnim : MonoBehaviour
{
    private void OnEnable()
    {
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
