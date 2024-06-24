using UnityEngine;

public class Antorcha : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetBool("isLit", true); 
        }
    }
}

