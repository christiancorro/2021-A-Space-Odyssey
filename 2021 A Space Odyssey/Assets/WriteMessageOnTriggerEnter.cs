using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteMessageOnTriggerEnter : MonoBehaviour {

    [SerializeField] TypeWriter typeWriter;
    [SerializeField] Sentences sentences;
    [SerializeField] bool writeOnlyOnce = true;
    private bool write;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Starship") {
            if (write) {
                if (writeOnlyOnce) {
                    write = false;
                }
                typeWriter.Write(sentences);
            }
        }
    }
}
