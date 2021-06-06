using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour {

    // Dont destroy object. If restart the current area is visible 

    public static AreaManager instance;
    private static string currentArea;

    private void Awake() {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Current area comparasion based on area name
    public static bool isCurrentArea(Area area) {
        if (currentArea != null) {
            Debug.Log("Current Area: " + currentArea + "\nArea: " + area.name);
            return currentArea == area.name; // currentArea could be null
        } else {
            Debug.Log("Current Area NULL");
        }
        return false;
    }

    public static void SetCurrentArea(Area area) {
        Debug.Log("Current area: " + area.name);
        currentArea = area.name;
    }

}
