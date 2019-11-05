using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    PlayerControl1 player;
    SpriteRenderer sprite;
    float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl1>();
        sprite = GetComponent<SpriteRenderer>();
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
    }
}
