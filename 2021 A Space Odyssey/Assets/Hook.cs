using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
    private Transform hook;
    private LineRenderer lineRenderer;
    private GameObject targetObject;

    private RopeBridge rope;

    private bool isActive = false;
    private bool isStarted = false;
    private SpringJoint joint;

    [SerializeField] float maxDistance = 50;

    private void Awake() {
        hook = GetComponent<Transform>();
        rope = GetComponent<RopeBridge>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update() {
        if (GameStateManager.isInGame()) {
            // Toggle hook
            if (Input.GetButtonDown("Hook")) {
                isActive = !isActive;
            }
            // else {
            // isActive = false;
            // }

            if (isActive && !isStarted) {
                StartHook();
                isStarted = true;
            }

            if (!isActive && isStarted) {
                StopHook();
                isStarted = false;
            }
        }
    }

    RaycastHit hit;
    void StartHook() {

        Debug.Log("Start Hook");
        if (Physics.SphereCast(hook.position, 2f, hook.up, out hit, maxDistance)) {
            targetObject = hit.transform.gameObject;
            joint = hit.transform.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = hook.position;
            float distanceFromPoint = Vector3.Distance(hook.position, targetObject.transform.position);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = Mathf.Min(30, distanceFromPoint);
            joint.minDistance = 0.1f;

            //Adjust these values to fit your game.
            joint.spring = 60.5f;
            joint.damper = 1f;
            joint.massScale = 1f;

            lineRenderer.positionCount = 2;
        }
    }
    void StopHook() {
        Debug.Log("Stop Hook");
        lineRenderer.positionCount = 0;
        rope.SetTarget(hook);
        Destroy(joint);
    }

    void DrawRope() {
        //If not grappling, don't draw rope
        if (joint) {
            // rope.SetTarget(hook);
            // lineRenderer.SetPosition(0, targetPoint.position);
            // lineRenderer.SetPosition(1, hook.position);
            joint.maxDistance += Input.GetAxis("Hook Rope Length") * Time.deltaTime * 20;
            joint.connectedAnchor = hook.position;
            rope.SetTarget(targetObject.transform);
            rope.SetRopeLength(joint.maxDistance);
            rope.Draw();
        }
    }

    private void FixedUpdate() {
        if (joint) {
            rope.Simulate();
        }
    }

    void LateUpdate() {
        DrawRope();
    }



}
