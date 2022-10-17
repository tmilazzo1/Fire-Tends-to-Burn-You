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
    AnimatorFunctions animatorFunctions;

    [Header("String names")]

    [SerializeField] string spikeTag;
    [SerializeField] string playerName;

    [Header("Particles")]

    [SerializeField] GameObject trailParticles;
    [SerializeField] GameObject deathParticles;
    ParticlesOnDeath particlesOnDeath;

    private void Awake()
    {
        gameObject.name = playerName;
        if (GameObject.Find(playerName) != gameObject) Destroy(gameObject);
        particlesOnDeath = GetComponent<ParticlesOnDeath>();
        animatorFunctions = GetComponent<AnimatorFunctions>();
    }

    public void die()
    {
        if (playerIsDead) return;
        playerIsDead = true;

        animatorFunctions.PlaySound(3);
        particlesOnDeath.particlesOnDeath(trailParticles, 0, null);
        particlesOnDeath.particlesOnDeath(deathParticles, 30, null);
        GameManager.Instance.playerDeath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == spikeTag)
        {
            die();
        }
    }
}
