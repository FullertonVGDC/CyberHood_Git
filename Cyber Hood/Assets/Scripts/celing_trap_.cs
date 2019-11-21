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
    public int blowBackForce = 800;


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
            // Calculate Angle Between the collision point and the player
            Vector3 dir = col.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            Player.GetComponent<PlayerControl>().BlowBack(dir, blowBackForce);
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
