using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleArea : MonoBehaviour {

    [SerializeField] Area area;
    [SerializeField] bool activate;

    // ----------------    -------------
    //       trigger pass trough detection
    // ----------------    -------------

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Starship") {
            Debug.DrawLine(new Vector3(transform.position.x - 300, transform.position.y, 0), new Vector3(transform.position.x + 300, transform.position.y, 0), Color.green, 2);

            area.isActive = activate;
            area.ToggleChildren();

            Debug.Log("Area " + area.name + (area.isActive ? " activated" : " deactivated"));
            if (activate) {
                AreaManager.SetCurrentArea(area);
            }
        }
    }
}
