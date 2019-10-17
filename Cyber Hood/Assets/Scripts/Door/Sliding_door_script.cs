using UnityEngine;

public class Sliding_door_script : MonoBehaviour
{

    public bool isLocked = false;
    public Light statusLight;

    Animator doorAnim;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isLocked) {
            statusLight.color = Color.red;
        }
        else
            statusLight.color = Color.green;
    }


    void SlideDoor(bool state)
    {
        doorAnim.SetBool("slide", state);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && !isLocked)
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

    public void UnlockDoor() {
        isLocked = false;
    }

    public void OpenCloseDoor() {
        SlideDoor(true);
    }
 
}
