using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    bool collisionState = false;
    [SerializeField] LayerMask layerMask;

    public bool detectCollision()
    {
        return collisionState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((layerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            collisionState = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((layerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            collisionState = false;
        }
    }
}
