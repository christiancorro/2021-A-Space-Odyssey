using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDNearestPlanetNavigationSystem : MonoBehaviour {

    public GameObject initTarget;
    public Camera UICamera;
    [SerializeField] float borderSize = 0;

    private static GameObject target;
    private RectTransform pointer;

    private Vector3 fromPosition, toPosition, direction, targetPositionScreenPoint, targetScreenPosition, pointerWorldPosition;
    bool isOffScreen;


    public static void setTarget(GameObject _target) {
        target = _target;
    }

    private void Awake() {
        pointer = transform.Find("Pointer").GetComponent<RectTransform>();
        target = initTarget;
    }

    void Update() {
        if (GameStateManager.isHUDVisible() && GameStateManager.isPlanetNavigationSystemActive()) {
            if (target != null) {
                pointer.gameObject.SetActive(true);
                toPosition = target.transform.position;
                fromPosition = Camera.main.transform.position;
                fromPosition.z = 0;
                direction = (toPosition - fromPosition).normalized;
                float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(direction, Vector3.up));
                Vector3 cross = Vector3.Cross(direction, Vector3.up);
                angle = -Mathf.Sign(cross.z) * angle;

                pointer.localEulerAngles = new Vector3(0, 0, angle);

                targetPositionScreenPoint = Camera.main.WorldToScreenPoint(target.transform.position);
                isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

                if (isOffScreen) {
                    Debug.Log("OffScreen");
                    targetScreenPosition = targetPositionScreenPoint;

                    if (targetScreenPosition.x <= borderSize) {
                        targetScreenPosition.x = borderSize;
                    }
                    if (targetScreenPosition.x >= Screen.width - borderSize) {
                        targetScreenPosition.x = Screen.width - borderSize;
                    }
                    if (targetScreenPosition.y <= borderSize) {
                        targetScreenPosition.y = borderSize;
                    }
                    if (targetScreenPosition.y >= Screen.height - borderSize) {
                        targetScreenPosition.y = Screen.height - borderSize;
                    }

                    pointerWorldPosition = UICamera.ScreenToWorldPoint(targetScreenPosition);
                    pointer.position = pointerWorldPosition;
                    pointer.localPosition = new Vector3(pointer.transform.localPosition.x, pointer.localPosition.y, 0);
                } else {
                    // Debug.Log("OffScreen");
                    pointerWorldPosition = Camera.main.ScreenToWorldPoint(targetPositionScreenPoint);
                    pointer.position = pointerWorldPosition;
                    pointer.localPosition = new Vector3(pointer.transform.localPosition.x, pointer.localPosition.y, 0);
                }
            }
        } else {
            pointer.gameObject.SetActive(false);
        }
    }
}
