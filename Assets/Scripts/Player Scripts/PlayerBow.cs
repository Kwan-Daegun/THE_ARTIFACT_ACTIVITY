using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    [Header("Arrow")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 10f;

    [Header("Attack")]
    public float attackCooldown = 0.3f;

    [Header("Fire Point")]
    [SerializeField] private Transform firePoint;

    [Header("Animation")]
    [SerializeField] private PlayerAnimation playerAnimation;

    private float attackTimer;
    private AudioSource audioSource;
    private XPSystem xpSystem;

    private Vector2 shootDirection = Vector2.right;
    private void Start()
    {
        xpSystem = FindObjectOfType<XPSystem>();
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnShootButton()
    {
        if (Time.time >= attackTimer)
        {
            ShootArrow();
            attackTimer = Time.time + attackCooldown;
        }
    }

    public void SetShootDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
            shootDirection = direction.normalized;
    }

    void ShootArrow()
    {
        if (!firePoint || !arrowPrefab) return;

        audioSource.Play();

        Vector2 direction = GetNearestEnemyDirection();

        if (playerAnimation != null)
            playerAnimation.OnShoot(direction);

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null && xpSystem != null)
            arrowScript.damage += xpSystem.arrowDamageBonus;

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction * arrowSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    Vector2 GetNearestEnemyDirection()
    {
        WolfAI[] enemies = FindObjectsOfType<WolfAI>();
        WolfAI nearest = null;
        float closestDist = Mathf.Infinity;

        foreach (WolfAI enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                nearest = enemy;
            }
        }

        if (nearest != null)
            return (nearest.transform.position - transform.position).normalized;
        else
            return shootDirection; // fallback to last known direction if no enemies
    }
}