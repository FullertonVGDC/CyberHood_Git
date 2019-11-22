using System.Collections;
using UnityEngine;


public class DisappearingFloors : MonoBehaviour
{
    bool isChanging = false;
    bool ActiveStatus = true;
    public float delayAmt = 0;
    public float timerAmt;

    private void Start()
    {
        isChanging = true;
        StartCoroutine(WaitForDelay());
    }

    void Update()
    {
        if (!isChanging) {
            isChanging = true;
            StartCoroutine(ChangeActive());
        }
    }


    IEnumerator ChangeActive() {
        ActiveStatus = !ActiveStatus;
        this.gameObject.GetComponent<MeshRenderer>().enabled = ActiveStatus;
        this.gameObject.GetComponent<BoxCollider>().enabled = ActiveStatus;

        yield return new WaitForSeconds(timerAmt);
        isChanging = false;
        yield return null;
    }

    IEnumerator WaitForDelay() {
        yield return new WaitForSeconds(delayAmt);
        isChanging = false;
    }

}
