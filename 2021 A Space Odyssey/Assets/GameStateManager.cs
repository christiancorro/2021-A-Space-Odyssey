using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    private Animator gameStates;

    void Start() {
        gameStates = GetComponent<Animator>();
    }

    public void ShowMap() {
        Debug.LogFormat("ShowMap");
        gameStates.SetBool("showMap", true);
    }



    void Update() {

    }
}
