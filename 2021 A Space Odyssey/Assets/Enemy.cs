using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float health = 100;

    private GameObject starship;

    void Start() {
        starship = GameObject.FindGameObjectsWithTag("Starship")[0];
    }

    void Update() {
        transform.LookAt(starship.transform);
    }
}
