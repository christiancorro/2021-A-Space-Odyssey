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
                Debug.DrawLine(new Vector3(transform.position.x - 300, transform.position.y, 0), new Vector3(transform.position.x + 300, transform.position.y, 0), Color.green, 2);
                triggered = true;
                onTriggerEnterEvent.Invoke();
            }

            if (!triggerOnlyOnce) {
                Debug.DrawLine(new Vector3(transform.position.x - 300, transform.position.y, 0), new Vector3(transform.position.x + 300, transform.position.y, 0), Color.green, 2);
                triggered = true;
                onTriggerEnterEvent.Invoke();
            }

            Debug.Log("Event");
        }
    }

}
