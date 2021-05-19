using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;

public class StartMenuManager : MonoBehaviour {

    [SerializeField] AudioMixer mixer;
    [SerializeField] Texture2D cursorIcon;
    [SerializeField] AudioSource highlightSound;
    [SerializeField]
    Button settingsFirstButton, settingsClosedButton,
            controlsFirstButton, controlsClosedButton,
            creditsFirstButton, creditsClosedButton;

    private Animator panelAnimator;


    void Awake() {
        panelAnimator = GetComponent<Animator>();
        Cursor.SetCursor(cursorIcon, Vector2.zero, CursorMode.Auto);
    }

    private void Update() {
        if (GameStateManager.isPaused() || GameStateManager.isStartMenu()) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void NewGame() {
        Debug.Log("New Game");
        GameStateManager.NewGame();
        panelAnimator.SetBool("pause", false);
    }

    public void OpenSettingsPanel() {
        panelAnimator.SetBool("settings", true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton.gameObject);
        settingsFirstButton.OnSelect(null);
    }

    public void CloseSettingsPanel() {
        panelAnimator.SetBool("settings", false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsClosedButton.gameObject);
        settingsClosedButton.OnSelect(null);
    }

    public void OpenControlsPanel() {
        panelAnimator.SetBool("controls", true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsFirstButton.gameObject);
        controlsFirstButton.OnSelect(null);
    }

    public void CloseControlsPanel() {
        panelAnimator.SetBool("controls", false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsClosedButton.gameObject);
        controlsClosedButton.OnSelect(null);
    }


    public void OpenCreditsPanel() {
        panelAnimator.SetBool("credits", true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsFirstButton.gameObject);
        creditsFirstButton.OnSelect(null);
    }

    public void CloseCreditsPanel() {
        panelAnimator.SetBool("credits", false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsClosedButton.gameObject);
        creditsClosedButton.OnSelect(null);
    }

    public void Quit() {
        Application.Quit();
    }

    public void SetMusicVolume(float sliderValue) {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20); // convert to dB
    }

    public void SetSFXVolume(float sliderValue) {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20); // convert to dB
    }

}
