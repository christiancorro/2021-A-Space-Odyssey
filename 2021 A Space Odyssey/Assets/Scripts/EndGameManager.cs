using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class EndGameManager : MonoBehaviour {

    [SerializeField] FollowPlayer cam;
    [SerializeField] TypeWriter endStoryWriter;
    [SerializeField] Sentences endStory_text;
    [SerializeField] TypeWriter title;
    [SerializeField] TypeWriter credits;
    [SerializeField] Sentences credits_text;
    [SerializeField] AudioMixerSnapshot endGameSnapshot;
    [SerializeField] AudioMixerSnapshot pauseSnapshot;

    [SerializeField] GameObject postProcessingObject;

    private VolumeProfile volume;
    private Vignette vignette; //The post processing effect you want to change

    void Start() {
        endGameSnapshot.TransitionTo(3f);
        volume = postProcessingObject.GetComponent<Volume>()?.profile;
        StartCoroutine(StartEndGame());
    }

    void Update() {

    }

    IEnumerator StartEndGame() {
        yield return new WaitForSeconds(1f);
        cam.SetDistance(130);
        endStoryWriter.Write(endStory_text);
    }


    public void StartCredits() {
        credits.Write(credits_text);
    }

    public void EndCredits() {
        title.Clean();
        credits.Clean();
        pauseSnapshot.TransitionTo(10f);
        StartCoroutine(EndCreditsAnimation());
    }



    private IEnumerator EndCreditsAnimation() {
        float time = 4;
        float elapsedTime = 0;

        volume.TryGet(out vignette); //Grab the value of the post processing effect

        while (elapsedTime < time) {
            vignette.intensity.Override(Mathf.Lerp(((float)vignette.intensity), 1, (elapsedTime / time))); //override the effect that you want with the value you need.
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GameStateManager.StartMenu();
        yield return new WaitForSeconds(0.1f);
        SceneLoader.LoadMainScene();
    }
}
