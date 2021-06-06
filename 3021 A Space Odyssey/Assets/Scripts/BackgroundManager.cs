using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {


    [SerializeField] Transform target;
    [SerializeField] GameObject stars1;
    [SerializeField] GameObject stars2;
    [SerializeField] float maxRadius;

    private Vector3 currentCenter;
    private float distance;
    private GameObject currentStars;

    private void Start() {
        currentStars = stars1;
        currentCenter = target.position;
        stars1.transform.position = currentCenter;
    }

    void Update() {
        if (GameStateManager.isInGame()) {
            stars1.SetActive(true);
            stars2.SetActive(true);

            Debug.DrawLine(target.position, currentCenter, Color.white, 0.1f);
            distance = Vector3.Distance(target.position, currentCenter);

            if (distance > maxRadius) {

                if (currentStars == stars1) {
                    currentStars = stars2;
                } else {
                    currentStars = stars1;
                }

                currentCenter = target.position;
                currentStars.transform.position = currentCenter;
            }
        }
    }
}
