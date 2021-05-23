using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestPlanetNavigationSystem : MonoBehaviour {

    [SerializeField] HUDNavigationSystem nearestPlanetNavigationSystem;

    void Update() {
        if (GameStateManager.isPlanetNavigationSystemActive()) {
            nearestPlanetNavigationSystem.Show();
        } else {
            nearestPlanetNavigationSystem.Hide();
        }
    }
}
