using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetImageBasedOnInput : MonoBehaviour {

    RawImage targetRawImage;
    [SerializeField] Texture2D imageForController;
    [SerializeField] Texture2D imageForMouseKeyboard;

    void Start() {
        targetRawImage = GetComponent<RawImage>();

        if (Input.GetJoystickNames().Length > 0) {
            targetRawImage.texture = imageForController;
        } else {
            targetRawImage.texture = imageForMouseKeyboard;
        }
    }
}
