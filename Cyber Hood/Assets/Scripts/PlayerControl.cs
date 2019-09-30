using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int playerSpeed = 5;
    public int camSlerpAmt = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) { transform.Translate(Vector3.right*playerSpeed*Time.deltaTime); }
        if (Input.GetKey(KeyCode.A)) { transform.Translate(Vector3.left * playerSpeed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.W)) { transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.S)) { transform.Translate(Vector3.back * playerSpeed * Time.deltaTime); }

        if (Input.GetKeyDown(KeyCode.E)) { transform.Rotate(0, 90, 0); }
        if (Input.GetKeyDown(KeyCode.Q)) { transform.Rotate(0, -90, 0); }

    }
}
