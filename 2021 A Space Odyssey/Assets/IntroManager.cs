using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour {

    [SerializeField] Animator introAnimation;

    [SerializeField] TypeWriter plotWriter;
    [SerializeField] Sentences initialPlot;

    [SerializeField] TypeWriter mapMessageWriter;
    [SerializeField] Sentences mapMessageText;

    [SerializeField] TypeWriter mapWriter;
    [SerializeField] Sentences mapText;

    [SerializeField] TypeWriter plotWriter2;
    [SerializeField] Sentences initialPlot2;

    [SerializeField] Animator mapAnimator;

    private Animator anim;
    private bool writing = false;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void showMapMessage() {
        mapMessageWriter.Write(mapMessageText);
    }

    public void showMap() {
        Debug.Log("Map");
        mapWriter.Write(mapText);
        mapAnimator.SetBool("showMap", true);
    }

    public void showSecondPart() {
        mapAnimator.SetBool("showMap", false);
        plotWriter2.Write(initialPlot2);
        Debug.Log("Second part initial story");
    }

    public void triggerTutorial() {
        Debug.Log("Tutorial");
        GameStateManager.StartTutorial();
    }

    void Update() {
        if (GameStateManager.isIntro()) {
            introAnimation.SetBool("showIntro", true);

            if (!writing) {
                Debug.Log("Initial Story");
                plotWriter.Write(initialPlot);
                writing = true;
            }
        } else {
            introAnimation.SetBool("showIntro", false);
        }
    }
}
