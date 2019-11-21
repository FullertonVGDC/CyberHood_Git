using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private bool hasControl = true;
    public int playerHealth = 100;
    public int playerSpeed = 10;
    private int dataPoints = 0; 
    private bool hasGun = false;

    public int jumpForce = 300;
    private Rigidbody player_rb;
    public bool isJumping;
    

    private Animator p_animator;

    //Vars for Camera Rotation:
    bool changeCamAngle = false;
    float timeTakenDuringCamSlerp = 1f;
    Quaternion q_startPosCam;
    Quaternion q_endPosCam;
    float timeStartedSlerpCam;
    public float camSlerpSpeed = 2f;

    //Vars for Moving through doors:
    Vector3 v3_startDoor;
    Vector3 v3_endDoor;
    private bool triggerDoor;
    private bool movingThruDoor;
    public float thruDoorSpeed = 10f;
    float startTimeDoor;
    float journeyLengthDoor;

    // Start is called before the first frame update
    void Start()
    {
        p_animator = GetComponent<Animator>();
        playerSpeed = 10;
        player_rb = GetComponent<Rigidbody>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { takeDmg(5); }

        hasControl = (movingThruDoor) ? false : true; 

        //If you player has Control
        if (hasControl) {
            //Movement Controls:
            if (Input.GetKey(KeyCode.D) ) { transform.Translate(Vector3.right * playerSpeed * Time.deltaTime); } //p_animator.SetBool("isRunning", false); 
            if (Input.GetKey(KeyCode.A) ) { transform.Translate(Vector3.left * playerSpeed * Time.deltaTime); }
            //if (Input.GetKey(KeyCode.W) ) { transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime); }
            //if (Input.GetKey(KeyCode.S) ) { transform.Translate(Vector3.back * playerSpeed * Time.deltaTime); }
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping) { Jump();}//StartCoroutine(Jump()); }

            //Door Controls:
            if (Input.GetKeyDown(KeyCode.W) && !isJumping && !movingThruDoor) { triggerDoor = true; }
            if (Input.GetKeyUp(KeyCode.W)) { triggerDoor = false; }

            //Camera Controls:
            //if (Input.GetKeyDown(KeyCode.E) && !changeCamAngle) { StartSlerp(90); }
            //if (Input.GetKeyDown(KeyCode.Q) && !changeCamAngle) { StartSlerp(-90); }

            //Sword Attack:
            if (Input.GetKey(KeyCode.K) ) {
                ;
            }

            //Gun Attack:
            if (Input.GetKey(KeyCode.L) && hasGun) {
                ;
            }
        }
        


    }

    private void FixedUpdate()
    {
        if (changeCamAngle) {
            float timeSinceStarted = (Time.time - timeStartedSlerpCam) * camSlerpSpeed;
            float percComplete = timeSinceStarted / timeTakenDuringCamSlerp;
            transform.rotation = Quaternion.Slerp(q_startPosCam, q_endPosCam, percComplete);
            Debug.Log("timeSinceStart: " + timeSinceStarted + " %Completed: " + percComplete);
            if (percComplete >= 1.0f)
                changeCamAngle = false;
        }

        if (movingThruDoor)
        {
            float distCovered = (Time.time - startTimeDoor) * thruDoorSpeed;
            float amtCompleted = distCovered / journeyLengthDoor;
            Debug.Log("distCovered: " + distCovered + " amtCompleted: " + amtCompleted);
            this.transform.position = Vector3.Lerp(v3_startDoor, v3_endDoor, amtCompleted);
            if (amtCompleted >= 1.0f)
                movingThruDoor = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Collection of DataPoints
        if (other.gameObject.tag == "DataPoint") {
            dataPoints++;
            // Todo: Check DataPoints Upgrade System
            Destroy(other.gameObject);
        }
    }

    public void takeDmg(int amt) {
        playerHealth -= amt;

        //UI auto updates based on current Health
        GameObject tempObj;
        if ((tempObj = GameObject.FindGameObjectWithTag("HealthBar")) && !tempObj.GetComponent<HealthController>().takingDamage) {
            tempObj.GetComponent<HealthController>().takingDamage = true;
            StartCoroutine(tempObj.GetComponent<HealthController>().ShakeHealth()); //Shake UI Screen
        }

        if (playerHealth <= 0) { Dead(); } // Check if player has died
            
    }

    private void Dead() {
        Debug.Log("Ya Dun Died Fool");
        hasControl = false;
        //Todo: Play Death Animation
        //Todo: Reload to last Save
    }

    void StartSlerp(int angleChange) {
        changeCamAngle = true;
        timeStartedSlerpCam = Time.time;
        q_startPosCam = transform.rotation;
        Vector3 tempEuler = new Vector3(q_startPosCam.eulerAngles.x, q_startPosCam.eulerAngles.y+angleChange, q_startPosCam.eulerAngles.z);
        q_endPosCam = Quaternion.Euler(tempEuler);
    }


    private void OnTriggerStay(Collider other)
    {
        if (triggerDoor && other.gameObject.tag == "DoorEntry" && !other.gameObject.transform.parent.Find("Door").GetComponent<Sliding_door_script>().isLocked) {
            triggerDoor = false;
            StartSlerp(-90);
            movingThruDoor = true;
            startTimeDoor = Time.time;
            v3_startDoor = transform.position;
            Vector3 endPos = other.gameObject.transform.parent.Find("Door_Point_2").transform.position;
            v3_endDoor = new Vector3(endPos.x, transform.position.y, endPos.z);
            journeyLengthDoor = Vector3.Distance(v3_startDoor, v3_endDoor);            
            Debug.Log("start: " + v3_startDoor + " end: " + v3_endDoor + " distance: " + journeyLengthDoor);
        }

        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!movingThruDoor && other.gameObject.tag == "DoorEntry2" && !other.gameObject.transform.parent.Find("Door").GetComponent<Sliding_door_script>().isLocked)
        {
            other.gameObject.transform.parent.Find("Door").GetComponent<Sliding_door_script>().OpenCloseDoor();
            triggerDoor = false;
            StartSlerp(90);
            movingThruDoor = true;
            startTimeDoor = Time.time;
            v3_startDoor = transform.position;
            Vector3 endPos = other.gameObject.transform.parent.Find("Door_Point_1").transform.position;
            v3_endDoor = new Vector3(endPos.x, transform.position.y, endPos.z);
            journeyLengthDoor = Vector3.Distance(v3_startDoor, v3_endDoor);
            Debug.Log("start: " + v3_startDoor + " end: " + v3_endDoor + " distance: " + journeyLengthDoor);
        }

        if (other.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

    private System.Collections.IEnumerator Jumps() {
        isJumping = true;
        player_rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(1.2f);
        isJumping = false;

        yield return null;
    }

    private void Jump()
    {
        player_rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;

    }

    public void BlowBack(Vector3 direction, float blowBackForce)
    {
        if (Input.GetKey(KeyCode.D))
            player_rb.AddForce(Vector3.left * blowBackForce);
        else if (Input.GetKey(KeyCode.A))
            player_rb.AddForce(Vector3.right * blowBackForce);
    }

}
