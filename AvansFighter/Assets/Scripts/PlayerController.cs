using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 1; // player left right walk speed
    private bool isGrounded = true; // is player on the ground?

    private Animator animator;

    private const int STATE_IDLE = 0;
    private const int STATE_WALK = 1;
    private const int STATE_CROUCH = 2;
    private const int STATE_JUMP = 3;
    private const int STATE_HADOOKEN = 4;
    private const int STATE_PUNCH = 5;

    private bool isPlayingCrouch = false;
    private bool isPlayingWalk = false;
    private bool isPlayingHadooken = false;

    private string currentDirection = "right";
    private int currentAnimationState = STATE_IDLE;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("j") )
        {
            ChangeState(STATE_PUNCH);
        }
        else if (Input.GetKey("h") && !isPlayingWalk)
        {
            ChangeState(STATE_HADOOKEN);
        }
        else if (Input.GetKey("w") && !isPlayingHadooken && !isPlayingCrouch)
        {
            if (isGrounded)
            {
                isGrounded = false;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 350));
                ChangeState(STATE_JUMP);
            }
        }
        else if (Input.GetKey("s") && !isPlayingWalk)
        {
            ChangeState(STATE_CROUCH);
        }
        else if (Input.GetKey("d") && !isPlayingHadooken)
        {
            ChangeDirection("right");
            transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);

            if (isGrounded)
                ChangeState(STATE_WALK);
        }
        else if (Input.GetKey("a") && !isPlayingHadooken)
        {
            ChangeDirection("left");
            transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);

            if (isGrounded)
                ChangeState(STATE_WALK);
        }
        else
        {
            if (isGrounded)
                ChangeState(STATE_IDLE);
        }

        // Check if crouch animation is playing
        isPlayingCrouch = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Crouch");

        // Check if hadooken animation is playing
        isPlayingHadooken = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Hadooken");

        // Check if walk animation is playing
        isPlayingWalk = animator.GetCurrentAnimatorStateInfo(0).IsName("Ken-Walk");
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
        if (coll.gameObject.name == "Floor")
        {
            isGrounded = true;
            ChangeState(STATE_IDLE);
        }
    }

    private void ChangeDirection(string direction)
    {
        if (currentDirection != direction)
        {
            if (direction == "right")
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                currentDirection = "right";
            }
            else if (direction == "left")
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                currentDirection = "left";
            }
        }
    }
}
