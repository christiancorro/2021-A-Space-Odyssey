using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playlist : MonoBehaviour {

    // Plays random music

    [SerializeField] AudioClip[] music;
    [SerializeField] AudioSource audioMusic;
    private bool playListStarted = false;

    private void Update() {
        if (!playListStarted && GameStateManager.startPlayList()) {
            Debug.Log("Playlist");
            audioMusic.clip = music[Random.Range(0, music.Length)];
            audioMusic.Play();
            audioMusic.loop = true;
            playListStarted = true;
        }

        if (GameStateManager.isInit()) {
            playListStarted = false;
        }
    }

    void PlayNextSong() {
        audioMusic.clip = music[Random.Range(0, music.Length)];
        audioMusic.Play();
        Invoke("PlayNextSong", audioMusic.clip.length);
    }
}
