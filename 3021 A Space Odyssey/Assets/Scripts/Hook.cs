using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {
    [SerializeField] Transform startPoint, endPoint;

    [Range(10f, 100.0f)]
    [SerializeField] float ropePullVelocity = 60.5f;

    [Range(0.0f, 100.0f)]
    [SerializeField] float springStiffness = 60.5f;

    [Range(0.0f, 10.0f)]
    [SerializeField] float springDamper = 3f;

    [Range(0.0f, 10.0f)]
    [SerializeField] float massScale = 1f;

    [Range(1f, 10f)]
    [SerializeField] float outlineWidth = 3f;

    [ColorUsage(true, true)]
    public Color outlineColor = new Color(0, 1, 1);

    [SerializeField] float maxDistance = 50;

    [SerializeField] bool showTrigger = false;

    public GameObject targetObject;


    [SerializeField] AudioSource shootHookAudio, returnHookAudio, notTargetAvailableHookAudio;

    private LineRenderer lineRenderer;

    private RopeBridge rope;

    private bool isHookShooted = false;
    private bool isTargetArrived = false;
    private SpringJoint joint;


    private List<GameObject> inside;


    private void Awake() {
        inside = new List<GameObject>();
        // hook = GetComponent<Transform>();
        rope = GetComponent<RopeBridge>();
        lineRenderer = GetComponent<LineRenderer>();
        rope.SetStartPoint(startPoint);
        rope.SetEndPoint(endPoint);
        lineRenderer.startWidth = 0;
        lineRenderer.endWidth = 0;
        if (showTrigger) {
            GetComponent<Renderer>().material.color = new Color(0, 1, 1, 0.1f);
        } else {
            GetComponent<Renderer>().material.color = new Color(0, 1, 1, 0);
        }
    }


    void Update() {
        if (GameStateManager.isInGame() && GameStateManager.CanStarShipHook()) {
            endPoint.gameObject.SetActive(true);
            // Toggle hook
            if (Input.GetButtonDown("Hook")) {
                // isActive = !isActive;
                if (!isHookShooted) {
                    StartHook();
                } else {
                    if (isTargetArrived) {
                        StopHook();
                    }
                }
            }

            // when target is disabled
            if (isHookShooted) {
                if (!targetObject.activeSelf) {
                    StopHook();
                    inside.Remove(targetObject);
                }
            }
        } else {
            endPoint.gameObject.SetActive(false);
        }
    }

    RaycastHit hit;
    void StartHook() {
        isHookShooted = true;
        // Debug.Log("Start Hook");
        // don't work well, switch to trigger
        // if (Physics.SphereCast(startPoint.position, 2f, startPoint.up, out hit, maxDistance)) 
        if (inside.Count > 0) { // check if any object hookable
            shootHookAudio.Play();
            var outlineCloser = closestObject.gameObject.GetComponent<Outline>();
            outlineCloser.OutlineWidth = 0;
            targetObject = closestObject;
            StartCoroutine(ShootHook(targetObject.transform));
            joint = targetObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = startPoint.position;
            float distanceFromPoint = Vector3.Distance(startPoint.position, targetObject.transform.position);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = Mathf.Min(maxDistance, distanceFromPoint);
            joint.minDistance = -10f;

        } else {
            // No target, hook return
            isHookShooted = false;
            notTargetAvailableHookAudio.Play();
        }
    }

    void StopHook() {
        // Debug.Log("Stop Hook");
        returnHookAudio.Play();
        StartCoroutine(ReturnHook());
    }

    void DrawRope() {
        //If not grappling, don't draw rope
        if (joint) {
            joint.maxDistance += Input.GetAxis("Hook Rope Length") * Time.deltaTime * ropePullVelocity;
            if (joint.maxDistance > maxDistance) joint.maxDistance = maxDistance;
            joint.connectedAnchor = startPoint.position;
        }

        if (isHookShooted) {
            UpdateTargetPosition(targetObject.transform);
            rope.Draw();
        }
    }

    private void FixedUpdate() {
        if (isHookShooted) {
            rope.Simulate();
        }
    }

    void LateUpdate() {
        if (GameStateManager.CanStarShipHook()) {
            DrawRope();
        }
    }

    private void UpdateTargetPosition(Transform target) {
        if (target && isHookShooted && isTargetArrived) {
            endPoint.position = target.position;
            rope.SetRopeLength(joint.maxDistance);
        }
    }

    IEnumerator ShootHook(Transform target) {
        // Debug.Log("Shoot Hook");
        isHookShooted = true;
        isTargetArrived = false;
        float distance;
        float time = 0;
        while (time <= 0.15) {
            time += Time.deltaTime;
            distance = Vector3.Distance(endPoint.position, target.position);
            rope.SetRopeLength(distance);
            endPoint.position = Vector3.Slerp(endPoint.position, target.position, Time.deltaTime * 10);
            yield return null;
        }
        isTargetArrived = true;
        yield return null;
    }


    IEnumerator ReturnHook() {
        // Debug.Log("Hook returning");
        yield return StartCoroutine(ShootHook(startPoint));
        yield return StartCoroutine(HookShutdown());
    }

    IEnumerator HookShutdown() {
        // Debug.Log("Hook returned");
        endPoint.position = startPoint.position;
        rope.UpdateEndPointPosition(startPoint.position);
        isHookShooted = false;
        lineRenderer.positionCount = 0;
        endPoint.position = startPoint.position;
        targetObject = null;
        selectedOldObject = null;
        Destroy(joint);
        isTargetArrived = false;
        yield return null;
    }

    private GameObject closestObject;
    private GameObject selectedOldObject;

    private void OnTriggerEnter(Collider other) {
        if (IsInLayerMask(other.gameObject, LayerMask.GetMask("Hookable"))) {
            var outline = other.gameObject.GetComponent<Outline>();
            if (!outline) {
                outline = other.gameObject.AddComponent<Outline>();
            }

            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = outlineColor;
            outline.OutlineWidth = 0f;

            inside.Add(other.gameObject);
            // Debug.Log("Add " + inside.Count);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (!isHookShooted && GameStateManager.CanStarShipHook()) {
            if (IsInLayerMask(other.gameObject, LayerMask.GetMask("Hookable"))) {
                closestObject = getClosetToCenterObject();
                if (selectedOldObject != closestObject) {
                    var outlineCloset = closestObject.gameObject.GetComponent<Outline>();
                    if (selectedOldObject == null) {
                        // Debug.Log("Change Color of " + closestObject.name);
                        outlineCloset.OutlineWidth = outlineWidth;
                    } else {
                        var outlineSelected = selectedOldObject.gameObject.GetComponent<Outline>();
                        // Debug.Log("Change Color of " + closestObject.name);
                        outlineCloset.OutlineWidth = outlineWidth;
                        outlineSelected.OutlineWidth = 0;
                    }
                    selectedOldObject = closestObject;
                    // Debug.Log("Closer now is " + closestObject.name);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (IsInLayerMask(other.gameObject, LayerMask.GetMask("Hookable"))) {
            try {
                if (selectedOldObject == other.gameObject) {
                    var outline = selectedOldObject.GetComponent<Outline>();
                    outline.OutlineWidth = 0;
                    selectedOldObject = null;
                }
                inside.Remove(other.gameObject);
                // Debug.Log("Remove " + inside.Count);
            } catch { }
        }
    }

    private float GetDistPointToLine(Vector3 origin, Vector3 direction, Vector3 point) {
        Vector3 point2origin = origin - point;
        Vector3 point2closestPointOnLine = point2origin - Vector3.Dot(point2origin, direction) * direction;
        return point2closestPointOnLine.magnitude;
    }

    private GameObject getClosetToCenterObject() {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        foreach (GameObject t in inside) {

            // distance based on distance from center and distance from start point
            float dist = (3 * GetDistPointToLine(startPoint.position, startPoint.up, t.transform.position) + Vector3.Distance(startPoint.position, t.transform.position)) / 4;

            if (dist < minDist) {
                tMin = t;
                minDist = dist;
            }
        }
        Debug.DrawLine(startPoint.position, startPoint.position + (startPoint.up * maxDistance), Color.red, 0.016f);
        return tMin;
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask) {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }

}
