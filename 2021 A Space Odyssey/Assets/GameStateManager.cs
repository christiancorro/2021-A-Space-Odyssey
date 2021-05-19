using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStateManager : MonoBehaviour {

    public static Animator gameStates;

    void Awake() {
        gameStates = GetComponent<Animator>();
    }


    private void Update() {
        if (GameStatusManager.isInit() && Input.GetAxis("Vertical") > 0) {
        }
    }

    void OnEnable() {
        Debug.Log("Init");

    }

    public static bool isIntro() {
        return gameStates.GetCurrentAnimatorStateInfo(0).IsName("Initial Story");
    }

    public static bool isTutorial() {
        return gameStates.GetCurrentAnimatorStateInfo(0).IsName("Tutorial");
    }

    public static bool isGameover() {
        return gameStates.GetCurrentAnimatorStateInfo(0).IsName("Game Over");
    }

    public static bool isPaused() {
        return gameStates.GetCurrentAnimatorStateInfo(0).IsName("Pause");
    }

    public static bool startPlayList() {
        return
                // !gameStates.GetBool("tutorial") &&
                gameStates.GetCurrentAnimatorStateInfo(0).IsName("Tutorial")
                || (gameStates.GetCurrentAnimatorStateInfo(0).IsName("Pause")
               || gameStates.GetCurrentAnimatorStateInfo(0).IsName("Start Game")
               || gameStates.GetCurrentAnimatorStateInfo(0).IsName("Mid Game")
               || gameStates.GetCurrentAnimatorStateInfo(0).IsName("End Game"));
    }

    public static bool isPausable() {
        return gameStates.GetCurrentAnimatorStateInfo(0).IsName("Pause")
                || gameStates.GetCurrentAnimatorStateInfo(0).IsName("Initial Story")
                || gameStates.GetCurrentAnimatorStateInfo(0).IsName("Tutorial")
                || gameStates.GetCurrentAnimatorStateInfo(0).IsName("Start Game")
                || gameStates.GetCurrentAnimatorStateInfo(0).IsName("Mid Game")
                || gameStates.GetCurrentAnimatorStateInfo(0).IsName("End Game");
    }


    public static bool isInGame() {
        return gameStates.GetCurrentAnimatorStateInfo(0).IsName("Tutorial")
                || gameStates.GetCurrentAnimatorStateInfo(0).IsName("Start Game")
                || gameStates.GetCurrentAnimatorStateInfo(0).IsName("Mid Game");
    }

    public static bool isStartMenu() {
        return gameStates.GetCurrentAnimatorStateInfo(0).IsName("Start Menu");
    }

    public static bool isInit() {
        return gameStates.GetCurrentAnimatorStateInfo(0).IsName("Start Menu");
    }

    public static void TogglePause() {
        if (gameStates.GetBool("pause")) {
            gameStates.SetBool("pause", false);
            Time.timeScale = 1;
        } else {
            gameStates.SetBool("pause", true);
            Time.timeScale = 0;
        }

        Debug.Log("Pause");
    }

    public static void NewGame() {
        gameStates.SetBool("newGame", true);
        Debug.Log("New Game");
    }

    public static void Gameover() {
        gameStates.SetTrigger("gameOver");
        Debug.Log("Gameover");
    }

    public static void StartTutorial() {
        gameStates.SetBool("tutorial", true);
        Debug.Log("Tutorial started");
    }
}
