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
    private Camera mainCamera;
    private XPSystem xpSystem;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        xpSystem = GetComponent<XPSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= attackTimer)
        {
            ShootArrow();
            attackTimer = Time.time + attackCooldown;
        }
    }

    void ShootArrow()
    {
        if (!firePoint || !arrowPrefab)
            return;

        audioSource.Play();

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 direction = (mouseWorld - firePoint.position).normalized;

        if (playerAnimation != null)
            playerAnimation.OnShoot(direction);

        GameObject arrow = Instantiate(
            arrowPrefab,
            firePoint.position,
            Quaternion.identity
        );

        // Apply damage bonus from XPSystem
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null && xpSystem != null)
            arrowScript.damage += xpSystem.arrowDamageBonus;

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction * arrowSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}