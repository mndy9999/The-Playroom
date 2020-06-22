using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Transform playerSprite;

    private bool mLocked;
    public bool PlayerLocked
    {
        get
        {
            return mLocked || CinematicsManager.Instance.CinematicsStarted;
        }
        set
        {
            if (value != mLocked)
            {

                mLocked = value;
            }
        }
    }

    private bool mRespawn;
    public bool PlayerRespawn
    {
        set
        {
            if (mRespawn != value)
            {
                mRespawn = value;
                PlayerLocked = value;
                if (value)
                    playerRb.velocity = playerRb.velocity.y != 0 ? Vector2.up * jumpSpeed : Vector2.zero;
            }
        }
    }

    public bool PlayerHit
    {
        set
        {
            PlayerLocked = value;
        }
    }

    private PlayerInteractionController interactionController;

    private bool mClimbing;
    private float gravityScale;

    public bool Climbing
    {
        get
        {
            return mClimbing;
        }
        set
        {
            if (PlayerLocked)
                return;
            mClimbing = value;
            playerRb.gravityScale = value ? 0 : gravityScale;
            playerAnim.SetBool("climbing", value);
            if (value)
            {
                if (facingRight)
                    Flip();
                var interactible = interactionController.interactable;
                if (interactible != null)
                {
                    var pos = interactible.transform.position;
                    transform.position = new Vector3(pos.x, transform.position.y);
                    jumpingRight = facingRight;
                }
            }
            else
            {
                playerAnim.SetFloat("climb_speed", 0);
            }
        }
    }

    private Rigidbody2D playerRb;
    private Animator playerAnim;

    public bool mIsGrounded;
    public bool IsGrounded
    {
        get
        {
            return mIsGrounded;
        }
        set
        {
            if(value != mIsGrounded)
            {
                mIsGrounded = value;
                if (mIsGrounded)
                {
                    //Debug.Log("Reset jumps");
                    extraJumps = extraJumpsVal;
                    IsOnLeftWall = false;
                    IsOnRightWall = false;
                    leftCheck.gameObject.SetActive(true);
                    leftCheck.gameObject.SetActive(true);
                    playerAnim.SetBool("jumping", false);
                }
            }
        }
    }
    public bool mIsOnLeftWall;
    public bool mIsOnRightWall;

    private bool wallJumpLeft;
    private bool wallJumpRight;
    private bool IsOnLeftWall
    {
        set
        {
            if (mIsOnLeftWall != value)
            {
                mIsOnLeftWall = value;
                wallJumpLeft = value;
                leftCheck.gameObject.SetActive(!mIsOnLeftWall);               
            }
        }
    }
    private bool IsOnRightWall
    {
        set
        {
            if (value != mIsOnRightWall)
            {
                mIsOnRightWall = value;
                wallJumpRight = value;
                rightCheck.gameObject.SetActive(!mIsOnRightWall);

            }
        }
    }

    public float runSpeed = 5.0f;
    public float jumpSpeed = 5.0f;

    public Transform groundCheck;
    public Transform leftCheck;
    public Transform rightCheck;

    public float checkRadius;

    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private int extraJumpsVal = 1;

    [SerializeField]
    private int extraJumps;

    private bool facingRight;
    private bool jumpingRight;

    // Start is called before the first frame update
    void Awake()
    {
        extraJumps = extraJumpsVal;
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponentInChildren<Animator>();
        interactionController = GetComponentInChildren<PlayerInteractionController>();
        gravityScale = playerRb.gravityScale;
    }

    private void HandleClimbing()
    {
        var verticalInput = Input.GetAxisRaw("Vertical");

        //set climb speed and direction to play the right climb animation
        playerAnim.SetFloat("climb_speed", Mathf.Abs(verticalInput));
        playerAnim.SetFloat("climb_direction", horizontalInput);

        //set velocity to go up/down
        playerRb.velocity = new Vector2(0, verticalInput * runSpeed);

        //remember facing direction
        if (!jumpingRight && horizontalInput < 0 || jumpingRight && horizontalInput > 0)
            jumpingRight = !jumpingRight;
    }

    private void HandleWalking()
    {
        //set speed to start/stop walk animation
        playerAnim.SetFloat("speed", Mathf.Abs(horizontalInput));

        //add velocity to move
        playerRb.velocity = new Vector2(horizontalInput * runSpeed, playerRb.velocity.y);

        //invert sprite rendere to flip in walking direction
        if (!facingRight && horizontalInput < 0 || facingRight && horizontalInput > 0)
            Flip();
    }

    private void HandleJumping()
    {
        if (Climbing)
        {
            Climbing = false;
            if (facingRight != jumpingRight)
                Flip();
            playerRb.velocity = new Vector2(facingRight ? 1 : -1, 1) * jumpSpeed;
            AudioManager.Instance.Play("Jump");
        }
        else if (wallJumpLeft)
        {
            wallJumpLeft = false;
            IsGrounded = false;
            playerRb.velocity = Vector2.up * jumpSpeed;
            AudioManager.Instance.Play("Jump");
        }
        else if (wallJumpRight)
        {
            wallJumpRight = false;
            IsGrounded = false;
            playerRb.velocity = Vector2.up * jumpSpeed;
            AudioManager.Instance.Play("Jump");
        }
        else if (IsGrounded)
        {
            IsGrounded = false;
            playerRb.velocity = Vector2.up * jumpSpeed;
            AudioManager.Instance.Play("Jump");
        }
        else if (extraJumps > 0)
        {
            IsGrounded = false;
            playerRb.velocity = Vector2.up * jumpSpeed;
            extraJumps--;
            AudioManager.Instance.Play("Jump");
        }
    }

    private void Flip()
    {
        //switch facing direction
        facingRight = !facingRight;

        //rescale on x axis to flip the sprite renderer
        Vector3 scale = playerSprite.localScale;
        scale.x *= -1;
        playerSprite.localScale = scale;
    }

    private float horizontalInput;

    private void Update()
    {
        if (PlayerLocked)
            return;
            

        horizontalInput = Input.GetAxisRaw("Horizontal");

        //update animator
        playerAnim.SetBool("jumping", !IsGrounded);
        playerAnim.SetFloat("jump_direction", Mathf.Abs(horizontalInput));

        if (Input.GetKeyDown(KeyCode.Space))
            HandleJumping();
    }

    private void FixedUpdate()
    {
        if (PlayerLocked)
            return;

        //check if it's currently on a wall to stop the player from climbing on it without jumping off
        IsOnLeftWall = Physics2D.OverlapCircle(leftCheck.position, checkRadius, wallLayer);
        IsOnRightWall = Physics2D.OverlapCircle(rightCheck.position, checkRadius, wallLayer);

        if (Climbing)
            HandleClimbing();
        else
            HandleWalking();

    }

    public void ResetPlayerValues()
    {
        PlayerLocked = false;
        PlayerHit = false;
        Climbing = false;
        playerAnim.SetBool("jumping", false);
        playerAnim.SetBool("spinning", false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftCheck.position, checkRadius);
        Gizmos.DrawWireSphere(rightCheck.position, checkRadius);

    }
}


