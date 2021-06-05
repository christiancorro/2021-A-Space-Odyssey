using UnityEngine;

[System.Serializable]
public class Parameter {
    public string name;
    public bool value;
}

public class SetParameterOnStateEnter : StateMachineBehaviour {

    [SerializeField]
    Parameter[] parametersToChange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        foreach (Parameter parameter in parametersToChange) {
            animator.SetBool(parameter.name, parameter.value);
            // if (parameter.value) {
            //     animator.SetTrigger(parameter.name);
            // } else {
            //     animator.ResetTrigger(parameter.name);
            // }
        }
    }
}
