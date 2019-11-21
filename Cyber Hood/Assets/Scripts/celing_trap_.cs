using UnityEngine;
using System.Collections;



public class celing_trap_ : MonoBehaviour
{
    public GameObject Player;

    bool isChanging = false;
    bool ActiveStatus = true;
    public float delayAmt = 0;
    public float timerAmt;
    public int damage = 5;

    private void Start()
    {
        isChanging = true;
        StartCoroutine(WaitForDelay());
    }

    void Update()
    {
        if (!isChanging)
        {
            isChanging = true;
            StartCoroutine(ChangeActive());
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            Player.GetComponent<PlayerControl>().takeDmg(damage);
            Player.GetComponent<Rigidbody>().AddForce(transform.forward * -100);
        }
    }

    IEnumerator ChangeActive()
    {
        ActiveStatus = !ActiveStatus;
        this.gameObject.GetComponent<MeshRenderer>().enabled = ActiveStatus;
        this.gameObject.GetComponent<BoxCollider>().enabled = ActiveStatus;

        yield return new WaitForSeconds(timerAmt);
        isChanging = false;
        yield return null;
    }

    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(delayAmt);
        isChanging = false;
    }


}
