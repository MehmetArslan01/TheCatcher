using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float forwardMovement = Input.GetAxis("Vertical");

        if (forwardMovement > 0.0f)
        {
            animator.SetBool("IsMovingForward", true);
        }
        else
        {
            animator.SetBool("IsMovingForward", false);
        }
    }
}
