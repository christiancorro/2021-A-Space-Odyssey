using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestFuelNavigationSystem : MonoBehaviour {

    [SerializeField] HUDNavigationSystem navigationSystem;
    private GameObject starship;
    public GameObject[] fuels;
    public GameObject nearestFuel;

    private void Start() {
        starship = GameObject.FindGameObjectsWithTag("Starship")[0];
        fuels = GameObject.FindGameObjectsWithTag("Fuel");
        InvokeRepeating("UpdateNearestFuel", 0, 2); // update asteroids every 2 seconds
    }

    void Update() {
        if (GameStateManager.isFuelNavigationSystemActive()) {
            navigationSystem.Show();
        } else {
            navigationSystem.Hide();
        }
    }

    public void UpdateFuels() {
        StartCoroutine(UpdateFuelsCoroutine());
    }

    IEnumerator UpdateFuelsCoroutine() {
        fuels = GameObject.FindGameObjectsWithTag("Fuel");
        yield return null;
    }

    private float distance = 0;
    void UpdateNearestFuel() {
        float min = 100000000;
        nearestFuel = null;
        try {
            for (int i = 0; i < fuels.Length; i++) {
                if (fuels[i].activeSelf) {
                    distance = (fuels[i].transform.position - starship.transform.position).magnitude;
                    if (distance < min) {
                        min = distance;
                        nearestFuel = fuels[i];
                    }
                }
            }
            navigationSystem.SetTarget(nearestFuel);
        } catch { }
    }

    private void OnDestroy() {
        Array.Clear(fuels, 0, fuels.Length);
    }
}
