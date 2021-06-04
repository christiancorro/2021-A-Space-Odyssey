using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Starship") {
            GameStateManager.SetCheckpoint(transform.position);
            Debug.DrawLine(new Vector3(transform.position.x - 300, transform.position.y, 0), new Vector3(transform.position.x + 300, transform.position.y, 0), Color.green, 5);
            Debug.Log("Checkpoint " + transform.position);
        }
    }

}
