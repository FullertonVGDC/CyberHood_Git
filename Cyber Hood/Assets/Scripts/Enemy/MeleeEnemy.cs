using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public float speed;

    Animator anim;
    Rigidbody body;
    SpriteRenderer sprite;

    PlayerControl1 player;
    float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();

        player = FindObjectOfType<PlayerControl1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localEulerAngles.y == 270 || transform.localEulerAngles.y == 90)
        {
            distanceToPlayer = player.transform.position.z - transform.position.z;
        }
        else if (transform.localEulerAngles.y == 0 || transform.localEulerAngles.y == 180)
        {
            distanceToPlayer = player.transform.position.x - transform.position.x;
        }

        sprite.flipX = distanceToPlayer < 0f;

        if (Vector3.Distance(player.transform.position, transform.position) <= 20f)
        {
            anim.SetFloat("Distance To Player", Mathf.Abs(distanceToPlayer));
            if (transform.localEulerAngles.y == 270 || transform.localEulerAngles.y == 90)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, player.transform.position.z), speed * Time.deltaTime);
            }
            else if (transform.localEulerAngles.y == 0 || transform.localEulerAngles.y == 180)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            }
        }
        else
            anim.SetFloat("Distance To Player", 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.takeDmg(10);
        }
    }
}
