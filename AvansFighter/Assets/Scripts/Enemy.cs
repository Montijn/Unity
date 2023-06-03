using UnityEngine;
public class Enemy : MonoBehaviour
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
                Debug.Log("Enemy hit by punch!");
                TakeDamage(10);
                break;
            case "CrouchPunchHitbox":
                Debug.Log("Enemy hit by crouch punch!");
                TakeDamage(10);
                break;
            case "JumpKickHitbox":
                Debug.Log("Enemy hit by jump kick!");
                TakeDamage(30);
                break;
            case "KickHitbox":
                Debug.Log("Enemy hit by kick!");
                TakeDamage(10);
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add your desired logic for enemy death
        Debug.Log("Enemy died!");
    }
}
