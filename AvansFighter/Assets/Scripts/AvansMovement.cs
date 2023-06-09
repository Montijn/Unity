using UnityEngine;

public class AvansMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the movement speed as needed

    private GameObject enemy;
    private Vector3 directionToEnemy;

    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy != null)
        {
            // Calculate the initial direction from the "avans" object to the enemy
            Vector3 targetPosition = enemy.transform.position;
            targetPosition.y = transform.position.y; // Keep the same y position as the "avans" object
            directionToEnemy = targetPosition - transform.position;
            directionToEnemy.Normalize();
        }
    }

    private void Update()
    {
        if (enemy != null)
        {
            // Move the "avans" object towards the enemy in the precalculated direction
            Vector3 newPosition = transform.position + directionToEnemy * moveSpeed * Time.deltaTime;
            newPosition.y = transform.position.y; // Keep the same y position as the "avans" object
            transform.position = newPosition;
        }
    }
}
