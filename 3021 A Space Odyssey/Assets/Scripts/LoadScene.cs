using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : StateMachineBehaviour {
    public string sceneName;

    // Scene loading management directly on FSM

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (SceneManager.GetActiveScene().name != sceneName) {
            SceneLoader.LoadScene(sceneName);
        }
    }
}
