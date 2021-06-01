using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    [SerializeField] AudioMixerSnapshot startMenu;
    [SerializeField] AudioMixerSnapshot intro;
    [SerializeField] AudioMixerSnapshot tutorial;
    [SerializeField] AudioMixerSnapshot pause;
    [SerializeField] AudioMixerSnapshot gameOver;
    [SerializeField] AudioMixerSnapshot startGame;

    [SerializeField] AudioSource introAudio;
    [SerializeField] AudioSource tutorialAudio;

    void Start() {

    }

    void Update() {
        if (GameStateManager.isStartMenu()) {
            startMenu.TransitionTo(6f);
        }

        if (GameStateManager.isIntro() && !GameStateManager.isPaused()) {
            if (!introAudio.isPlaying) {
                introAudio.Play();
            }
            intro.TransitionTo(0f);
        }

        if (GameStateManager.isTutorial()) {
            if (!tutorialAudio.isPlaying) {
                tutorialAudio.Play();
            }
            tutorial.TransitionTo(0.3f);
        } else if (!GameStateManager.isPaused()) {
            tutorialAudio.Stop();
        }


        if (GameStateManager.isPaused()) {
            pause.TransitionTo(0.3f);
        }

        if (GameStateManager.isGameover()) {
            gameOver.TransitionTo(0.4f);
        }
    }
}
