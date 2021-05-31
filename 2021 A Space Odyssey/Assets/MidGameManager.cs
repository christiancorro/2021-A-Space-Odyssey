using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MidGameManager : MonoBehaviour {

    [SerializeField] AudioMixerSnapshot startGameSnapshot;
    private bool gameStarted = false;

    void Start() {
        Debug.Log("Game Scene loaded");
        startGameSnapshot.TransitionTo(1f);
        GameStateManager.BlockStarShipMovements();
        GameStateManager.HideHUD();
    }


    void Update() {
        if (GameStateManager.isInGame() && !GameStateManager.isTutorial()) {
            if (!gameStarted && Input.GetAxis("Vertical") > 0) {
                gameStarted = true;
                GameStateManager.AllowStarShipMovements();
                GameStateManager.ShowHUD();
            }
            GameStateManager.AllowStarShipHook();
            GameStateManager.ShowPlanetNavigationSystem();
            GameStateManager.ShowFuelNavigationSystem();
            GameStateManager.ShowVestaNavigationSystem();
        }
    }
}
