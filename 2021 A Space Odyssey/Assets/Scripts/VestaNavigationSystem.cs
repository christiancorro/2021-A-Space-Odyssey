using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VestaNavigationSystem : MonoBehaviour {

    [SerializeField] HUDNavigationSystem navigationSystem;
    private GameObject vesta;

    private void Start() {
        vesta = GameObject.FindGameObjectsWithTag("Vesta")[0];
        navigationSystem.SetTarget(vesta);
    }

    void Update() {
        if (GameStateManager.isVestaNavigationSystemActive()) {
            navigationSystem.Show();
        } else {
            navigationSystem.Hide();
        }
    }
}
