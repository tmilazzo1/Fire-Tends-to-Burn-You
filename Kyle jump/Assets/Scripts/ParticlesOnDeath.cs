using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnDeath : MonoBehaviour
{
    public void particlesOnDeath(GameObject particlesGameObject, int emitAmount, Transform newParent)
    {
        ParticleSystem ps = particlesGameObject.GetComponent<ParticleSystem>();
        var main = ps.main;

        ps.Emit(emitAmount);
        particlesGameObject.transform.parent = newParent;
        main.stopAction = ParticleSystemStopAction.Destroy;
        ps.Stop();
    }
}
