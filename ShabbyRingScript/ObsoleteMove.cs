using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem;

public class ObsoleteMove : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed;
    public float maxSpeed;
    public float jumpingPower;
    public float realSpeed = 7;

    private bool isJumping;
    private bool isGrounded;

    [SerializeField] GameObject grabDetecter;
    [SerializeField] GameObject objectHolder;
    [SerializeField] GameObject mover;

    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] float stopSliding;
    private bool bendDown;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sp;
    [SerializeField] SpriteRenderer brazos;
    [SerializeField] Animator armAnim;

    //Grounded movida
    [Header("Ground Check")]
    public float groundCheckDistance = 0.1f;
    //public LayerMask groundLayer;
    public Vector2 groundCheckOffsetFront;
    public Vector2 groundCheckOffsetBack;

    public bool isGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //MOVIMIENTOS BASICOS

        anim.SetFloat("Speed", rb.linearVelocityX);
        armAnim.SetFloat("Speed", rb.linearVelocityX);
        anim.SetFloat("AirSpeed", rb.linearVelocity.y);
        armAnim.SetFloat("AirSpeed", rb.linearVelocity.y);

        if (horizontal < 0 && !bendDown)
        {
            sp.flipX = true;
            brazos.flipX = true;
            mover.transform.eulerAngles = new Vector2(0f, -180f);
        }
        else if (horizontal > 0 && !bendDown)
        {
            sp.flipX = false;
            brazos.flipX = false;
            mover.transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if (horizontal < 0 && bendDown && !isGrounded)
        {
            sp.flipX = true;
            brazos.flipX = true;
        }
        else if (horizontal > 0 && bendDown && !isGrounded)
        {
            sp.flipX = false;
            brazos.flipX = false;
        }

        //AGACHARSE

        vertical = Input.GetAxisRaw("Vertical");

        if (vertical < -0.5f)
        {
            anim.SetBool("Bend", true);
            armAnim.SetBool("Bend", true);
            bendDown = true;

        }
        else if (vertical >= -0.5f)
        {
            anim.SetBool("Bend", false);
            armAnim.SetBool("Bend", false);
            bendDown = false;
        }
        
        //ISGROUNDED

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            anim.SetBool("Ground", true);
            armAnim.SetBool("Ground", true);
            isGrounded = true;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            anim.SetBool("Ground", false);
            armAnim.SetBool("Ground", false);
            isGrounded = false;
        }

        //GROUNDED

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            anim.SetTrigger("Jump");
            armAnim.SetTrigger("Jump");
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //COYOTE Y BUFFER

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            jumpBufferCounter = 0f;
            StartCoroutine("JumpCooldown");
        }

        //JUMP

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (!bendDown || isJumping)
        {
            speed = realSpeed;
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
        else if (bendDown && rb.linearVelocityX != 0 && !isJumping)
        {
            if (rb.linearVelocityX > 0)
            {
                rb.linearVelocityX  -= (Mathf.Sign(speed)) * (maxSpeed / stopSliding) * Time.fixedDeltaTime;
                if (rb.linearVelocityX <= 0)
                    {
                        rb.linearVelocityX = 0;
                    }
            }
            else if (rb.linearVelocityX < 0)
            {
                    rb.linearVelocityX += (Mathf.Sign(speed)) * (maxSpeed / stopSliding) * Time.fixedDeltaTime;
                    if (rb.linearVelocityX >= 0)
                    {
                        rb.linearVelocityX = 0;
                    }
                }
            }
    }
    private bool IsGrounded()
    {
        //return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        Vector2 originFront = (Vector2)transform.position + groundCheckOffsetFront;
        Vector2 originBack = (Vector2)transform.position + groundCheckOffsetBack;

        RaycastHit2D hitFront = Physics2D.Raycast(originFront, Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D hitBack = Physics2D.Raycast(originBack, Vector2.down, groundCheckDistance, groundLayer);

        Debug.DrawRay(originFront, Vector2.down * groundCheckDistance, hitFront.collider ? Color.green : Color.red);
        Debug.DrawRay(originBack, Vector2.down * groundCheckDistance, hitBack.collider ? Color.green : Color.red);

        return hitFront.collider != null || hitBack.collider != null;
    }
  

    IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
}