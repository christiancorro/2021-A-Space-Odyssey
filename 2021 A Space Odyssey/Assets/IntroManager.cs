using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour {

    [SerializeField] Animator gameStateManager;
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
        mapWriter.Write(mapText);
        mapAnimator.SetBool("showMap", true);
    }

    public void showSecondPart() {
        mapAnimator.SetBool("showMap", false);
    }

    public void triggerTutorial() {
        Debug.Log("XIAO");
        gameStateManager.SetBool("tutorial", true);
    }

    void Update() {
        if (!writing && gameStateManager.GetCurrentAnimatorStateInfo(0).IsName("Initial Story")) {
            Debug.Log("Initial Story");
            plotWriter.Write(initialPlot);
            writing = true;
        }
    }
}
