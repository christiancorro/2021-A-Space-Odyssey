using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingDustTrigger : MonoBehaviour {

    [SerializeField] Transform startPoint;
    [SerializeField]
    ParticleSystem landingDustRight, landingDustLeft;

    [SerializeField] Vector3 offsetPositionX = new Vector3(2, 0, 0);

    [SerializeField] bool showTrigger = false;


    void Start() {
        if (showTrigger) {
            GetComponent<Renderer>().material.color = new Color(0, 1, 1, 0.1f);
        } else {
            GetComponent<Renderer>().material.color = new Color(0, 1, 1, 0);
        }
        StopDust();
    }

    private void OnTriggerStay(Collider other) {
        if (Input.GetAxis("Vertical") > 0 && (other.gameObject.tag == "Planet" || other.gameObject.tag == "Mars" || other.gameObject.tag == "Vesta") && GameStateManager.canStarShipMove() && GameStateManager.isInGame()) {
            SetDustPosition(other.ClosestPoint(startPoint.position));
            PlayDust();
            Debug.DrawLine(transform.position, other.ClosestPoint(transform.position), Color.red, 0.1f);
        } else {
            StopDust();
        }
    }


    private void OnTriggerExit(Collider other) {
        if ((other.gameObject.tag == "Planet" || other.gameObject.tag == "Mars" || other.gameObject.tag == "Vesta")) {
            StopDust();
        }
    }

    private void PlayDust() {
        landingDustLeft.Play();
        landingDustRight.Play();
    }

    private void StopDust() {
        landingDustLeft.Stop();
        landingDustRight.Stop();
    }

    private void SetDustPosition(Vector3 targetPosition) {
        landingDustLeft.transform.position = targetPosition - offsetPositionX;
        landingDustRight.transform.position = targetPosition + offsetPositionX;
    }
}
