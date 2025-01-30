using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // --- COMPONENTES ---
    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sp;

    // --- MOVIMIENTO ---
    [Header("Movimiento")]
    [SerializeField] private float speed;
    [SerializeField] private float threshold;
    private float horizontalInput;

    // --- SALTO ---
    [Header("Salto")]
    private bool isGrounded;
    [SerializeField] private GameObject GroundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce;

    [Header("Coyote Time")]
    [SerializeField] private bool coyoteEnable = true;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimer;

    [Header("Jump Queue")]
    [SerializeField] private bool queueEnable = true;
    [SerializeField] private float queueTime = 0.2f;
    private float queueTimer;

    [Header("Ataque")]
    [SerializeField] private float attackCd = 1.1f;
    private float attackCdTimer;
    [SerializeField] private int attackDmg = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.transform.position, 0.1f, groundLayer); //Posición en la que se va a dibujar el círculo.
        Movement();
        JumpLogic();
        AttackLogic();
    }

    void Movement()
    {
        //Procesar input
        float horizontal = ApplyDeadzone(horizontalInput, threshold);

        //Movimiento
        horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        //Sprite
        if(horizontal != 0)
        {
            sp.flipX = horizontal < 0;
        }

        //Animación
        bool isMoving = horizontal != 0;
        anim.SetBool("IsRunning", isMoving);
    }

    float ApplyDeadzone(float input, float threshold)
    {
        return Mathf.Abs(input) > threshold ? input : 0f;
    }

    void JumpLogic()
    {
        // Actualizar animación de salto
        anim.SetBool("IsJumping", !isGrounded);

        // Manejo de Coyote Time
        if (isGrounded)
        {
            coyoteTimer = coyoteTime; // Resetear coyote time cuando toca el suelo
        }
        else
        {
            coyoteTimer -= Time.deltaTime; // Reducir el tiempo de gracia cuando está en el aire
        }

        // Manejo de Jump Queue (Cola de salto)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            queueTimer = queueTime; // Si presiona salto, almacenar el tiempo de espera
        }
        else
        {
            queueTimer -= Time.deltaTime; // Reducir el tiempo de espera del salto
        }

        // Ejecutar el salto si está permitido por Coyote Time o Jump Queue
        if (queueTimer > 0 && coyoteTimer > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Aplicar salto
            queueTimer = 0; // Resetear la cola de salto para evitar saltos dobles
        }
    }
    //<>
    void AttackLogic()
    {
        if (attackCdTimer > 0)
        {
            attackCdTimer -= Time.deltaTime; 
        }

        if (Input.GetKeyDown(KeyCode.J) && attackCdTimer <= 0)
        {
            anim.SetTrigger("IsAttacking");
            attackCdTimer = attackCd;
        }

    }
}