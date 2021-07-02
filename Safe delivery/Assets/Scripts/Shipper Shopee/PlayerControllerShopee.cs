using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerShopee: MonoBehaviour
{
    [Header("Basic Controller")]
    public GameObject ShipperHolder;
    // Rigidbody 2D of the Player
    private Rigidbody2D rb;
    // Movement speed
    [Range(10, 400)]
    public float speed;
    // Movement vector
    private float moveHorizontal;
    // Jump force
    [Range(10, 400)]
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
    [Range(10, 20000)]
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
    [SerializeField]
    public HealthBar energyBar;

   
    // Start is called before the first frame update
    void Start()
    {
        ShareVariables.MAX_ENERGY = maxEnergy;
        ShareVariables.CURRENT_ENERGY = maxEnergy;
        ShareVariables.IS_USING_ABILITY = false;
        extraJump = extraJumpValue;
        rb = transform.GetComponent<Rigidbody2D>();
        anim = transform.GetComponent<Animator>();
        energyBar.SetMaxValue(ShareVariables.MAX_ENERGY);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShareVariables.IS_USING_ABILITY == true)
        {
            ShareVariables.CURRENT_ENERGY -= 20;
            Mathf.Clamp(ShareVariables.CURRENT_ENERGY, 0, ShareVariables.MAX_ENERGY);
            energyBar.SetValue(ShareVariables.CURRENT_ENERGY);
            ShareVariables.IS_USING_ABILITY = false;
        }
        ShareVariables.CURRENT_ENERGY += Time.deltaTime * 3;
        Mathf.Clamp(ShareVariables.CURRENT_ENERGY, 0, 100);
        energyBar.SetValue(ShareVariables.CURRENT_ENERGY);
        if (isGrounded == true)
        {
            //Debug.Log("Grounded Shopee");
            ModifyRotationAfterTransforming();
            extraJump = extraJumpValue;
            ShareVariables.ENABLE_LAUNCH = true;
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
            Instantiate(dust, frontCheck.position, Quaternion.identity);
            wallJumping = true;
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
        if (ShareVariables.IS_BASHING == false)
        {
            rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        }

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

    void ModifyRotationAfterTransforming()
    {
        foreach (Transform item in transform)
        {
            item.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    IEnumerator JumpAnimation()
    {
        anim.SetBool("isJump", true);
        yield return new WaitForSecondsRealtime(0.3f);
        anim.SetBool("isJump", false);
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
