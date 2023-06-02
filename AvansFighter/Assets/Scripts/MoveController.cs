using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D hitboxCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        hitboxCollider = GetComponent<BoxCollider2D>();
        hitboxCollider.enabled = false; // Disable the hitbox initially
    }

    public void ExecuteMove()
    {
        // Trigger the move animation
        animator.SetTrigger("MoveTrigger");

        // Enable the hitbox
        hitboxCollider.enabled = true;

        // Disable the hitbox after the move animation has finished
        StartCoroutine(DisableHitboxAfterAnimation());
    }

    private IEnumerator DisableHitboxAfterAnimation()
    {
        // Wait until the current move animation has finished playing
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // Disable the hitbox
        hitboxCollider.enabled = false;
    }
}
