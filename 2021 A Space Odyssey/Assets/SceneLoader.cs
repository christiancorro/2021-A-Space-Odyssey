using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour {

    private static float transitionDelay = 0.5f;
    private static Animator transition;

    public static SceneLoader instance;

    private void Awake() {
        if (!instance) {
            instance = this;
            transition = GetComponent<Animator>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void LoadGameScene() {
        LoadScene("Game Scene");
    }

    public static void LoadMainScene() {
        LoadScene("Main Scene");
    }

    public static void LoadScene(string sceneName) {
        transition.SetBool("show", true);
        instance.StartCoroutine(TransitionTo(sceneName));
    }

    public static IEnumerator TransitionTo(string sceneName) {
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitForSeconds(transitionDelay);
        transition.SetBool("show", false);
    }

}
