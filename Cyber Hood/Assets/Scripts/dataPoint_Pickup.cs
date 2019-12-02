using System.Collections;
using UnityEngine;

public class dataPoint_Pickup : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    AudioSource a_collectSound;

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
        a_collectSound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }

    public void playCollectSound() {
        a_collectSound.Play();
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(this.gameObject, 1f);
    }

}
