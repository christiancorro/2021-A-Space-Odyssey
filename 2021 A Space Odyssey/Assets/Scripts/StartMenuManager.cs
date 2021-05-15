using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System;

public class StartMenuManager : MonoBehaviour {

    [SerializeField] AudioMixer mixer;
    [SerializeField] Texture2D cursorIcon;
    private Animator panelAnimator;


    void Start() {
        panelAnimator = GetComponent<Animator>();
        Cursor.SetCursor(cursorIcon, Vector2.zero, CursorMode.Auto);
    }

    void Update() {

    }

    public void OpenSettingsPanel() {
        panelAnimator.SetBool("settings", true);
    }

    public void CloseSettingsPanel() {
        panelAnimator.SetBool("settings", false);
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
