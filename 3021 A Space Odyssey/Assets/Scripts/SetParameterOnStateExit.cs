using UnityEngine;


public class SetParameterOnStateExit : StateMachineBehaviour {

    [SerializeField]
    Parameter[] parametersToChange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        foreach (Parameter parameter in parametersToChange) {
            animator.SetBool(parameter.name, parameter.value);
            if (!parameter.value) {
                animator.ResetTrigger(parameter.name);
            }
        }
    }
}
