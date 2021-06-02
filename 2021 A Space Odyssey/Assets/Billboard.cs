using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    private Transform cam;

    private void Awake() {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void LateUpdate() {
        transform.LookAt(transform.position + cam.forward);
        transform.position = this.gameObject.transform.parent.position + Vector3.up * 2f;
    }

}
