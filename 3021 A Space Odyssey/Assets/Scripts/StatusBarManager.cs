using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarManager : MonoBehaviour {

    [SerializeField] Image fillImage;
    [SerializeField] Gradient colorGradient;
    [Range(0.0f, 1.0f)]
    [SerializeField] float transparency = 0.8f;
    [Range(0.0f, 100f)]
    [SerializeField] float dangerValue = 20f;

    [Range(0.1f, 2)]
    [SerializeField] float alertAnimationDuration = 0.8f;

    private Slider statusBar;
    private Color newColor;

    private void Start() {
        statusBar = GetComponent<Slider>();
    }

    void Update() {
        newColor = colorGradient.Evaluate(statusBar.value / 100);

        if (statusBar.value >= dangerValue) {
            newColor.a = transparency;
        } else {
            float lerp = Mathf.PingPong(Time.time, alertAnimationDuration) / alertAnimationDuration;
            float alpha = Mathf.Lerp(0f, 1.0f, Mathf.SmoothStep(0f, 1.0f, lerp));
            newColor.a = alpha;
        }
        fillImage.color = newColor;
    }
}
