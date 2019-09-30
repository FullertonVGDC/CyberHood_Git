using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int playerSpeed = 5;

    //Vars for Camera Rotation:
    bool changeCamAngle = false;
    float timeTakenDuringSlerp = 1f;
    Quaternion q_startPos;
    Quaternion q_endPos;
    Vector3 tempEuler;
    float timeStartedSlerp;

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

        //if (Input.GetKeyDown(KeyCode.E)) { transform.Rotate(0, 90, 0); }
        //if (Input.GetKeyDown(KeyCode.Q)) { transform.Rotate(0, -90, 0); }

        if (Input.GetKeyDown(KeyCode.E) && !changeCamAngle) { StartSlerp(90); }
        if (Input.GetKeyDown(KeyCode.Q) && !changeCamAngle) { StartSlerp(-90); }


    }

    private void FixedUpdate()
    {
        if (changeCamAngle) {
            float timeSinceStarted = Time.time - timeStartedSlerp;
            float percComplete = timeSinceStarted / timeTakenDuringSlerp;
            transform.rotation = Quaternion.Slerp(q_startPos, q_endPos, percComplete);
            if (percComplete >= 1.0f)
                changeCamAngle = false;
        }
    }


    void StartSlerp(int angleChange) {
        changeCamAngle = true;
        timeStartedSlerp = Time.time;
        q_startPos = transform.rotation;
        tempEuler = new Vector3(q_startPos.eulerAngles.x, q_startPos.eulerAngles.y+angleChange, q_startPos.eulerAngles.z);
        q_endPos = Quaternion.Euler(tempEuler);
    }
}
