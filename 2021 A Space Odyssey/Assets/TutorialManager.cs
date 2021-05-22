using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {


    [SerializeField] TypeWriter writer1;
    [SerializeField] Sentences sentence1;

    [SerializeField] TypeWriter writer2;
    [SerializeField] Sentences sentence2;

    [SerializeField] TypeWriter writer3;
    [SerializeField] Sentences sentence3;


    [SerializeField] GameObject tutorialObjects;
    [SerializeField] GameObject step1;

    void Start() {
        step1.SetActive(false);
    }

    void Update() {

        // Starts tutorial
        if (GameStateManager.isTutorial()) {

            if (TutorialStateManager.isTutorialWaiting()) {
                TutorialStateManager.StartTutorial();
                tutorialObjects.SetActive(true);
                Debug.Log("WRITE!!!!!!!!");
                writer1.Write(sentence1);
            }

            if (TutorialStateManager.isTutorialEnded()) {
                // firstPersonWriter.Write(fpSentence1);
                Debug.Log("Step ended");
                hideTutorialObjects();
            }

            if (!GameStateManager.isPaused() && Input.GetButtonDown("Back")) {
                TutorialStateManager.EndTutorial();
            }
        }
    }

    private void hideTutorialObjects() {
        for (int i = 0; i < tutorialObjects.transform.childCount; i++) {
            tutorialObjects.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void StartStep1() {
        GameStateManager.AllowStarShipMovements();
        step1.SetActive(true);
        writer2.Write(sentence2);
    }

    public void EndStep1() {
        step1.SetActive(false);
        writer3.Write(sentence3);
    }


}
