using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {

    public static GameStateManager instance;
    public static Animator gameStates;

    void Awake() {
        if (!instance) {
            instance = this;
            gameStates = GetComponent<Animator>();
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start() {
        Time.timeScale = 1;
    }

    private void Update() {
    }

    void OnEnable() {
        // Debug.Log("Init");
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
        return (!gameStates.GetBool("tutorial") || gameStates.GetBool("newGame")) &&
                 (gameStates.GetCurrentAnimatorStateInfo(0).IsName("Pause")
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
        return gameStates.GetCurrentAnimatorStateInfo(0).IsName("Start Menu") && !(gameStates.GetBool("tutorial") || gameStates.GetBool("startGame") || gameStates.GetBool("midGame"));
    }

    public static bool isInit() {
        return gameStates.GetCurrentAnimatorStateInfo(0).IsName("Start Menu");
    }

    public static void TogglePause() {
        if (gameStates.GetBool("pause")) {
            gameStates.SetBool("pause", false);
        } else {
            gameStates.SetBool("pause", true);
        }

        Debug.Log("Pause");
    }

    public static void Restart() {
        // if (isInGame()) {
        //     StartGame();
        // }
        gameStates.SetTrigger("restart");
    }

    public static void StartMenu() {
        gameStates.SetTrigger("startMenu");
    }

    public static void StartGame() {
        gameStates.SetBool("startGame", true);
        Debug.Log("Start Game");
    }

    public static bool canStarShipMove() {
        return gameStates.GetBool("allowStarShipMovements");
    }

    public static bool isHUDVisible() {
        return gameStates.GetBool("showHUD");
    }

    public static bool isPlanetNavigationSystemActive() {
        return gameStates.GetBool("showPlanetNavigationSystem");
    }

    public static bool isVestaNavigationSystemActive() {
        return gameStates.GetBool("showVestaNavigationSystem");
    }

    public static void ShowPlanetNavigationSystem() {
        gameStates.SetBool("showPlanetNavigationSystem", true);
    }

    public static void HidePlanetNavigationSystem() {
        gameStates.SetBool("showPlanetNavigationSystem", false);
    }

    public static void ShowVestaNavigationSystem() {
        gameStates.SetBool("showVestaNavigationSystem", true);
    }

    public static void HideVestaNavigationSystem() {
        gameStates.SetBool("showVestaNavigationSystem", false);
    }


    public static void ShowHUD() {
        gameStates.SetBool("showHUD", true);
    }

    public static void HideHUD() {
        gameStates.SetBool("showHUD", false);
    }

    public static void AllowStarShipMovements() {
        gameStates.SetBool("allowStarShipMovements", true);
    }

    public static void BlockStarShipMovements() {
        gameStates.SetBool("allowStarShipMovements", false);
    }

    public static void NewGame() {
        gameStates.SetBool("newGame", true);
        Debug.Log("New Game");
    }

    public static void ExitGame() {
        gameStates.SetBool("pause", false);
        gameStates.SetBool("newGame", false);
        gameStates.SetBool("tutorial", false);
        gameStates.SetTrigger("exitGame");
        Debug.Log("Exit");
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
