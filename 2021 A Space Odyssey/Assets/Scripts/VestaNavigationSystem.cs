using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VestaNavigationSystem : MonoBehaviour {

    [SerializeField] HUDNavigationSystem navigationSystem;

    void Update() {
        if (GameStateManager.isVestaNavigationSystemActive()) {
            navigationSystem.Show();
        } else {
            navigationSystem.Hide();
        }
    }
}
