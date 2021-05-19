using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

    [SerializeField] Animator gameStateManager;
    [SerializeField] Animator startMenu;


    void Start() {

    }

    void Update() {

        // Toggle pause
        if (Input.GetButtonDown("Pause")) {
            Debug.Log("Pause");
            gameStateManager.SetBool("pause", !gameStateManager.GetBool("pause"));
        }

        if (gameStateManager.GetCurrentAnimatorStateInfo(0).IsName("Start Menu") || gameStateManager.GetCurrentAnimatorStateInfo(0).IsName("Pause")) {
            startMenu.SetBool("show", true);
        } else {
            startMenu.SetBool("show", false);
        }
    }
}
