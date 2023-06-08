using System.Collections;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private AudioSource hitSoundEffect;
    private Animator animator;

    private bool isHitAnimationPlaying = false;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(animator.GetInteger("state") != 2)
        {
            switch (collider.tag)
            {
                case "PunchHitBox":
                    Debug.Log("Hit by punch!");
                    hitSoundEffect.Play();
                    hitSoundEffect.Play();
                    animator.SetInteger("state", 8);
                    isHitAnimationPlaying = true;
                    TakeDamage(10);
                    StartCoroutine(TransitionToIdle());
                    break;
                case "CrouchPunchHitbox":
                    Debug.Log("Hit by crouch punch!");
                    hitSoundEffect.Play();
                    hitSoundEffect.Play();
                    animator.SetInteger("state", 8);
                    isHitAnimationPlaying = true;
                    TakeDamage(10);
                    StartCoroutine(TransitionToIdle());
                    break;
                case "JumpKickHitbox":
                    hitSoundEffect.Play();
                    animator.SetInteger("state", 8);
                    Debug.Log("Hit by jump kick!");
                    isHitAnimationPlaying = true;
                    TakeDamage(30);
                    StartCoroutine(TransitionToIdle());
                    break;
                case "KickHitbox":
                    Debug.Log("Hit by kick!");
                    animator.SetInteger("state", 8);
                    hitSoundEffect.Play();
                    isHitAnimationPlaying = true;
                    TakeDamage(20);
                    StartCoroutine(TransitionToIdle());
                    break;
            }
        }
       
    }

    private IEnumerator TransitionToIdle()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed
        animator.SetInteger("state", 0); // Transition back to idle state
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add your desired logic for enemy death
        Debug.Log("Died!");
    }
}
