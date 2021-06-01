using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnTriggerEnterEvent : UnityEvent { }

public class TriggerEvent : MonoBehaviour {

    [SerializeField] OnTriggerEnterEvent onTriggerEnterEvent;
    [SerializeField] bool triggerOnlyOnce = true;
    private bool triggered = false;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Starship") {
            if (triggerOnlyOnce && !triggered) {
                triggered = true;
                onTriggerEnterEvent.Invoke();
            }

            if (!triggerOnlyOnce) {
                triggered = true;
                onTriggerEnterEvent.Invoke();
            }

            Debug.Log("Event");
        }
    }

}
