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
        StartCoroutine(f_GameStart());

    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator f_GameStart() {
        Debug.Log("Game Start");
        yield return StartCoroutine(f_GameTutorial());
        //MainGame
        //End

        Debug.Log("END OF IT ALL");
        yield return null;
    }

    IEnumerator f_GameTutorial()
    {
        Debug.Log("Tutorial Start");
        //repeat while dialog and tut still going
        yield return new WaitForSeconds(2);

        yield return null;
    }



}
