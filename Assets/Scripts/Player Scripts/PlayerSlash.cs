using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
  [Header("Arrow")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 10f;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 0.3f;

    [Header("Fire Point")]
    [SerializeField] private Transform firePoint;

    private float attackTimer;

    private AudioSource audioSource;
    private Camera mainCamera;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
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

        GameObject arrow = Instantiate(
            arrowPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * arrowSpeed;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

} // class


































