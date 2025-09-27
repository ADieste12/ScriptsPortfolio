using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class BasicMove : MonoBehaviour
{
    [Header("Fuerzas")]
    private float speed;
    public float maxSpeed;
    public float jumpingPower;
    public float realSpeed;

    private bool isGrounded;

    [Header("Agarrar")]
    [SerializeField] GameObject grabDetecter;
    [SerializeField] GameObject objectHolder;
    [SerializeField] GameObject mover;
    [SerializeField] GameObject codo;

    [Header("Movement Improvements")]
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] float stopSliding;
    private bool bendDown;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Visual")]
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sp;
    [SerializeField] SpriteRenderer brazos;
    [SerializeField] Animator armAnim;
    public Animator itemBrazosAnim;
    public GameObject objectItemBrazos;


    //Grounded movida
    [Header("Ground Check")]
    public float groundCheckDistance = 0.1f;
    //public LayerMask groundLayer;
    public Vector2 groundCheckOffsetFront;
    public Vector2 groundCheckOffsetMiddle;
    public Vector2 groundCheckOffsetBack;

    [SerializeField] Controls playerInput;
    Vector2 moveDirection = Vector2.zero;
    bool isJumpPressed;
    int limitAttack;

    public ObjectFeedback stuned;
    bool isStuned;
    bool isHitted;
    public GrabObjects gO;
    public SpriteRenderer anotherSp;
    public GameObject anotherPlayer;

    public int numeroJugador;
    AudioManager audioManager;

    private void Awake()
    {
        playerInput = new Controls();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //ANIMACIONES
        anim.SetFloat("Speed", rb.linearVelocityX);
        anim.SetFloat("AirSpeed", rb.linearVelocity.y);

        if (moveDirection.x <= -0.3f && !bendDown && !isStuned)
        {
            sp.flipX = true;
            brazos.flipX = true;
            anotherSp.flipX = true;

            mover.transform.eulerAngles = new Vector2(0f, -180f);
            if(objectItemBrazos != null) objectItemBrazos.transform.eulerAngles = new Vector2(0f, -180f);
            codo.transform.eulerAngles = new Vector2(0f, -180f);
        }
        else if (moveDirection.x >= 0.3f && !bendDown && !isStuned)
        {
            sp.flipX = false;
            brazos.flipX = false;
            anotherSp.flipX = false;

            mover.transform.eulerAngles = new Vector2(0f, 0f);
            if (objectItemBrazos != null) objectItemBrazos.transform.eulerAngles = new Vector2(0f, 0f);
            codo.transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if (moveDirection.x <= -0.3 && bendDown && !isGrounded && !isStuned)
        {
            sp.flipX = true;
            brazos.flipX = true;
            anotherSp.flipX = true;

            mover.transform.eulerAngles = new Vector2(0f, -180f);
            if (objectItemBrazos != null) objectItemBrazos.transform.eulerAngles = new Vector2(0f, -180f);
            codo.transform.eulerAngles = new Vector2(0f, -180f);
        }
        else if (moveDirection.x >= 0.3 && bendDown && !isGrounded && !isStuned)
        {
            sp.flipX = false;
            brazos.flipX = false;
            anotherSp.flipX = false;

            mover.transform.eulerAngles = new Vector2(0f, 0f);
            if (objectItemBrazos != null) objectItemBrazos.transform.eulerAngles = new Vector2(0f, 0f);
            codo.transform.eulerAngles = new Vector2(0f, 0f);
        }

        if (stuned.isHitted)
        {
            anim.SetBool("Hitted", true);
        }
        else
        {
            anim.SetBool("Hitted", false);
        }

        if (armAnim.isActiveAndEnabled) { armAnim.SetFloat("Speed", rb.linearVelocityX); }
        if (armAnim.isActiveAndEnabled) { armAnim.SetFloat("AirSpeed", rb.linearVelocity.y); }

        if (moveDirection.y < -0.5f && gO.playerGrabbing == false)
        {
            anim.SetBool("Bend", true);
            if (armAnim.isActiveAndEnabled) { armAnim.SetBool("Bend", true); }
            if (itemBrazosAnim != null) { itemBrazosAnim.SetBool("Bend", true); }
            bendDown = true;

        }
        else if (moveDirection.y >= -0.5f)
        {
            anim.SetBool("Bend", false);
            if (armAnim.isActiveAndEnabled) { armAnim.SetBool("Bend", false); }
            if (itemBrazosAnim != null) { itemBrazosAnim.SetBool("Bend", false); }
            bendDown = false;
        }

        if (gO.playerGrabbing == true)
        {
            anotherPlayer.SetActive(true);
            armAnim.SetBool("IsHolding", true);
        }
        else
        {
            anotherPlayer.SetActive(false);
            armAnim.SetBool("IsHolding", false);
        }

        if (itemBrazosAnim != null)
        {
            itemBrazosAnim.SetFloat("Speed", rb.linearVelocityX);
            itemBrazosAnim.SetFloat("AirSpeed", rb.linearVelocity.y);

        }

        //COYOTE & BUFFER
        if (!isGrounded && moveDirection.y < -0.5f && rb.linearVelocity.y < 0 && gO.playerGrabbing == false)
        {
            codo.SetActive(true);
        }
        else
        {
            codo.SetActive(false);
        }
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            anim.SetBool("Ground", true);
            //audioManager.PlaySFX(audioManager.caer);
            if (armAnim.isActiveAndEnabled) { armAnim.SetBool("Ground", true); }
            if (itemBrazosAnim != null) { itemBrazosAnim.SetBool("Ground", true); }
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            anim.SetBool("Ground", false);
            if (armAnim.isActiveAndEnabled) { armAnim.SetBool("Ground", false); }
            if (itemBrazosAnim != null) { itemBrazosAnim.SetBool("Ground", false); }
        }

        if (!isJumpPressed)
        {

            if (rb.linearVelocity.y > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);

            }
        }

        if (rb.linearVelocity.y > 0f)
        {
            //coyoteTimeCounter = 0f;
        }

        jumpBufferCounter -= Time.deltaTime;
        if (jumpBufferCounter <= 0f || !isJumpPressed)
        {
            jumpBufferCounter = 0f;
        }

        if (stuned.stun <= 0f)
        {
            isStuned = true;
            anim.SetBool("Stuned", true);
            if (armAnim.isActiveAndEnabled) { armAnim.SetBool("Stuned", true); }
            if (itemBrazosAnim != null) { itemBrazosAnim.SetBool("Stuned", true); }
        }
        else
        {
            isStuned = false;
            anim.SetBool("Stuned", false);
            if (armAnim.isActiveAndEnabled) { armAnim.SetBool("Stuned", false); }
            if (itemBrazosAnim != null) { itemBrazosAnim.SetBool("Stuned", false); }
        }
    }

    #region Grounded
    private bool GroundCheck()
    {
        //return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        Vector2 originFront = (Vector2)transform.position + groundCheckOffsetFront;
        Vector2 originMiddle = (Vector2)transform.position + groundCheckOffsetMiddle;
        Vector2 originBack = (Vector2)transform.position + groundCheckOffsetBack;

        RaycastHit2D hitFront = Physics2D.Raycast(originFront, Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D hitMiddle = Physics2D.Raycast(originMiddle, Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D hitBack = Physics2D.Raycast(originBack, Vector2.down, groundCheckDistance, groundLayer);

        Debug.DrawRay(originFront, Vector2.down * groundCheckDistance, hitFront.collider ? Color.green : Color.red);
        Debug.DrawRay(originMiddle, Vector2.down * groundCheckDistance, hitMiddle.collider ? Color.green : Color.red);
        Debug.DrawRay(originBack, Vector2.down * groundCheckDistance, hitBack.collider ? Color.green : Color.red);

        return hitFront.collider != null || hitMiddle.collider != null || hitBack.collider != null;
    }
    #endregion

    void FixedUpdate()
    {
        isGrounded = GroundCheck();

        //horizontal = Input.GetAxisRaw("Horizontal");     
        if ((isGrounded && !bendDown) || (!isGrounded))
        {
            if (!isStuned)
                speed = realSpeed;
            else
                speed = 0;

            rb.linearVelocity = new Vector2(moveDirection.x * speed, rb.linearVelocity.y);
        }
        else 
        {
            if (rb.linearVelocityX > 0)
            {
                rb.linearVelocityX -= (Mathf.Sign(speed)) * (speed / stopSliding) * Time.fixedDeltaTime;
                if (rb.linearVelocityX <= 0)
                {
                    rb.linearVelocityX = 0;
                }
            }
            else if (rb.linearVelocityX < 0)
            {
                rb.linearVelocityX += (Mathf.Sign(speed)) * (speed / stopSliding) * Time.fixedDeltaTime;
                if (rb.linearVelocityX >= 0)
                {
                    rb.linearVelocityX = 0;
                }
            }
        }
    }

    #region Inputs
    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        isJumpPressed = value.isPressed;
        if ((isJumpPressed && (coyoteTimeCounter > 0 || isGrounded)) && (!isStuned))          
        {
            jumpBufferCounter = jumpBufferTime; 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            anim.SetTrigger("Jump");
            if (armAnim.isActiveAndEnabled) { armAnim.SetTrigger("Jump"); }
            if (itemBrazosAnim != null) { itemBrazosAnim.SetTrigger("Jump"); }
            audioManager.PlaySFX(audioManager.salto);
        }
    }

    void OnAttack(InputValue value)
    {
        if (!isStuned && !bendDown && gO.playerGrabbing == false)
        {
            bool isAttackPressed = value.isPressed;
            if (itemBrazosAnim != null && isAttackPressed && limitAttack == 0)
            {
                objectItemBrazos.GetComponent<ObjetoAgarrado>().Attack();
                float cooldown = objectItemBrazos.GetComponent<ObjetoAgarrado>().cooldown;
                if (itemBrazosAnim != null) { itemBrazosAnim.SetBool("Attack", true); audioManager.PlaySFX(audioManager.objetoFallo); }
                limitAttack++;
                StartCoroutine(Cooldown(cooldown));
            }
        }
    }

    IEnumerator Cooldown(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        if (itemBrazosAnim != null) { itemBrazosAnim.SetBool("Attack", false);}
        limitAttack = 0;
    }
    #endregion
}