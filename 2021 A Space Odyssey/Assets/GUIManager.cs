using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

    [SerializeField] StartMenuManager startMenu;
    [SerializeField] PauseMenuManager pauseMenu;


    void Start() {

    }

    void Update() {

        //  
        if (!(Input.GetJoystickNames().Length > 0) && (GameStateManager.isStartMenu() || GameStateManager.isPaused())) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
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
