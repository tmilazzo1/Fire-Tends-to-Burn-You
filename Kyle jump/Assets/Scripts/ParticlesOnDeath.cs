using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnDeath : MonoBehaviour
{
    public void particlesOnDeath(GameObject particlesGameObject, int emitAmount)
    {
        ParticleSystem ps = particlesGameObject.GetComponent<ParticleSystem>();
        var main = ps.main;

        ps.Emit(emitAmount);
        particlesGameObject.transform.parent = null;
        main.stopAction = ParticleSystemStopAction.Destroy;
        ps.Stop();
    }
}
