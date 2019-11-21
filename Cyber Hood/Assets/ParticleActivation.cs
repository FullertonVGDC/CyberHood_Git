using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleActivation : MonoBehaviour
{
    public GameObject Particles;


     void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Player")
        {
            Particles.GetComponent<ParticleSystem>().enableEmission = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.name == "Player")
        {
            Particles.GetComponent<ParticleSystem>().enableEmission = false;
        }
    }
}
