using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MidGameManager : MonoBehaviour {

    [SerializeField] AudioMixerSnapshot startGameSnapshot;

    [Header("Step0 Liftoff")]
    [SerializeField] TypeWriter comunicationsWriter;
    [SerializeField] Sentences sentence0_1;
    [SerializeField] Sentences sentence0_2;

    private bool gameStarted = false;

    void Start() {
        Debug.Log("Game Scene loaded");
        startGameSnapshot.TransitionTo(1f);
        GameStateManager.BlockStarShipMovements();
        GameStateManager.HideHUD();
        GameStateManager.TestGame();
    }


    void Update() {
        if (GameStateManager.isInGame() && !GameStateManager.isTutorial()) {

            if (!comunicationsWriter.HasAlreadyWritten()) {
                comunicationsWriter.Write(sentence0_1);
            }

            if (!gameStarted && Input.GetAxis("Vertical") > 0) {
                gameStarted = true;
                GameStateManager.AllowStarShipMovements();
                GameStateManager.ShowHUD();
                comunicationsWriter.Write(sentence0_2);
            }
            GameStateManager.AllowStarShipHook();
            GameStateManager.ShowPlanetNavigationSystem();
            GameStateManager.ShowFuelNavigationSystem();
            GameStateManager.ShowVestaNavigationSystem();
        }
    }
}
