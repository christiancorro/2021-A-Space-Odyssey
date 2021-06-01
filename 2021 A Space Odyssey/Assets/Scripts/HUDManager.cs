using System;
using System.Globalization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    [SerializeField] Animator HUDAnimator;
    public TMPro.TextMeshProUGUI distanceText;
    public Slider fuelBar, healthBar, oxygenBar;

    private System.Globalization.CultureInfo customCulture;

    private void Start() {
        customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
    }

    void Update() {
        if (GameStateManager.isInGame() && GameStateManager.isHUDVisible()) {
            GameStateManager.ShowHUD();
            HUDAnimator.SetBool("showHUD", true);
        } else {
            HUDAnimator.SetBool("showHUD", false);
        }

        fuelBar.value = Mathf.Lerp(fuelBar.value, Starship.fuel, Time.deltaTime * 6);
        oxygenBar.value = Mathf.Lerp(oxygenBar.value, Starship.oxygen, Time.deltaTime * 6);
        healthBar.value = Mathf.Lerp(healthBar.value, Starship.health, Time.deltaTime * 6);

        distanceText.text = String.Format("{0:0.00}", Starship.distance) + "";
    }

}
