using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float distanceOffset = 1f;
    [SerializeField] private float heightOffset = 0.5f;
    [SerializeField] private GameObject avans;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioManager audioManager;

    public float walkSpeed = 1;
    private bool isGrounded = true;
    private bool isPlayingCrouch = false;
    private bool isPlayingWalk = false;
    private bool isPlayingPunch = false;
    private bool isPlayingHit = false;
    private bool isPlayingKick = false;
    private bool isPlayingJumpKick = false;
    private bool isPlayingMove = false;
    private bool isPlayingSpecial = false;
    private bool isCrouching = false;

    private const int STATE_IDLE = 0;
    private const int STATE_WALK = 1;
    private const int STATE_CROUCH = 2;
    private const int STATE_JUMP = 3;
    private const int STATE_CROUCH_PUNCH = 4;
    private const int STATE_PUNCH = 5;
    private const int STATE_JUMP_KICK = 6;
    private const int STATE_KICK = 7;
    private const int STATE_SPECIAL = 9;
    private const int STATE_CHEAT = 10;
    private int currentAnimationState = STATE_IDLE;

    private int comboCounter = 0;
    private bool previousHitState = false;
    private bool hasSpawnedAvans = false;

    

    private void Start()
    {
        ChangeState(STATE_IDLE);
        ChangeDirection(enemy.transform.position.x - transform.position.x);
    }

    private void Update()
    {
        isCrouching = Input.GetKey("s");
        bool currentHitState = enemy.GetComponent<HitDetection>().IsHit;

        if (currentHitState && !previousHitState && !isPlayingHit)
        {
            // Enemy has been hit (transition from false to true)
            comboCounter++;
            Debug.Log("Combo Counter: " + comboCounter);
        }

        if (!currentHitState && isPlayingHit)
        {
            // Player has been hit
            comboCounter = 0;
        }

        previousHitState = currentHitState;


        if(isPlayingPunch || isPlayingKick || isPlayingJumpKick || isPlayingSpecial)
        {
            isPlayingMove = true;
        }
        else
        {
            isPlayingMove = false;
        }

        if (!isPlayingHit)
        {
            if (isCrouching && Input.GetKey("j"))
            {
                ChangeState(STATE_CROUCH_PUNCH);
                audioManager.PlayPunchSound();
            }
            else if (Input.GetKey("k"))
            {
                if (isGrounded)
                {
                    isGrounded = false;
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 190));
                    ChangeState(STATE_JUMP_KICK);
                    audioManager.PlayJumpKickSound();
                }
            }
            else if (Input.GetKey("j"))
            {
                ChangeState(STATE_PUNCH);
                audioManager.PlayPunchSound();
            }
            else if (Input.GetKey("i") && comboCounter >= 3)
            {
                ChangeState(STATE_SPECIAL);
            }
            else if (Input.GetKey("c") && !isPlayingMove)
            {
                ChangeState(STATE_CHEAT);
            }
            else if (Input.GetKey("l"))
            {
                ChangeState(STATE_KICK);
                audioManager.PlayKickSound();
            }
            else if (Input.GetKey("w") && !isPlayingMove && !isPlayingCrouch)
            {
                if (isGrounded)
                {
                    isGrounded = false;
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250));
                    ChangeState(STATE_JUMP);
                    audioManager.PlayJumpSound();
                }
            }
            else if (isCrouching && !isPlayingWalk && !isPlayingMove)
            {
                ChangeState(STATE_CROUCH);
            }
            else if (Input.GetKey("d") && !isPlayingMove)
            {
                ChangeDirection(enemy.transform.position.x - transform.position.x);
                if (enemy.transform.position.x - transform.position.x > 0)
                {
                    transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
                }
                if (isGrounded)
                    ChangeState(STATE_WALK);
            }
            else if (Input.GetKey("a") && !isPlayingMove)
            {
                ChangeDirection(enemy.transform.position.x - transform.position.x);
                if (enemy.transform.position.x - transform.position.x > 0)
                {
                    transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
                }
                if (isGrounded)
                    ChangeState(STATE_WALK);
            }
            else
            {
                if (isGrounded)
                    ChangeState(STATE_IDLE);
            }
            if (currentAnimationState == STATE_SPECIAL && !hasSpawnedAvans)
            {
                SpawnAvans();
            }

        }

        isPlayingCrouch = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Crouch");
        isPlayingPunch = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Punch");
        isPlayingWalk = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Walk");
        isPlayingHit = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Hit");
        isPlayingKick = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Kick");
        isPlayingJumpKick = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-JumpKick");
        isPlayingSpecial = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Special");
    }

    private void ChangeState(int state)
    {
        if (currentAnimationState == state)
            return;

        animator.SetInteger("state", state);
        currentAnimationState = state;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Foreground")
        {
            isGrounded = true;
            ChangeState(STATE_IDLE);
        }
    }

    private void ChangeDirection(float direction)
    {
        if (direction > 0)
        {
            // Enemy is to the right of the player
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            // Enemy is to the left of the player
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void SpawnAvans()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime >= 1 && !stateInfo.loop)
        {
            Vector3 directionToEnemy = enemy.transform.position - transform.position;

            Vector3 spawnPosition = transform.position + directionToEnemy.normalized * distanceOffset;

            spawnPosition.y += heightOffset;

            Instantiate(avans, spawnPosition, Quaternion.identity);

            hasSpawnedAvans = true;
        }
    }

  
}
