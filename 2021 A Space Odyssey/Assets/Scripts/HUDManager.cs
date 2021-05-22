using System;
using System.Globalization;
using System.Collections;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    [SerializeField] Animator HUDAnimator;
    public TMPro.TextMeshProUGUI fuelText, oxygenText, distanceText, healthText;

    private System.Globalization.CultureInfo customCulture;

    private void Start() {
        customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
    }

    void Update() {
        if (GameStateManager.isInGame()) {
            HUDAnimator.SetBool("showHUD", true);
        } else {
            HUDAnimator.SetBool("showHUD", false);
        }
        fuelText.text = Mathf.Ceil(Starship.fuel) + "%";
        oxygenText.text = Mathf.Ceil(Starship.oxygen) + "%";
        healthText.text = Mathf.Ceil(Starship.health) + "%";
        distanceText.text = String.Format("{0:0.00}", Starship.distance / 1000) + "";
    }

}
