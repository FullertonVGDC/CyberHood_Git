using UnityEngine;

public class PlayerControl1 : MonoBehaviour
{
    //Variables for Stats
    public int playerHealth = 100;
    public int playerSpeed;
    private int dataPoints = 0; 
    private bool hasGun = false;
    private Vector3 defaultScale;

    //Variables For Movement
    public int jumpForce = 5;    
    public Transform groundChecker;
    public float groundDistance;
    public LayerMask GroundLayer;
    public float gravityModifier = 1f;
    private bool _isGrounded = true;
    private bool _isRunning = false;
    private bool hasControl = true;
    private bool playerActive = true;
    private bool _isAttacking = false;
    private CharacterController player_Controller;
    private Vector3 p_Velocity = new Vector3(0,0,0);


    private Animator p_animator;
    AudioSource as_playerSounds;
    public AudioClip ac_swordSwing;
    public AudioClip ac_deathSound;

    //Vars for Camera Rotation:
    bool changeCamAngle = false;
    float timeTakenDuringCamSlerp = 1f;
    Quaternion q_startPosCam;
    Quaternion q_endPosCam;
    float timeStartedSlerpCam;
    public float camSlerpSpeed = 2f;
    

    //Vars for Moving through doors:
    public float thruDoorSpeed = 20f;
    Vector3 v3_startDoor;
    Vector3 v3_endDoor;
    private bool triggerDoor;

    [HideInInspector]
    public bool movingThruDoor;

    float startTimeDoor;
    float journeyLengthDoor;

    // Start is called before the first frame update
    void Start()
    {
        p_animator = GetComponent<Animator>();
        playerSpeed = 10;
        player_Controller = GetComponent<CharacterController>();
        defaultScale = transform.localScale;
        as_playerSounds = GetComponent<AudioSource>();
        as_playerSounds.clip = ac_swordSwing;
    }

    public void StartAttacking()
    {
        _isAttacking = true;
    }

    public void EndAttacking()
    {
        _isAttacking = false;
    }

    public bool IsAttacking()
    {
        return _isAttacking;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        if (playerActive) {
            //if (Input.GetKeyDown(KeyCode.P)) { takeDmg(5); }

            hasControl = (movingThruDoor) ? false : true;
            _isRunning = false;
            
            //If you player has Control
            if (hasControl)
            {
                _isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, GroundLayer, QueryTriggerInteraction.Ignore);
                if (_isGrounded)
                {
                    p_Velocity.y = 0;
                    p_animator.SetBool("isGrounded", true);

                }
                else // In air
                {
                    p_animator.SetBool("isRunning", false);
                    p_animator.SetBool("isGrounded", false);
                }

                //Movement Controls:            
                if (Input.GetKey(KeyCode.D))
                {
                    transform.localScale = defaultScale;
                    _isRunning = true;
                    player_Controller.Move(transform.right * Time.deltaTime * playerSpeed);
                }

                if (Input.GetKey(KeyCode.A))
                {
                    transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);
                    _isRunning = true;
                    player_Controller.Move(-transform.right * Time.deltaTime * playerSpeed);
                }

                if (_isRunning)
                    p_animator.SetBool("isRunning", true);
                else
                    p_animator.SetBool("isRunning", false);

                if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
                {
                    p_animator.SetBool("isGrounded", false);
                    p_Velocity.y += Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
                }

                //Door Controls:
                if (Input.GetKeyDown(KeyCode.W) && _isGrounded && !movingThruDoor) { triggerDoor = true; }
                if (Input.GetKeyUp(KeyCode.W)) { triggerDoor = false; }

                //Camera Controls:
                //if (Input.GetKeyDown(KeyCode.E) && !changeCamAngle) { StartSlerp(90); }
                //if (Input.GetKeyDown(KeyCode.Q) && !changeCamAngle) { StartSlerp(-90); }

                //Sword Attack:
                if (Input.GetKeyDown(KeyCode.K) && !_isAttacking)
                {
                    //_isAttacking = true;
                    p_animator.SetTrigger("meleeTrigger");
                    as_playerSounds.Play();
                }

                //Gun Attack:
                if (Input.GetKeyDown(KeyCode.L) && hasGun && !_isAttacking)
                {
                    
                }
            }

            //Always Apply Gravity        
            p_Velocity.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

            player_Controller.Move(p_Velocity * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (changeCamAngle) {
            float timeSinceStarted = (Time.time - timeStartedSlerpCam) * camSlerpSpeed;
            float percComplete = timeSinceStarted / timeTakenDuringCamSlerp;
            transform.rotation = Quaternion.Slerp(q_startPosCam, q_endPosCam, percComplete);
            if (percComplete >= 1.0f)
                changeCamAngle = false;
        }

        if (movingThruDoor)
        {
            float distCovered = (Time.time - startTimeDoor) * thruDoorSpeed;
            float amtCompleted = distCovered / journeyLengthDoor;
            this.transform.position = Vector3.Lerp(v3_startDoor, v3_endDoor, amtCompleted);
            if (amtCompleted >= 1.0f)
                movingThruDoor = false;
        }
    }

    public void GivePlayerControl() { playerActive = true; }
    public void StopPlayerControl() { playerActive = false; }

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

        if (playerHealth > 100)
            playerHealth = 100;

        //UI auto updates based on current Health
        GameObject tempObj;
        if ((tempObj = GameObject.FindGameObjectWithTag("HealthBar")) && !tempObj.GetComponent<HealthController>().takingDamage) {
            tempObj.GetComponent<HealthController>().takingDamage = true;
            if(amt > 0)
                StartCoroutine(tempObj.GetComponent<HealthController>().ShakeHealth()); //Shake UI Screen
        }

        if (playerHealth <= 0) { Dead(); } // Check if player has died
            
    }

    private void Dead() {
        Debug.Log("Ya Dun Died Fool");
        hasControl = false;
        //Todo: Play Death Animation
        //Todo: Reload to last Save
        as_playerSounds.clip = ac_deathSound;
        as_playerSounds.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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

        if (other.gameObject.tag == "Enemy")
        {
            if (_isAttacking)
            {
                other.gameObject.GetComponent<MeleeEnemy>().health--;
                _isAttacking = false;
            }
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
        // Collection of DataPoints
        if (other.gameObject.tag == "DataPoint")
        {
            
            dataPoints++;
            takeDmg(-25);
            other.gameObject.GetComponent<dataPoint_Pickup>().playCollectSound();
        }
    }
}
