using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playlist : MonoBehaviour {

    [SerializeField] AudioClip[] music;
    [SerializeField] AudioSource audioMusic;
    private bool playListStarted = true;

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
