using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private PointsBarUI hpBar;
    [SerializeField] private int hp = 4;

    [Header("Ataque")]
    [SerializeField] private GameObject fireballPrefab; // Prefab de la bola de fuego
    [SerializeField] private float attackCooldown = 3f; // Tiempo entre ataques
    [SerializeField] private float fireballSpawnHeight = 6f; // Altura desde donde caen las bolas
    [SerializeField] private float spawnRangeX = 5f; // Rango horizontal de aparición
    [SerializeField] private int fireballsPerAttack = 3; // Cantidad de bolas por ataque

    private float attackTimer;

    private void Update()
    {
        HandleAttack();
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
        Debug.LogWarning($"Boss hit! HP = {hp}");
    }

    private void HandleAttack()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            SpawnFireballs();
            attackTimer = attackCooldown;
        }
    }

    private void SpawnFireballs()
    {
        for (int i = 0; i < fireballsPerAttack; i++)
        {
            float randomX = transform.position.x + Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y + fireballSpawnHeight, 0);
            GameObject fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
