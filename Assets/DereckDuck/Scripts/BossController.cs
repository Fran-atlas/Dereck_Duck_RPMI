using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Header("Vida")]
    [SerializeField] private PointsBarUI hpBar;
    [SerializeField] private int hp = 4;

    [Header("Ataque")]
    [SerializeField] private GameObject fireballPrefab; // Prefab de la bola de fuego
    [SerializeField] private float fireballSpawnHeight = 6f; // Altura desde donde caen las bolas
    [SerializeField] private float spawnRangeX = 5f; // Rango horizontal de aparición
    [SerializeField] private int fireballsPerAttack = 3; // Cantidad de bolas por ataque

    [Header("Animación")]
     private Animator animator; // Referencia al Animator
    [SerializeField] private string attack1Trigger = "Attack1"; // Trigger para Ataque 1
    [SerializeField] private string attack2Trigger = "Attack2"; // Trigger para Ataque 2

    private enum BossState { Idle, Ataque1, Ataque2 }
    [SerializeField] private BossState currentState;

    private float stateTimer;
    [SerializeField] private float attackCooldown = 15f; // Tiempo de espera entre Idle y ataque

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentState = BossState.Idle;
        stateTimer = attackCooldown;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            HandleWin();
            return;
        }

        if (!gameManager.pause)
        {

            switch (currentState)
            {
                case BossState.Idle:
                    HandleIdleState();
                    break;
                case BossState.Ataque1:
                    HandleAtaque1State();
                    break;
                case BossState.Ataque2:
                    HandleAtaque2State();
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProyectile"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        hp--;
        hp = Mathf.Max(hp, 0);
        hpBar.UpdatePoints(hp);
    }

    private void HandleIdleState()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            currentState = (Random.Range(0, 2) == 0) ? BossState.Ataque1 : BossState.Ataque2;
            animator.SetTrigger(currentState == BossState.Ataque1 ? attack1Trigger : attack2Trigger);
            stateTimer = attackCooldown;
        }
    }

    private void HandleAtaque1State()
    {
        SpawnFireballs();
        currentState = BossState.Idle; // Vuelve a Idle después de atacar
    }

    private void HandleAtaque2State()
    {
        currentState = BossState.Idle; // Vuelve a Idle después de atacar
    }

    private void SpawnFireballs()
    {
        for (int i = 0; i < fireballsPerAttack; i++)
        {
            float randomX = transform.position.x + Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y + fireballSpawnHeight, 0);
            GameObject fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
            // Aquí puedes añadir lógica para que la bola se mueva con una velocidad hacia abajo si es necesario
        }
    }

    void HandleWin()
    {
        gameManager.Win();
    }
}
