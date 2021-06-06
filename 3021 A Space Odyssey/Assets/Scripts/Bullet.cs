using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] float speed = 35f;
    [SerializeField] float demage = 10f;
    [SerializeField] float lifeTime = 8;
    private Rigidbody rb;

    private float timer;

    private void Update() {

        // Add bullet lifetime

        if (timer < lifeTime) {
            timer += Time.deltaTime;
        }

        if (timer > lifeTime) {
            Destroy(gameObject);
        }
    }

    void Start() {
        rb = GetComponent<Rigidbody>();

        // bullet goes only forward at a constant velocity
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Starship") {
            Starship.ApplyDamage(demage);
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
