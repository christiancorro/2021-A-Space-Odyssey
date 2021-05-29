using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStateManager : MonoBehaviour {

    public static Animator tutorialStates;

    void Awake() {
        tutorialStates = GetComponent<Animator>();
    }

    public static void ShowPlanetIndicator() {
        tutorialStates.SetTrigger("showPlanetIndicator");
    }

    public static void ShowVestaIndicator() {
        tutorialStates.SetTrigger("showVestaIndicator");
    }

    public static void StartTutorial() {
        tutorialStates.SetTrigger("startTutorial");
    }

    public static void Step1() {
        tutorialStates.SetTrigger("step1");
    }

    public static void Step2() {
        tutorialStates.SetTrigger("step2");
    }

    public static void Step3() {
        tutorialStates.SetTrigger("step3");
    }

    public static void Step4() {
        tutorialStates.SetTrigger("step4");
    }

    public static void Step5() {
        tutorialStates.SetTrigger("step5");
    }

    public static void EndTutorial() {
        GameStateManager.StartGame();
        tutorialStates.SetTrigger("endTutorial");
    }

    public static void SkipTutorial() {
        tutorialStates.SetTrigger("skipTutorial");
    }


    public static bool isTutorialWaiting() {
        return tutorialStates.GetCurrentAnimatorStateInfo(0).IsName("Tutorial Waiting");
    }

    public static bool isTutorialStarted() {
        return tutorialStates.GetCurrentAnimatorStateInfo(0).IsName("Tutorial Started");
    }

    public static bool isStep1() {
        return tutorialStates.GetBool("step1");
    }

    public static bool isStep2() {
        return tutorialStates.GetBool("step2");
    }

    public static bool isStep3() {
        return tutorialStates.GetBool("step3");
    }

    public static bool isStep4() {
        return tutorialStates.GetBool("step4");
    }

    public static bool isStep5() {
        return tutorialStates.GetBool("step5");
    }

    public static bool isTutorialEnded() {
        return tutorialStates.GetBool("endTutorial");
    }

}
