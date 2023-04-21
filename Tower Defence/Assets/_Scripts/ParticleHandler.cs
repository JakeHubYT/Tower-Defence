using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{

    public GameObject particlePrefab;

    public void PlayDeathParticle(Transform playPoint)
    {
        Instantiate(particlePrefab, playPoint.position, particlePrefab.transform.rotation, null);


    }
}
