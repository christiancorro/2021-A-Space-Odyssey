
using UnityEngine;
using UnityEngine.EventSystems; // 1

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {
    [SerializeField] MenuButtonController menuButtonController;
    Animator animator;
    // [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;

    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (menuButtonController.index == thisIndex) {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1) {
                animator.SetBool("pressed", true);
            } else if (animator.GetBool("pressed")) {
                animator.SetBool("pressed", false);
                //  animatorFunctions.disableOnce = true;
            }
        } else {
            animator.SetBool("selected", false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        menuButtonController.index = thisIndex;
    }

    public void OnPointerClick(PointerEventData eventData) {
        print("I was clicked");
        animator.SetBool("pressed", true);
        menuButtonController.index = thisIndex;
    }
}
