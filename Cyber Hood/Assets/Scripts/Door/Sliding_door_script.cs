using UnityEngine;

public class Sliding_door_script : MonoBehaviour
{

    public GameObject Trigger;
    public GameObject door;

    Animator doorAnim;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SlideDoor(bool state)
    {
        doorAnim.SetBool("slide", state);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            SlideDoor(true);
        }

    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            SlideDoor(false);
        }
    }

 
}
