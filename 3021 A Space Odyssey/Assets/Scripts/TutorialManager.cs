using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    // Tutorial sequence

    [SerializeField] AudioSource tutorialAudio;
    [SerializeField] AudioMixerSnapshot tutorialSnapshot;

    [Header("Step1 Movements")]
    [SerializeField] GameObject step1Trigger;

    [SerializeField] TypeWriter step1Writer1;
    [SerializeField] Sentences step1Sentence1;

    [SerializeField] TypeWriter step1Writer2;
    [SerializeField] Sentences step1Sentence2;

    [SerializeField] TypeWriter step1Writer3;
    [SerializeField] Sentences step1Sentence3;

    [Header("Step2 Fuel")]
    [SerializeField] GameObject step2Trigger;

    [SerializeField] TypeWriter step2Writer1;
    [SerializeField] Sentences step2Sentence1;
    [SerializeField] TypeWriter step2Writer2;
    [SerializeField] Sentences step2Sentence2;

    [SerializeField] TypeWriter step2Writer3;
    [SerializeField] Sentences step2Sentence3;


    [Header("Step3 Hook")]
    [SerializeField] GameObject step3Trigger;
    [SerializeField] TypeWriter step3Writer1;
    [SerializeField] Sentences step3Sentence1;

    [SerializeField] TypeWriter step3Writer2;
    [SerializeField] Sentences step3Sentence2;

    [SerializeField] TypeWriter step3Writer3;
    [SerializeField] Sentences step3Sentence3;

    [Header("Step4 Planets")]
    [SerializeField] GameObject step4Trigger;
    [SerializeField] TypeWriter step4Writer1;
    [SerializeField] Sentences step4Sentence1;

    [SerializeField] TypeWriter step4Writer2;
    [SerializeField] Sentences step4Sentence2;

    [SerializeField] TypeWriter step4Writer3;
    [SerializeField] Sentences step4Sentence3;

    [Header("Step5 Vesta")]
    [SerializeField] TypeWriter step5Writer1;
    [SerializeField] Sentences step5Sentence1;
    [SerializeField] TypeWriter step5Writer2;
    [SerializeField] Sentences step5Sentence2;


    [Header("HUD Navigation System")]
    [SerializeField] HUDNavigationSystem tutorialTargetsNavigationSystem;

    [SerializeField] GameObject tutorialObjects;


    void Start() {
        step1Trigger.SetActive(false);
        step2Trigger.SetActive(false);
        step3Trigger.SetActive(false);
        GameStateManager.BlockStarShipMovements();
        GameStateManager.BlockStarShipHook();
        GameStateManager.HideVestaNavigationSystem();
        GameStateManager.HideFuelNavigationSystem();
        GameStateManager.HidePlanetNavigationSystem();
        GameStateManager.HideHUD();
    }

    void Update() {

        // Starts tutorial
        if (GameStateManager.isTutorial()) {

            tutorialTargetsNavigationSystem.gameObject.SetActive(true);

            if (TutorialStateManager.isTutorialWaiting()) {

                if (!tutorialAudio.isPlaying) {
                    tutorialAudio.Play();
                }
                tutorialSnapshot.TransitionTo(0.1f);
                GameStateManager.BlockStarShipMovements();
                TutorialStateManager.StartTutorial();
                tutorialObjects.SetActive(true);
                step1Writer1.Write(step1Sentence1);
            }

            if (TutorialStateManager.isTutorialEnded()) {
                // firstPersonWriter.Write(fpSentence1);
                Debug.Log("Step ended");
                hideTutorialObjects();
            }


            if (!GameStateManager.isPaused() && Input.GetButtonDown("Back")) {
                // TutorialStateManager.EndTutorial();
                // TODO: Implement Skip tutorial
                Debug.Log("Skip tutorial");
                EndTutorial();
            }
        } else {
            tutorialTargetsNavigationSystem.gameObject.SetActive(false);
        }
    }

    private void hideTutorialObjects() {
        for (int i = 0; i < tutorialObjects.transform.childCount; i++) {
            tutorialObjects.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void StartStep1_Movements() {
        // GameStateManager.BlockStarShipMovements();
        GameStateManager.AllowStarShipMovements();
        TutorialStateManager.Step1();
        step1Trigger.SetActive(true);
        step1Writer2.Write(step1Sentence2);
        tutorialTargetsNavigationSystem.Show();
        tutorialTargetsNavigationSystem.SetTarget(step1Trigger);
    }

    public void EndStep1() {
        step1Trigger.SetActive(false);
        step1Writer3.Write(step1Sentence3);
        tutorialTargetsNavigationSystem.Hide();
        GameStateManager.BlockStarShipMovements();
    }

    public void ShowHUD() {
        Starship.oxygen = 100;
        GameStateManager.ShowHUD();
        step2Writer1.Write(step2Sentence1);
        GameStateManager.AllowStarShipMovements();
    }

    public void StartStep2_Fuel() {
        TutorialStateManager.Step2();
        step2Trigger.SetActive(true);
        tutorialTargetsNavigationSystem.Show();
        tutorialTargetsNavigationSystem.SetTarget(step2Trigger);
    }

    public void Step2_Attract_Message() {
        if (!step2Writer2.HasAlreadyWritten() && TutorialStateManager.isStep2()) {
            Starship.fuel = 100;
            step2Writer2.Write(step2Sentence2);
            GameStateManager.BlockStarShipMovements();
        }
    }

    public void EndStep2() {
        step2Trigger.SetActive(false);
        step2Writer3.Write(step2Sentence3);
        tutorialTargetsNavigationSystem.Hide();
    }

    public void StartStep3_Hook() {
        TutorialStateManager.Step3();
        step3Trigger.SetActive(true);
        step3Writer1.Write(step3Sentence1);
        tutorialTargetsNavigationSystem.SetTarget(step3Trigger);
        tutorialTargetsNavigationSystem.Show();
    }

    public void Step3_Hook_Message() {
        if (!step3Writer2.HasAlreadyWritten() && TutorialStateManager.isStep3()) {
            Starship.fuel = 0;
            step3Writer2.Write(step3Sentence2);
            GameStateManager.BlockStarShipMovements();
        }
    }

    public void Step3_ActivateHook() {
        GameStateManager.AllowStarShipHook();
        GameStateManager.AllowStarShipMovements();
    }


    public void EndStep3() {
        step3Trigger.SetActive(false);
        step3Writer3.Write(step3Sentence3);
        GameStateManager.ShowFuelNavigationSystem();
        tutorialTargetsNavigationSystem.Hide();
    }

    public void StartStep4_Planets() {
        TutorialStateManager.Step4();
        step4Writer1.Write(step4Sentence1);
        Starship.oxygen = 100;
    }

    public void Step4_Activate_Planets_Navigation_System() {
        step4Writer2.Write(step4Sentence2);
        GameStateManager.ShowPlanetNavigationSystem();
    }

    public void Step4_Planet_Landing_Message() {
        if (!step4Writer3.HasAlreadyWritten() && TutorialStateManager.isStep4()) {
            step4Writer3.Write(step4Sentence3);
            GameStateManager.BlockStarShipMovements();
        }
    }

    public void EndStep4() {
        TutorialStateManager.Step5();
        if (!step5Writer1.HasAlreadyWritten()) {
            step5Writer1.Write(step5Sentence1);
        }
        GameStateManager.ShowVestaNavigationSystem();
    }

    public void EndTutorial() {
        TutorialStateManager.EndTutorial();
        GameStateManager.StartGame();
        // SceneLoader.LoadGameScene();
    }


}
