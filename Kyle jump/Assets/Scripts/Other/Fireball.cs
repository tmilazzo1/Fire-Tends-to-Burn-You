using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] GameObject fireTrailParticles;
    [SerializeField] GameObject fireDeathParticles;
    [SerializeField] GameObject fireBallLight;
    Transform parentOnDeath;
    ParticlesOnDeath particlesOnDeath;
    bool collided = false;

    private void Start()
    {
        Destroy(gameObject, 3f);
        parentOnDeath = transform.parent;
        particlesOnDeath = GetComponent<ParticlesOnDeath>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == gameObject.tag) return;
        if (collided) return;
        collided = true;

        if (Player.Instance)
        {
            if(collision.gameObject == Player.Instance.gameObject) Player.Instance.die();
        }

        particlesOnDeath.particlesOnDeath(fireTrailParticles, 0, parentOnDeath);
        particlesOnDeath.particlesOnDeath(fireDeathParticles, 5, parentOnDeath);
        fireBallLight.GetComponent<FadeLight>().startFade(parentOnDeath);
        GetComponent<AnimatorFunctions>().PlaySound(0);
        Destroy(gameObject);
    }
}
