using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float walkSpeed = 0.5f;
    public float walkDuration = 1;
    public float idleDuration = 2f;
    private Animator animator;
    private const int STATE_IDLE = 0;
    private const int STATE_WALK = 1;
    private const int STATE_CROUCH_PUNCH = 4;
    private const int STATE_PUNCH = 5;
    private const int STATE_KICK = 7;

    private int currentAnimationState = STATE_IDLE;
    public GameObject player;
    private bool isMoving = false; // Flag to determine if the enemy is currently moving
    private float moveTimer = 0f; // Timer for controlling movement
    private float idleTimer = 0f; // Timer for controlling idle duration
    private bool isAttacking;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Ryu-Punch") || animator.GetCurrentAnimatorStateInfo(0).IsName("Ryu-Kick");
        ChangeState(STATE_IDLE);
        
        ChangeDirection(player.transform.position.x - transform.position.x);

        // Start with an initial idle duration
        idleTimer = Random.Range(1f, 4f);
    }

    private void Update()
    {
        ChangeDirection(player.transform.position.x - transform.position.x);

        if (!isMoving && !isAttacking)
        {
            idleTimer -= Time.deltaTime;

            if (idleTimer <= 0f)
            {
                idleTimer = Random.Range(1f, 4f);
                isMoving = true;
                moveTimer = walkDuration;

                ChangeState(STATE_WALK);
            }
        }
        else
        {
            moveTimer -= Time.deltaTime;

            if (moveTimer <= 0f)
            {
                isMoving = false;
                ChangeState(STATE_IDLE);
                PerformRandomAttack();
                moveTimer = 0f;
            }
        }

        if (isMoving)
        {
                transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
        }
    }
    private void ChangeState(int state)
    {
        if (currentAnimationState == state)
            return;

        animator.SetInteger("state", state);
        currentAnimationState = state;
    }

    private void ChangeDirection(float direction)
    {
        if (direction > 0)
        {
            // Player is to the right of the enemy
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            // Player is to the left of the enemy
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void PerformRandomAttack()
    {
        // Generate a random number to determine the attack action
        int randomAction = Random.Range(0, 3);

        switch (randomAction)
        {
            case 0:
                ChangeState(STATE_PUNCH);
                break;
            case 1:
                ChangeState(STATE_KICK);
                break;
            case 2:
                ChangeState(STATE_CROUCH_PUNCH);
                break;
            default:
                ChangeState(STATE_IDLE);
                break;
        }

    }

}
