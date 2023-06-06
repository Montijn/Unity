using UnityEngine;
public class HitDetection : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "PunchHitBox":
                Debug.Log("Hit by punch!");
                TakeDamage(10);
                break;
            case "CrouchPunchHitbox":
                Debug.Log("Hit by crouch punch!");
                TakeDamage(10);
                break;
            case "JumpKickHitbox":
                Debug.Log("Hit by jump kick!");
                TakeDamage(30);
                break;
            case "KickHitbox":
                Debug.Log("Hit by kick!");
                TakeDamage(10);
                break;
        }
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
