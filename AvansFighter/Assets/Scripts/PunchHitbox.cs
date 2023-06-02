using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(90); // Assuming the player's punch deals 10 damage
            }
        }
    }
}
