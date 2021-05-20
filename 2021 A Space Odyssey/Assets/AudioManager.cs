using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    [SerializeField] AudioMixerSnapshot startMenu;
    [SerializeField] AudioMixerSnapshot intro;
    [SerializeField] AudioMixerSnapshot tutorial;
    [SerializeField] AudioMixerSnapshot pause;
    [SerializeField] AudioMixerSnapshot gameOver;

    [SerializeField] AudioSource introAudio;
    [SerializeField] AudioSource tutorialAudio;


    void Start() {

    }

    void Update() {
        if (GameStateManager.isStartMenu()) {
            startMenu.TransitionTo(0.2f);
        }

        if (GameStateManager.isIntro()) {
            if (!introAudio.isPlaying) {
                introAudio.Play();
            }
            intro.TransitionTo(0f);
        }

        if (GameStateManager.isTutorial()) {
            if (!tutorialAudio.isPlaying) {
                tutorialAudio.Play();
            }
            tutorial.TransitionTo(2f);
        }

        if (GameStateManager.isPaused()) {
            pause.TransitionTo(0.2f);
        }
    }
}
