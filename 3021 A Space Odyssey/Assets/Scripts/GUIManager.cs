using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

    [SerializeField] StartMenuManager startMenu;
    [SerializeField] PauseMenuManager pauseMenu;


    void Start() {

    }

    void Update() {

        //  !(Input.GetJoystickNames().Length > 0) && 
        if ((GameStateManager.isStartMenu() || GameStateManager.isPaused())) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.visible = false;
            // Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetButtonDown("Pause") && GameStateManager.isPausable()) {
            GameStateManager.TogglePause();
        }

        if (GameStateManager.isStartMenu()) {
            startMenu.OpenStartMenu();
        } else {
            startMenu.CloseStartMenu();
        }

        if (GameStateManager.isPaused()) {
            pauseMenu.OpenPauseMenu();
        } else {
            pauseMenu.ClosePauseMenu();
        }
    }
}
