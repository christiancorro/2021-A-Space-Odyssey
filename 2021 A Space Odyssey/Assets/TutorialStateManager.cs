using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStateManager : MonoBehaviour {

    public static Animator tutorialStates;

    void Awake() {
        tutorialStates = GetComponent<Animator>();
    }

    public static void StartTutorial() {
        tutorialStates.SetTrigger("startTutorial");
    }

    public static void EndTutorial() {
        tutorialStates.SetTrigger("endTutorial");
    }

    public static void SkipTutorial() {
        tutorialStates.SetTrigger("skipTutorial");
    }

    public static bool isTutorialEnded() {
        return tutorialStates.GetCurrentAnimatorStateInfo(0).IsName("Tutorial Ended");
    }
}
