using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenuManager : MonoBehaviour {

    [SerializeField] AudioMixer mixer;
    [SerializeField] Texture2D cursorIcon;
    [SerializeField] AudioSource highlightSound;
    [SerializeField]
    Button resumeButton, settingsFirstButton, settingsClosedButton,
            controlsFirstButton, controlsClosedButton;

    private Animator panelAnimator;
    private bool paused = false;



    void Awake() {
        panelAnimator = GetComponent<Animator>();
        Cursor.SetCursor(cursorIcon, Vector2.zero, CursorMode.Auto);
    }

    private void Update() {
        if (GameStateManager.isPaused()) {
            if (Input.GetButtonDown("Back")) {
                if (panelAnimator.GetBool("settings")) {
                    CloseSettingsPanel();
                } else if (panelAnimator.GetBool("controls")) {
                    CloseControlsPanel();
                }
            }
        }
    }

    public void OpenPauseMenu() {
        if (!paused) {
            Debug.Log(EventSystem.current.currentSelectedGameObject);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
            resumeButton.OnSelect(null);
            Time.timeScale = 0f;
            paused = true;
            panelAnimator.SetBool("show", true);
        }
    }

    public void ClosePauseMenu() {
        if (paused) {
            Time.timeScale = 1;
            paused = false;
            panelAnimator.SetBool("show", false);
            panelAnimator.SetBool("settings", false);
            panelAnimator.SetBool("controls", false);
        }
    }

    public void Restart() {
        Debug.Log("Restart");
        GameStateManager.TogglePause();
        ClosePauseMenu();
        GameStateManager.Restart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void Resume() {
        Debug.Log("Resume");
        GameStateManager.TogglePause();
        ClosePauseMenu();
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

    public void Exit() {
        GameStateManager.ExitGame();
        SceneLoader.LoadMainScene();
    }

    public void SetMusicVolume(float sliderValue) {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20); // convert to dB
    }

    public void SetSFXVolume(float sliderValue) {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20); // convert to dB
    }

}
