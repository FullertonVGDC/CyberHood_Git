using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleActivation : MonoBehaviour
{
    public GameObject Particles;
    private bool particlesOn = false;

    public void Update()
    {
        if (particlesOn)
            Particles.gameObject.GetComponent<ParticleSystem>().Play();
    }

     void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Player")
        {
            particlesOn = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.name == "Player")
        {
            particlesOn = false;
        }
    }
}
