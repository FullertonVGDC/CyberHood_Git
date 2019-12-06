using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    
    enum GameState { Tutorial, Area_1, End }
    GameState currentState;
    GameObject gf_Player;

    // Start is called before the first frame update
    void Start()
    {
        gf_Player = GameObject.FindGameObjectWithTag("Player");
        currentState = GameState.Tutorial;
        gf_Player.GetComponent<PlayerControl1>().StopPlayerControl();
        StartCoroutine(f_GameStart());

    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator f_GameStart() {
        Debug.Log("Game Start");
        yield return StartCoroutine(f_GameTutorial());
        yield return StartCoroutine(f_MainGame());
        //End

        Debug.Log("END OF IT ALL");
    }

    IEnumerator f_GameTutorial()
    {
        Debug.Log("Tutorial Start");
        //repeat while dialog and tut still going

        yield return null;

    }

    IEnumerator f_MainGame()
    {
        Debug.Log("Game Starts");
        gf_Player.GetComponent<PlayerControl1>().GivePlayerControl();

        yield return new WaitForSeconds(2);

        yield return null;
    }

    //Created for crunch time
    public void givePlrControl() { gf_Player.GetComponent<PlayerControl1>().GivePlayerControl(); }
    public void stopPlrControl() { gf_Player.GetComponent<PlayerControl1>().StopPlayerControl(); }


}
