using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGlow : MonoBehaviour {

    // Manage the color of starship's pinhole, if low health red

    public Gradient gradient;
    private Material material;

    void Start() {
        material = this.gameObject.GetComponent<Renderer>().material;
    }

    void Update() {
        Color c = getEmissionColor(Starship.health / 100);
        material.SetColor("_EmissionColor", c);
        material.color = c;
    }

    Color getEmissionColor(float a) {
        Color c = gradient.Evaluate(a);
        c = c * 3.21f;
        return c;
    }
}
