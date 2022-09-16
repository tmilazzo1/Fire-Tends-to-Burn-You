using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] string spikeTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == spikeTag)
        {
            Player.Instance.die();
        }
    }
}
