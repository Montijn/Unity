using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource kickSoundEffect;
    [SerializeField] private AudioSource punchSoundEffect;
    [SerializeField] private AudioSource jumpKickSoundEffect;
    [SerializeField] private AudioSource jumpSoundEffect;
    private GameObject enemy;
    private Animator animator;
    public float walkSpeed = 1;
    private bool isGrounded = true;
    private bool isPlayingCrouch = false;
    private bool isPlayingWalk = false;
    private bool isPlayingPunch = false;
    private bool isPlayingHit = false;
    private bool isCrouching = false;

    private const int STATE_IDLE = 0;
    private const int STATE_WALK = 1;
    private const int STATE_CROUCH = 2;
    private const int STATE_JUMP = 3;
    private const int STATE_CROUCH_PUNCH = 4;
    private const int STATE_PUNCH = 5;
    private const int STATE_JUMP_KICK = 6;
    private const int STATE_KICK = 7;
    private int currentAnimationState = STATE_IDLE;

    private int comboCounter = 0;


    private void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        ChangeState(STATE_IDLE);
        ChangeDirection(enemy.transform.position.x - transform.position.x);
    }

    private void FixedUpdate()
    {
        isCrouching = Input.GetKey("s");

        if (enemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Ryu-Hit"))
        {
            comboCounter++;
            Debug.Log(comboCounter);
        }
        if(animator.GetInteger("state") == 8)
        {
            comboCounter = 0;
        }
        if (!isPlayingHit)
        {
            if (isCrouching && Input.GetKey("j"))
            {
                ChangeState(STATE_CROUCH_PUNCH);
                punchSoundEffect.Play();
            }
            else if (Input.GetKey("k") && comboCounter == 75)
            {
                if (isGrounded)
                {
                    isGrounded = false;
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 190));
                    ChangeState(STATE_JUMP_KICK);
                    jumpKickSoundEffect.Play();
                }

            }
            else if (Input.GetKey("j"))
            {
                ChangeState(STATE_PUNCH);
                punchSoundEffect.Play();
                if (!punchSoundEffect.isPlaying)
                {
                    punchSoundEffect.Play();
                }


            }
            else if (Input.GetKey("l"))
            {
                ChangeState(STATE_KICK);
                kickSoundEffect.Play();
            }
            else if (Input.GetKey("w") && !isPlayingPunch && !isPlayingCrouch)
            {
                if (isGrounded)
                {
                    isGrounded = false;
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250));
                    ChangeState(STATE_JUMP);
                    jumpSoundEffect.Play();
                }
            }
            else if (isCrouching && !isPlayingWalk && !isPlayingPunch)
            {
                ChangeState(STATE_CROUCH);
            }
            else if (Input.GetKey("d") && !isPlayingPunch)
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
            else if (Input.GetKey("a") && !isPlayingPunch)
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

        }

        isPlayingCrouch = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Crouch");
        isPlayingPunch = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Punch");
        isPlayingWalk = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Walk");
        isPlayingHit = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Hit");
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
}
