using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public float damage = 20;
    public float velocityDamage = 1.5f;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Starship") {
            if (Starship.getVelocity() > velocityDamage) {
                Starship.ApplyDamage(damage);
            }
        }
    }

}
