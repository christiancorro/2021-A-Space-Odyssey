using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour {

    [SerializeField] Animator gameStatusManager;
    [SerializeField] TypeWriter plotWriter;
    [SerializeField] Sentences initialPlot;
    private Animator anim;
    private bool writing = false;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        anim.SetBool("showMap", gameStatusManager.GetBool("showMap"));

        if (!writing && gameStatusManager.GetBool("newGame")) {
            plotWriter.Write(initialPlot);
            writing = true;
        }
    }
}
