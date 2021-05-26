using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestPlanetNavigationSystem : MonoBehaviour {

    [SerializeField] HUDNavigationSystem navigationSystem;

    void Update() {
        if (GameStateManager.isPlanetNavigationSystemActive()) {
            navigationSystem.Show();
        } else {
            navigationSystem.Hide();
        }
    }
}
