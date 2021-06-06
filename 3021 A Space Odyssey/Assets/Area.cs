using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour {

    [SerializeField] public bool isActive;

    void Start() {
        isActive = AreaManager.isCurrentArea(this);
        ToggleChildren();
    }

    public void ToggleChildren() {
        StartCoroutine(Toggle());
    }

    IEnumerator Toggle() {
        foreach (Transform child in transform)
            child.gameObject.SetActive(isActive);
        yield return null;
    }
}
