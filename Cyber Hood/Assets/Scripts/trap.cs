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
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            onTrap = true;
            Player.GetComponent<PlayerControl>().takeDmg(initialDamage);
            StartCoroutine(waitDamage(waitTime, col));
        }
    }

    //When Player continues to stand on the block



    IEnumerator waitDamage(float waitTime, Collision col)
    {
        while (onTrap)
        {
            if (col.gameObject.name == "Player")
            {
                Player.GetComponent<PlayerControl>().takeDmg(continuousDamage);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

}
