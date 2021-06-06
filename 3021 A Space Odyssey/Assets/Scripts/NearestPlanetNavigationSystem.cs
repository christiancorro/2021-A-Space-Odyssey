using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestPlanetNavigationSystem : MonoBehaviour {

    [SerializeField] HUDNavigationSystem navigationSystem;
    private GameObject starship;
    public GameObject[] planets;
    public GameObject nearestPlanet;

    private void Start() {
        starship = GameObject.FindGameObjectsWithTag("Starship")[0];
        planets = GameObject.FindGameObjectsWithTag("Planet");
        InvokeRepeating("UpdateNearestPlanet", 0, 5); // update asteroids every 2 seconds
    }


    void Update() {
        if (GameStateManager.isFuelNavigationSystemActive()) {
            navigationSystem.Show();
        } else {
            navigationSystem.Hide();
        }
    }


    public void UpdatePlanets() {
        StartCoroutine(UpdatePlanetsCoroutine());
    }

    IEnumerator UpdatePlanetsCoroutine() {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        yield return null;
    }

    private float distance = 0;
    void UpdateNearestPlanet() {
        float min = 100000000;
        try {
            for (int i = 0; i < planets.Length; i++) {
                distance = (planets[i].transform.position - starship.transform.position).magnitude;
                if (distance < min) {
                    min = distance;
                    nearestPlanet = planets[i];
                }
            }
            navigationSystem.SetTarget(nearestPlanet);
        } catch { }
    }

    private void OnDestroy() {
        Array.Clear(planets, 0, planets.Length);
    }
}
