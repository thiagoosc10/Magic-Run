using UnityEngine;

public class AntorchaAzul : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetBool("isLitBlue", true); 
        }
    }
}

