using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 camStartPos;
    private Vector3 camEndPos;

    private float camStartTime;
    private float camJourneyLength;

    public float camSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        camStartTime = Time.time;
        camStartPos = transform.localPosition;
        camEndPos = camStartPos + new Vector3(30,0,0);
        camJourneyLength = Vector3.Distance(camStartPos, camEndPos);
        Debug.Log("CamStart: " + camStartPos + " EndPos: " + camEndPos );
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - camStartTime) * camSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / camJourneyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.localPosition = Vector3.Lerp(camStartPos, camEndPos, fractionOfJourney);
        }
        
    }



}

