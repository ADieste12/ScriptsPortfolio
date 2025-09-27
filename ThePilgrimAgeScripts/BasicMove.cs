using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMove : MonoBehaviour
{
    [Header("Fuerzas")]
    float speed;
    public float maxSpeed;
    float moreSpeed = 10.5f;
    public float jumpingPower;
    Rigidbody2D rb;
    public bool isJumping;
    public bool isGrounded;

    [Header("Mecanicas")]
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool bendDown;

    public GameObject moveAttack;
    public Collider2D attack;
    public Collider2D downAttack;
    public Collider2D upAttack;
    bool isAttacking = true;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] GameObject cloud;
    [SerializeField] GameObject cloudPosition;
    [SerializeField] GameObject moveCloud;
    public int cloudAmount = 0;
    [SerializeField] Bounce bounce;
    public GameObject cloudExist;
    bool canCreate;

    [Header("Visual")]
    Animator anim;
    SpriteRenderer sp;

    //Grounded movida
    [Header("Ground Check")]
    public float groundCheckDistance = 0.1f;
    //public LayerMask groundLayer;
    public Vector2 groundCheckOffsetFront;
    public Vector2 groundCheckOffsetMiddle;
    public Vector2 groundCheckOffsetBack;

    [SerializeField] Controls playerInput;
    public Vector2 moveDirection = Vector2.zero;
    public bool isJumpPressed;
    GameObject clouds;
    public bool isBouncing;
    public bool isInteractPressed;
    public int keyCount;
    public GameObject finalDoor;

    [Header("Audios")]
    public AudioSource ambientMusic;
    public AudioSource bossMusic;
    public AudioClip attackAir;
    public AudioClip jump;


    public bool isPaused = false;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject key1UI;
    [SerializeField] GameObject key2UI;
    private void Awake()
    {
        playerInput = new Controls();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        anim.SetBool("DownAttack", downAttack.enabled);
        anim.SetBool("UpAttack", upAttack.enabled);
        anim.SetBool("Grounded", isGrounded);

        //ENCONTRRAR DICHOS GAMEOBJECTS PARA EVITAR INSTANCIAR MAS DE UNO
        clouds = GameObject.FindGameObjectWithTag("Cloud");
        cloudAmount = GameObject.FindGameObjectsWithTag("Cloud").Length;
        
        if (isGrounded)
        {
            isBouncing = false;
            downAttack.enabled = false;
        }
        if (!isPaused)
        {
            if (moveDirection.x < -0.2f)
                sp.flipX = true;
            else if (moveDirection.x > 0.2f)
                sp.flipX = false;
        }
        if (moveDirection.x < -0.2f && moveDirection.y >= -0.5f)
        {
            moveCloud.transform.eulerAngles = new Vector3(0f, -180f, 0f);
            moveAttack.transform.eulerAngles = new Vector3(0f, -180f, 0f);
        }
        else if (moveDirection.x > 0.2f && moveDirection.y >= -0.5f)
        {
            moveCloud.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            moveAttack.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }      
        else if (moveDirection.x > -0.7f && moveDirection.x < 0.7f && moveDirection.y < -0.2f && !isGrounded)
        {
            moveCloud.transform.eulerAngles = new Vector3(0f, 0f, -90f);
        }
        //COYOTE Y BUFFER
        if (!isGrounded)
        {
            speed = moreSpeed;
        }
        if (isBouncing)
        {
            speed = moreSpeed + 1;
        }
        if (isGrounded)
        {
            anim.SetFloat("Speed", rb.linearVelocityX);
            speed = maxSpeed;
            canCreate = true;
            coyoteTimeCounter = coyoteTime;
        }
        else if (!isGrounded && !isJumping)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        else if (!isGrounded && isJumping)
        {
            coyoteTimeCounter = 0;
        }

        if (!isGrounded && cloudAmount != 0)
        {
            canCreate = false;
        }

        if (!isJumpPressed && !isBouncing)
        {
            if (rb.linearVelocity.y > 0f)
            {
                jumpBufferCounter = jumpBufferTime;
                //ESTO HACE QUE SI SUELTAS EL BOTON DE SALTO ANTES DE LLEGAR AL MAXIMO DE FUERZA DE SALTO, EL PERSONAJE CAIGA ANTES
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }
        }

        if (jumpBufferCounter <= 0f || !isJumpPressed)
        {
            jumpBufferCounter = 0f;
        }

        if (rb.linearVelocity.y < -30f)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 7;
        }

        jumpBufferCounter -= Time.deltaTime;
        if (jumpBufferCounter <= 0f || !isJumpPressed)
        {
            jumpBufferCounter = 0f;
        }

        if (!isPaused)
        {
            Time.timeScale = 1f;
            pauseUI.SetActive(false);
        }
    }

    #region Grounded

    private bool GroundCheck()
    {
        //UN RAYCAST TRIPLE PARA SER PRECISO CON EL COLIDER
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

    private void FixedUpdate()
    {
        isGrounded = GroundCheck();
        if (isGrounded && !isJumpPressed)
            isJumping = false;
        if (moveDirection.x > 0.2f)
            moveDirection.x = 1;
        if (moveDirection.x < - 0.2f)
            moveDirection.x = -1;
        if ((isGrounded) || (!isGrounded))
        {
            if (moveDirection.x >= 0.2f || moveDirection.x <= -0.2f) 
                rb.linearVelocity = new Vector2(moveDirection.x * speed, rb.linearVelocity.y);
            else if (moveDirection.x < 0.2f || moveDirection.x > -0.2f)
                rb.linearVelocityX = 0;
        }
        else
        {
            rb.linearVelocityX = 0;
        }
    }

    //NUEVO INPUT SYSTEM
    #region Inputs
    void OnMove(InputValue value)
    {
        //RECOGER LA INCLINACION DEL JOYSTICK IZQUIERDO
        moveDirection = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isPaused)
        {
            isJumpPressed = value.isPressed;
            if (isJumpPressed && (coyoteTimeCounter > 0 || isGrounded))
            {
                SoundEffectsManager.Instance.PlaySFXClip(jump, transform, 0.5f);
                jumpBufferCounter = jumpBufferTime;
                isJumping = true;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
                }
            }
    }

    void OnAttack(InputValue value)
    {
        //AÑADIDO A TODOS UN IF ISPAUSED PORQUE O SINO LOS BOTONES SE ACTIVAN IGUAL 
        if (!isPaused)
        {
            bool isAttackPressed = value.isPressed;
            //DEPENDIENDO DE DISTINTAS CONDICIONES SE ACTIVA EL ATAQUE NORMAL, EL PARA ARRIBA O EL PARA ABAJO
            if (((isAttacking && !isGrounded && moveDirection.y >= -0.5f && moveDirection.y <= 0.5f) || isAttacking && isGrounded && moveDirection.y <= 0.5f))
            {
                anim.SetBool("Attack", true);
                SoundEffectsManager.Instance.PlaySFXClip(attackAir, transform, 0.2f);
                StartCoroutine(AttackDuration());
                StartCoroutine(AttackCooldown());
            }
            else if ((isAttacking && moveDirection.x > -0.5f && moveDirection.x < 0.5f && moveDirection.y < -0.5f && !isGrounded))
            {
                downAttack.enabled = true;
                SoundEffectsManager.Instance.PlaySFXClip(attackAir, transform, 0.2f);
                StartCoroutine(AttackCooldown());
            }
            else if ((isAttacking && moveDirection.x > -0.5f && moveDirection.x < 0.5f && moveDirection.y > 0.5f))
            {
                upAttack.enabled = true;
                SoundEffectsManager.Instance.PlaySFXClip(attackAir, transform, 0.2f);
                StartCoroutine(AttackDuration());
                StartCoroutine(AttackCooldown());
            }
        }
    }

    void OnCloud(InputValue value)
    {
        //CON ESTE INPUT INSTANCIAS NUBES QUE SIRVEN COMO TRAMPOLINES
        if (cloudAmount < 1 && canCreate && !isPaused)
        {
            Instantiate(cloud, cloudPosition.transform.position, Quaternion.identity);
        }
        else if (cloudAmount >= 1 && canCreate && !isPaused)
        {
            Destroy(clouds);
            Instantiate(cloud, cloudPosition.transform.position, Quaternion.identity);
        }
    }

    void OnInteract(InputValue value)
    {
        if (!isPaused)
        {
            isInteractPressed = value.isPressed;
            if (isInteractPressed)
                StartCoroutine(StopInteract());
        }
    }

    void OnPause(InputValue value)
    {
        bool isPausePressed = value.isPressed;
        if (isPausePressed && !isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
            pauseUI.SetActive(true);
        }
        else if ((isPausePressed && isPaused))
        {
            Time.timeScale = 1f;
            isPaused = false;
            pauseUI.SetActive(false);
        }
    }
    #endregion

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(0.15f);
        attack.enabled = true;
        yield return new WaitForSeconds(0.4f);
        upAttack.enabled = false;
        attack.enabled = false;
        anim.SetBool("Attack", false);
    }

    IEnumerator AttackCooldown()
    {
        //UN COOLDOWN PARA QUE EL ATAQUE NO SE PUEDA HACER SIEMPRE
        isAttacking = false;
        yield return new WaitForSeconds(0.6f);
        isAttacking = true;
    }

    IEnumerator StopInteract()
    {
        yield return new WaitForEndOfFrame();
        isInteractPressed = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cloud"))
        {
            isBouncing = true;
        }
        if (other.CompareTag("Key1"))
        {
            keyCount++;
            Destroy(other.gameObject);
            key1UI.SetActive(true);
        }
        if (other.CompareTag("Key2"))
        {
            keyCount++;
            Destroy(other.gameObject);
            key2UI.SetActive(true);
        }
        if (other.CompareTag("Hill"))
        {
            anim.SetBool("isSliding", true);
        }
        if (other.CompareTag("FinalDoor") && keyCount >= 2)
        {
            finalDoor.SetActive(false);
        }
        if (other.CompareTag("FinalBossActive"))
        {
            finalDoor.SetActive(true);
            ambientMusic.enabled = false;
            bossMusic.enabled = true;
        }
        if (other.CompareTag("ActivateMusic"))
        {
            ambientMusic.enabled = true;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hill"))
        {
            anim.SetBool("isSliding", false);
        }
    }
}
