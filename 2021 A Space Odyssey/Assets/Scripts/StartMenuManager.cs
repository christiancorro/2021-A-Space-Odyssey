using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;
using System;

public class StartMenuManager : MonoBehaviour {

    [SerializeField] AudioMixer mixer;
    [SerializeField] Texture2D cursorIcon;
    [SerializeField] AudioSource highlightSound;
    [SerializeField] Button settingsFirstButton, settingsClosedButton;

    private Animator panelAnimator;


    void Start() {
        panelAnimator = GetComponent<Animator>();
        Cursor.SetCursor(cursorIcon, Vector2.zero, CursorMode.Auto);
    }

    void Update() {

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
