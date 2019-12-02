using UnityEngine;
using System.Collections;

public class trap : MonoBehaviour
{
    public GameObject Player;

    // Control Variables for how much damage is givin
    public int initialDamage = 5;
    public int continuousDamage = 2;

    //Control Var for how often damagw is taken
    public float waitTime = 2.0f;

    public bool onTrap = false;

    //When Player initially steps on trap, tick off 5 health
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            onTrap = true;
            Player.GetComponent<PlayerControl1>().takeDmg(initialDamage);
            StartCoroutine(waitDamage(waitTime, col));
        }
    }


    void OnTriggerExit(Collider col)
    {
       if(col.gameObject.tag == "Player")
        {
            onTrap = false;
        }
    }


    //When Player continues to stand on the block
    IEnumerator waitDamage(float waitTime, Collider col)
    {
        while (onTrap)
        {
            if (col.gameObject.tag == "Player")
            {
                Player.GetComponent<PlayerControl1>().takeDmg(continuousDamage);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

}
