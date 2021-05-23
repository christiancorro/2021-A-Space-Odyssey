using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VestaNavigationSystem : MonoBehaviour {

    [SerializeField] HUDNavigationSystem nearestPlanetNavigationSystem;

    void Update() {
        if (GameStateManager.isVestaNavigationSystemActive()) {
            nearestPlanetNavigationSystem.Show();
        } else {
            nearestPlanetNavigationSystem.Hide();
        }
    }
}
