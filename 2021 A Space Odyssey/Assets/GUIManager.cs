using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

    [SerializeField] Animator startMenu;


    void Start() {

    }

    void Update() {

        if (Input.GetButtonDown("Pause") && GameStateManager.isPausable()) {
            GameStateManager.TogglePause();
        }

        if (GameStateManager.isStartMenu() || GameStateManager.isPaused()) {
            startMenu.SetBool("show", true);
        } else {
            startMenu.SetBool("show", false);
        }
    }
}
