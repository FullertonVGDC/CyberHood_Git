using System.Collections;
using UnityEngine;


public class DisappearingFloors : MonoBehaviour
{
    bool isChanging = false;
    bool ActiveStatus = true;
    public int timerAmt;

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
        Debug.Log("HALP!");
        isChanging = false;
        yield return null;
    }

}
