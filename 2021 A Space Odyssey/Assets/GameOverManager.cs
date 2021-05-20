using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    [SerializeField] Animator gameOverAnimator;
    [SerializeField] TypeWriter gameOverWriter;
    [SerializeField] Sentences gameOverSentence;

    private bool writing = false;

    void Update() {
        if (!writing && GameStateManager.isGameover()) {
            writing = true;
            gameOverAnimator.SetBool("showIntro", true);
            StartCoroutine(StartWriting());

        }
    }

    IEnumerator StartWriting() {
        yield return new WaitForSecondsRealtime(2);
        gameOverWriter.Write(gameOverSentence);
        yield return null;
    }

    public void CloseGameOver() {
        Debug.Log("Restart");
        gameOverAnimator.SetBool("showIntro", false);
        // GameStateManager.StartMenu();
        GameStateManager.Restart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
