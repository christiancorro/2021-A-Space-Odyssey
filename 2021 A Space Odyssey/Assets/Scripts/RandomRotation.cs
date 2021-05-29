using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {
    [SerializeField] Vector3 rotationSpeed;
    private Transform t;

    void Start() {
        t = GetComponent<Transform>();
    }

    void Update() {
        t.Rotate(Time.deltaTime * rotationSpeed * 10);
    }
}
