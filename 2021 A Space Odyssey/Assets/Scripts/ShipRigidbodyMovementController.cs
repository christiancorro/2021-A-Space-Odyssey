using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRigidbodyMovementController : MonoBehaviour {

    public float velocity;
    public float rotationSpeed;
    private new Rigidbody rigidbody;
    private Transform target;

    public float horizontal, vertical;

    // Use this for initialization
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectsWithTag("Starship")[0].transform;
    }

    // Update is called once per frame
    void Update() {
        var difference = (target.position - this.transform.position).normalized;
        horizontal = difference.x;
        vertical = difference.y;

        UpdateMoveAdaptiveRotation();

    }

    private void UpdateMoveAdaptiveRotation() {
        var inputDirection = new Vector3(horizontal, 0, vertical);
        var thrust = Vector3.Dot(inputDirection.normalized, this.transform.forward);
        var rotation = Vector3.Dot(inputDirection.normalized, this.transform.right);

        this.rigidbody.AddForce(thrust * inputDirection.magnitude *
                this.transform.forward * velocity * Time.deltaTime);

        rigidbody.AddTorque(Vector3.Cross(transform.forward, (target.transform.position - transform.position).normalized) * rotationSpeed * Time.deltaTime);
    }
}
