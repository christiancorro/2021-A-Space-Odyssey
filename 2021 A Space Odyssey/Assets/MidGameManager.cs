using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MidGameManager : MonoBehaviour {


    [Header("Game Objects Groups")]
    [SerializeField] GameObject startGameObjects;
    [SerializeField] GameObject midGameObjects;
    [SerializeField] GameObject Vesta;

    [Header("Audio")]
    [SerializeField] AudioMixerSnapshot startGameSnapshot;

    [SerializeField] AudioMixerSnapshot extremeDanger;
    [SerializeField] AudioSource extremeDangerAudio;

    [Header("Step0 Liftoff")]
    [SerializeField] TypeWriter comunicationsWriter;
    [SerializeField] Sentences sentence0_1;
    [SerializeField] Sentences sentence0_2;


    [Header("Extreme Danger ")]
    [SerializeField] TypeWriter extremeDangerEndWriter;
    [SerializeField] Sentences sentence_extreme_danger_end;

    private bool gameStarted = false;

    private GameObject starship;


    void Start() {
        starship = GameObject.FindGameObjectsWithTag("Starship")[0];
        Debug.Log("Game Scene loaded");
        startGameSnapshot.TransitionTo(1f);
        GameStateManager.BlockStarShipMovements();
        GameStateManager.HideHUD();


        if (!GameStateManager.isMidGame()) {
            GameStateManager.StartGame();
        }

        if (GameStateManager.checkpoint != Vector3.zero) {
            starship.transform.position = GameStateManager.checkpoint;
            GameStateManager.AllowStarShipMovements();

            GameStateManager.AllowStarShipHook();
            GameStateManager.ShowPlanetNavigationSystem();
            GameStateManager.ShowFuelNavigationSystem();
            GameStateManager.ShowVestaNavigationSystem();
            GameStateManager.ShowHUD();
            gameStarted = true;

            Debug.Log("Checkpoint loaded");

            // Vesta near starship
            if (GameStateManager.isMidGame()) {
                Vesta.transform.position = new Vector3(GameStateManager.checkpoint.x, GameStateManager.checkpoint.y + 60, 0);
            }
        }
    }


    void Update() {
        if (GameStateManager.isStartGame()) {
            startGameObjects.SetActive(true);
            midGameObjects.SetActive(false);
        }

        if (GameStateManager.isMidGame()) {
            midGameObjects.SetActive(true);
            startGameObjects.SetActive(false);
        }

        if (GameStateManager.isInGame() && !GameStateManager.isTutorial()) {

            GameStateManager.AllowStarShipHook();
            GameStateManager.ShowPlanetNavigationSystem();
            GameStateManager.ShowFuelNavigationSystem();
            GameStateManager.ShowVestaNavigationSystem();

            if (!comunicationsWriter.HasAlreadyWritten() && !GameStateManager.isCheckpoint()) {
                comunicationsWriter.Write(sentence0_1);
            }

            // Liftoff
            if (!gameStarted && Input.GetAxis("Vertical") > 0) {
                gameStarted = true;
                GameStateManager.AllowStarShipMovements();
                GameStateManager.ShowHUD();
                if (!GameStateManager.isCheckpoint()) {
                    comunicationsWriter.Write(sentence0_2);
                }
            }

            // end extreme danger message
            if (extremeDangerAudio.isPlaying && !EnemiesManager.isExtremeDanger()) {
                if (!extremeDangerEndWriter.HasAlreadyWritten()) {
                    extremeDangerEndWriter.Write(sentence_extreme_danger_end);
                }
            }

        }
    }

    public void StartMidGame() {
        GameStateManager.AllowStarShipMovements();
        GameStateManager.StartMidGame();
        midGameObjects.SetActive(true);
        startGameObjects.SetActive(false);
    }

    public void StartExtremeDangerMusic() {
        extremeDanger.TransitionTo(0.8f);
        if (!extremeDangerAudio.isPlaying) {
            extremeDangerAudio.Play();
        }
    }




}
