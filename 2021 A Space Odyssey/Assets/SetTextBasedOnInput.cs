using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTextBasedOnInput : MonoBehaviour {

    [SerializeField] Sentences sentencesMouseKeyboard;
    [TextArea(10, 40)]
    [SerializeField] string[] sentencesXboxController;

    void Start() {
        if (Input.GetJoystickNames().Length > 0) {
            for (int i = 0; i < sentencesMouseKeyboard.sentences.Length; i++) {
                sentencesMouseKeyboard.sentences[i] = sentencesXboxController[i];
            }

        }
    }
}
