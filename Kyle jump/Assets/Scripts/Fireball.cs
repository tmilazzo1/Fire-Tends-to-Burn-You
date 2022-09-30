using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] GameObject fireTrailParticles;
    [SerializeField] GameObject fireDeathParticles;
    Transform parent;
    ParticlesOnDeath particlesOnDeath;
    bool collided = false;

    private void Start()
    {
        Destroy(gameObject, 3f);
        parent = transform.parent;
        particlesOnDeath = GetComponent<ParticlesOnDeath>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Deadly") return;
        if (collided) return;
        collided = true;

        if (Player.Instance)
        {
            if(collision.gameObject == Player.Instance.gameObject) Player.Instance.die();
        }

        particlesOnDeath.particlesOnDeath(fireTrailParticles, 0, parent);
        particlesOnDeath.particlesOnDeath(fireDeathParticles, 5, parent);
        Destroy(gameObject);
    }
}
