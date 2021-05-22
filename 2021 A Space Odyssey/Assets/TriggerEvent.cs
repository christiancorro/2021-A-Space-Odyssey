using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnTriggerEnterEvent : UnityEvent { }

public class TriggerEvent : MonoBehaviour {

    [SerializeField] OnTriggerEnterEvent onTriggerEnterEvent;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Starship") {
            onTriggerEnterEvent.Invoke();
            Debug.Log("Event");
        }
    }

}
