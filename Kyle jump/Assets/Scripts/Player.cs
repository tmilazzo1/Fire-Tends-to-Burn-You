using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Player : MonoBehaviour
{
    //Singleton instatiation
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
    }

    bool playerIsDead;
    ParticlesOnDeath particlesOnDeath;
    [SerializeField] GameObject trailParticles;
    [SerializeField] GameObject deathParticles;

    private void Awake()
    {
        gameObject.name = "MainPlayer";
        if (GameObject.Find("MainPlayer") != gameObject) Destroy(gameObject);
    }

    //called from PlayerCollisions
    public void die()
    {
        particlesOnDeath = GetComponent<ParticlesOnDeath>();
        if (playerIsDead) return;
        playerIsDead = true;
        particlesOnDeath.particlesOnDeath(trailParticles, 0);
        particlesOnDeath.particlesOnDeath(deathParticles, 30);
        GameManager.Instance.playerDeath();
    }
}
