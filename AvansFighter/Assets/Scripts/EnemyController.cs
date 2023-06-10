using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private AudioSource kickSoundEffect;
    [SerializeField] private AudioSource punchSoundEffect;
    [SerializeField] private Animator animator;
    [SerializeField] public GameObject player;

    public float walkSpeed = 0.5f;
    public float walkDuration = 1;
    public float idleDuration = 2f;
    
    private const int STATE_IDLE = 0;
    private const int STATE_WALK = 1;
    private const int STATE_PUNCH = 5;
    private const int STATE_KICK = 7;

    private int currentAnimationState = STATE_IDLE;

    private bool isMoving = false;
    private float moveTimer = 0f; 
    private float idleTimer = 0f; 
    private bool isAttacking;
    private bool isPlayingHit = false;

    private void Start()
    {
        
        ChangeState(STATE_IDLE);
        
        ChangeDirection(player.transform.position.x - transform.position.x);

        idleTimer = Random.Range(1f, 4f);
    }

    private void Update()
    {
        isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Ryu-Punch") || animator.GetCurrentAnimatorStateInfo(0).IsName("Ryu-Kick");
        ChangeDirection(player.transform.position.x - transform.position.x);

        if (!isPlayingHit)
        {
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
        isPlayingHit = animator.GetCurrentAnimatorStateInfo(0).IsName("Ryu-Hit");
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
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void PerformRandomAttack()
    {
        int randomAction = Random.Range(0, 2);

        switch (randomAction)
        {
            case 0:
                ChangeState(STATE_PUNCH);
                punchSoundEffect.Play();
                punchSoundEffect.Play();
                StartCoroutine(TransitionToIdle());
                break;
            case 1:
                ChangeState(STATE_KICK);
                kickSoundEffect.Play();
                StartCoroutine(TransitionToIdle());
                break;
            default:
                ChangeState(STATE_IDLE);
                break;
        }

    }
    private IEnumerator TransitionToIdle()
    {
        yield return new WaitForSeconds(0.3f); 
        animator.SetInteger("state", 0); 
    }



}
