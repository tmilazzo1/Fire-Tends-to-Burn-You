using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    //called from animation
    public void respawnPlayer()
    {
        Instantiate(playerPrefab, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }
}
