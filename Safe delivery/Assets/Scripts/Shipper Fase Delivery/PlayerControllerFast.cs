using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFast : MonoBehaviour
{
    [Header("Basic Controller")]
    public GameObject ShipperHolder;
    // Rigidbody 2D of the Player
    private Rigidbody2D rb;
    // Movement speed
    [Range(10, 200)]
    public float speed;
    // Movement vector
    private float moveHorizontal;
    // Jump force
    [Range(10, 200)]
    public float jumpForce;
    // Check to flip the player
    private bool facingRight = true;
    // The groundcheck
    public Transform groundCheck;
    // The groundcheck (bool)
    private bool isGrounded;
    // The Radius to overlap
    public float radius;
    public float radiusFront;
    // The layermask to be overlaped
    public LayerMask whatIsGround;
    // Extra jump value : used to identify the maximum jump times in the air
    public int extraJumpValue;
    // Get the animator 
    private Animator anim;
    // Check to touching the wall at front side of the player
    private bool isTouchingFront;
    // Get the front check
    public Transform frontCheck;
    // Check if the player is sliding wall or not
    private bool slidingWall;
    // slide spedd
    [Range(10, 200)]
    public float slideWallSpeed;
    // wall jump
    private bool wallJumping;
    // x force to wall jump
    public float xWallJumpForce;
    // y force to wall jump
    public float yWallJumpForce;
    // Time during jumping wall
    public float timeWallJumping;
    // Check to spawn the dust at the player's feet
    private bool isSpawnDust;
    // The dust
    public GameObject dust;
    // extra jump
    private int extraJump;
    [Range(0, 100)]
    public float maxEnergy;
    public HealthBar energyBar;

    [Header("Dash")]
    private bool isDashing;
    [Range(0, 10000)]
    public float dashPower;

    [Header("Super Jump")]
    public GameObject beforeSuperJumpEffect;
    public GameObject afterSuperJumpEffect;
    private float timeToUse;
    private float timeToUseValue;
    private bool isEnableToSuperJump;

    // Start is called before the first frame update
    void Start()
    {
        ShareVariablesFast.MAX_ENERGY = maxEnergy;
        ShareVariablesFast.CURRENT_ENERGY = maxEnergy;
        ShareVariablesFast.IS_USING_ABILITY = false;
        energyBar.SetMaxValue(maxEnergy);
        isDashing = false;
        isEnableToSuperJump = false;
        beforeSuperJumpEffect.SetActive(false);
        afterSuperJumpEffect.SetActive(false);
        extraJump = extraJumpValue;
        timeToUseValue = 1f;
        timeToUse = timeToUseValue;
        rb = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) && isGrounded == true && ShareVariablesFast.CURRENT_ENERGY >= 50)
        {
            Shared.IS_ENABLE_TO_SWITCH = false;
            if (timeToUse > 0 && !isEnableToSuperJump)
            {
                beforeSuperJumpEffect.SetActive(false);
                timeToUse -= Time.deltaTime;
            }
            else
            {
                timeToUse = timeToUseValue;
                beforeSuperJumpEffect.SetActive(true);
                isEnableToSuperJump = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && isGrounded == true)
        {
            ShareVariablesFast.IS_USING_ABILITY = true;
            Shared.IS_ENABLE_TO_SWITCH = true;
            if (isEnableToSuperJump == true)
            {
                rb.velocity = Vector2.up * jumpForce * 3;
                StartCoroutine(SpawnSuperJumpEffect());
                timeToUse = timeToUseValue;
                beforeSuperJumpEffect.SetActive(false);
                isEnableToSuperJump = false;
            }
            else
            {
                beforeSuperJumpEffect.SetActive(false);
            }
        }
        if (ShareVariablesFast.IS_USING_ABILITY == true)
        {
            ShareVariablesFast.CURRENT_ENERGY -= 50;
            Mathf.Clamp(ShareVariablesFast.CURRENT_ENERGY, 0, ShareVariablesFast.MAX_ENERGY);
            energyBar.SetValue(ShareVariablesFast.CURRENT_ENERGY);
            ShareVariablesFast.IS_USING_ABILITY = false;
        }
        ShareVariablesFast.CURRENT_ENERGY += Time.deltaTime * 3;
        Mathf.Clamp(ShareVariablesFast.CURRENT_ENERGY, 0, 100);
        energyBar.SetValue(ShareVariablesFast.CURRENT_ENERGY);
        if (Input.GetKeyDown(KeyCode.F) && isDashing == false && ShareVariablesFast.CURRENT_ENERGY >= 20)
        {
            StartCoroutine(Dash());
        }
        if (isGrounded == true)
        {
            extraJump = extraJumpValue;
        }
        else
        {
            beforeSuperJumpEffect.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            Instantiate(dust, groundCheck.position, Quaternion.identity);
            rb.velocity = Vector2.up * jumpForce;
            StartCoroutine(JumpAnimation());
            extraJump--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJump == 0 && isGrounded == true)
        {
            Instantiate(dust, groundCheck.position, Quaternion.identity);
            rb.velocity = Vector2.up * jumpForce;
            StartCoroutine(JumpAnimation());
        }

        if (Input.GetKeyDown(KeyCode.Space) && slidingWall == true)
        {
            wallJumping = true;
            Instantiate(dust, frontCheck.position, Quaternion.identity);
            Invoke("SetWallJumpingToFalse", timeWallJumping);
        }

        if (isGrounded == true)
        {
            if (isSpawnDust == true)
            {
                GameObject dst = Instantiate(dust, groundCheck.position, Quaternion.identity);
                Destroy(dst, 2f);
                isSpawnDust = false;
            }
        }
        else
        {
            isSpawnDust = true;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);

        // Move left-right mechanism
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (isDashing == false)
            rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

        Vector3 Scaler = transform.localScale;
        if (moveHorizontal > 0)
            Scaler.x = 1;
        else if (moveHorizontal < 0)
            Scaler.x = -1;
        transform.localScale = Scaler;

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, radiusFront, whatIsGround);
        if (isTouchingFront == true && moveHorizontal != 0)
        {
            slidingWall = true;
        }
        else
        {
            slidingWall = false;
        }
        if (slidingWall == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, -slideWallSpeed);
        }
        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallJumpForce * -moveHorizontal, yWallJumpForce);
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(frontCheck.position, radiusFront);
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }

    IEnumerator JumpAnimation()
    {
        anim.SetBool("isJump", true);
        yield return new WaitForSecondsRealtime(0.3f);
        anim.SetBool("isJump", false);
    }

    IEnumerator Dash()
    {
        ShareVariablesFast.CURRENT_ENERGY -= 10;
        Mathf.Clamp(ShareVariablesFast.CURRENT_ENERGY, 0, ShareVariablesFast.MAX_ENERGY);
        energyBar.SetValue(ShareVariablesFast.CURRENT_ENERGY);
        isDashing = true;
        int dir = 0;
        if (moveHorizontal > 0)
            dir = 1;
        else if (moveHorizontal < 0)
            dir = -1;
        transform.GetComponent<Rigidbody2D>().AddForce(dir * Vector2.right * dashPower * 100, ForceMode2D.Force);
        yield return new WaitForSecondsRealtime(0.2f);
        isDashing = false;
    }

    IEnumerator SpawnSuperJumpEffect()
    {
        afterSuperJumpEffect.SetActive(true);
        yield return new WaitForSecondsRealtime(0.8f);
        afterSuperJumpEffect.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Platform"))
        {
            ShipperHolder.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Platform"))
        {
            ShipperHolder.transform.parent = null;
        }
    }
}
