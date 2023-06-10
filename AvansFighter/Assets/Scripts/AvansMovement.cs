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

            Vector3 targetPosition = enemy.transform.position;
            targetPosition.y = transform.position.y; 
            directionToEnemy = targetPosition - transform.position;
            directionToEnemy.Normalize();
        }
    }

    private void Update()
    {
        if (enemy != null)
        {

            Vector3 newPosition = transform.position + directionToEnemy * moveSpeed * Time.deltaTime;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
        }
    }
}
