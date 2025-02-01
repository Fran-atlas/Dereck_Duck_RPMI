using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // --- COMPONENTES ---
    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sp;
    private Transform tr;

    // --- MOVIMIENTO ---
    [Header("Movimiento")]
    [SerializeField] private float speed;
    [SerializeField] private float threshold;
    private float horizontalInput;
    private bool isLookingLeft;

    // --- SALTO ---
    [Header("Salto")]
    private bool isGrounded;
    [SerializeField] private GameObject GroundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimer;

    [Header("Jump Queue")]
    [SerializeField] private float queueTime = 0.2f;
    private float queueTimer;

    // --- ATAQUE ---
    [Header("Ataque")]
    [SerializeField] private float attackCd = 1.1f;
    private float attackCdTimer;
    [SerializeField] private GameObject proyectilePrefab;
    [SerializeField] private float proyectileSpeed = 10f;
    [SerializeField] private Transform shootingPoint;

    [Header("Vida")]
    [SerializeField] private int hp = 4;

    [Header("Invencibilidad")]
    [SerializeField] private float invincibilityDuration = 1.5f;
    private bool isInvincible;
    private float invincibilityTimer;

    [Header("Mana")]
    [SerializeField] private int mana = 5;
    [SerializeField] private float manaRechargeTime = 5f;
    private float manaRechargeTimer;

    [Header("UI")]
    [SerializeField] private PointsBarUI hpBar;
    [SerializeField] private PointsBarUI manaBar;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
        manaRechargeTimer = manaRechargeTime;
    }

void Update()
    {
        if (hp <= 0)
        {
            HandleDead();
            return;
        }

        if (!gameManager.pause)
        {   
            isGrounded = Physics2D.OverlapCircle(GroundCheck.transform.position, 0.1f, groundLayer);
            Movement();
            JumpLogic();
            AttackLogic();
            HandleInvincibility();
            HandleManaRecharge();
        }
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
            isLookingLeft = horizontal < 0;
            sp.flipX = isLookingLeft;
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
        anim.SetBool("IsJumping", !isGrounded);

        
        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            queueTimer = queueTime;
        }
        else
        {
            queueTimer -= Time.deltaTime;
        }

        if (queueTimer > 0 && coyoteTimer > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            queueTimer = 0;
        }
    }
    
    void AttackLogic()
    {
        if (attackCdTimer > 0)
        {
            attackCdTimer -= Time.deltaTime; 
        }

        if (Input.GetKeyDown(KeyCode.J) && attackCdTimer <= 0 && mana > 0)
        {
            anim.SetTrigger("IsAttacking");
            attackCdTimer = attackCd;
            Shoot();
            mana--;
            manaBar.UpdatePoints(mana);
        }

    }

    void Shoot()
    {
        GameObject proyectile = Instantiate(proyectilePrefab, shootingPoint.position, Quaternion.identity);

        Rigidbody2D proyectileRb = proyectile.GetComponent<Rigidbody2D>();
        SpriteRenderer proyectileSp = proyectile.GetComponent<SpriteRenderer>();

        if (proyectileRb != null)
        {
            float direction = isLookingLeft ? -1f : 1f;
            proyectileSp.flipX = isLookingLeft;
            proyectileRb.velocity = new Vector2(direction * proyectileSpeed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("BossProyectile") || collision.CompareTag("GroundFire")) && !isInvincible)
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        hp--;
        hp = Mathf.Max(hp, 0);
        hpBar.UpdatePoints(hp);

        if (hp > 0)
        {
            StartInvincibility();
        }
    }

    void StartInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
        StartCoroutine(BlinkEffect());
    }

    void HandleInvincibility()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
                sp.color = Color.white; // Restaurar color normal
            }
        }
    }

    IEnumerator BlinkEffect()
    {
        while (isInvincible)
        {
            sp.color = new Color(1, 1, 1, 0.5f); // Semi-transparente
            yield return new WaitForSeconds(0.15f);
            sp.color = Color.white;
            yield return new WaitForSeconds(0.15f);
        }
    }

    void HandleManaRecharge()
    {
        if (mana < 5)
        {
            manaRechargeTimer -= Time.deltaTime;

            if (manaRechargeTimer <= 0)
            {
                mana++; // Recargar 1 de mana
                manaBar.UpdatePoints(mana); // Actualizar UI
                manaRechargeTimer = manaRechargeTime; // Reiniciar temporizador
            }
        }
    }

    void HandleDead()
    {
        anim.SetBool("IsDead", true);
        gameManager.GameOver();
    }
}