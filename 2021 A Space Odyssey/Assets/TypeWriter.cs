using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TypeWriter : MonoBehaviour {

    [SerializeField] TMP_Text page;
    [TextArea(3, 20)]
    [SerializeField] string textToWrite;

    int characterIndex;
    [SerializeField] float timePerCharacter;
    float timer;
    bool active = true;
    string currentText;

    void Start() {

    }

    public void Write(string textToWrite, float timePerCharacter) {
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        active = true;
        characterIndex = 0;
    }

    void Update() {
        if (active) {
            if (Input.GetMouseButtonDown(0)) {
                if (textToWrite.Substring(characterIndex).IndexOf("\n\n") > 0) {
                    characterIndex = textToWrite.Substring(0, characterIndex).Length + textToWrite.Substring(characterIndex).IndexOf("\n\n");
                } else {
                    characterIndex = textToWrite.Length - 1;
                }
            }
            timer -= Time.deltaTime;
            while (timer <= 0) {
                timer += (textToWrite[characterIndex] == '.') ? timePerCharacter * 15 : timePerCharacter;
                characterIndex++;
                currentText = textToWrite.Substring(0, characterIndex);
                currentText += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                page.text = currentText;

                if (characterIndex >= textToWrite.Length) {
                    active = false;
                    return;
                }
            }
        }
    }
}
