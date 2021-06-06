using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemiesManager : MonoBehaviour {

    // Manage danger level counting enemies spawned, useful for audio transition

    private static int counter;
    private static bool extremeDanger;
    private static bool danger;


    [SerializeField] int numberOfEnemies;
    [SerializeField] AudioMixerSnapshot defaultSnapshot;

    private void Start() {
        extremeDanger = false;
        counter = 0;
    }

    private void Update() {
        numberOfEnemies = counter;

        if (counter > 0 && !extremeDanger) {
            danger = true;
        }

        if (counter == 0 && (extremeDanger || danger)) {
            StopDanger();
        }

    }

    private void StopDanger() {
        extremeDanger = false;
        danger = false;
        defaultSnapshot.TransitionTo(5f);
    }

    public static void setExtremeDanger() {
        danger = false;
        extremeDanger = true;
    }

    public static bool isExtremeDanger() {
        return extremeDanger;
    }

    public static bool isDanger() {
        return danger;
    }

    public static int getEnemiesCounter() {
        return counter;
    }

    public static void increaseEnemyCounter() {
        Debug.Log("+1 " + counter);
        counter++;
    }

    public static void decreaseEnemyCounter() {
        Debug.Log("-1 " + counter);
        counter--;
    }
}
