using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungus : MonoBehaviour
{
    [SerializeField] Vector2 shootDir;
    [SerializeField] float speed;
    [SerializeField] Transform shootPosition;
    [SerializeField] GameObject fireballPrefab;

    public void shootBullet()
    {
        GameObject fireball = (GameObject)Instantiate(fireballPrefab, shootPosition.position, Quaternion.identity, null);
        fireball.GetComponent<Rigidbody2D>().velocity = shootDir * speed;
    }
}
