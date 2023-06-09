using System.Collections;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private AudioSource hitSoundEffect;
    [SerializeField] private Animator animator;


    private bool isHitAnimationPlaying = false;
    public bool IsHit { get; private set; }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(animator.GetInteger("state") != 2)
        {
            switch (collider.tag)
            {
                case "PunchHitBox":
                    Hit(hitSoundEffect, 10);
                    StartCoroutine(TransitionToIdle());
                    break;
                case "CrouchPunchHitbox":
                    Hit(hitSoundEffect, 10);
                    StartCoroutine(TransitionToIdle());
                    break;
                case "JumpKickHitbox":
                    Hit(hitSoundEffect, 10);
                    StartCoroutine(TransitionToIdle());
                    break;
                case "KickHitbox":
                    Hit(hitSoundEffect, 10);
                    StartCoroutine(TransitionToIdle());
                    break;
                case "AvansHitbox":
                    Hit(hitSoundEffect, 50);
                    Destroy(collider.gameObject);
                    StartCoroutine(TransitionToIdle());
                    break;
            }
        }
       
    }

    private void Hit(AudioSource audioSource, int damage)
    {
        audioSource.Play();
        animator.SetInteger("state", 8);
        isHitAnimationPlaying = true;
        TakeDamage(damage);
        IsHit = true;
    }

    private IEnumerator TransitionToIdle()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed
        animator.SetInteger("state", 0); // Transition back to idle state
        IsHit = false;
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
