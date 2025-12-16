using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactAbility : MonoBehaviour
{
    [Header("Ability Settings")]
    public float abilityRadius = 3.5f;
    public float pushForce = 8f;
    public float cooldown = 60f;
    [Header("VFX")]
    public GameObject pushVFXPrefab;

    [Header("References")]
    public LayerMask enemyLayer;

    private float cooldownTimer;
    private bool playerInRange;

    private void Update()
    {
        if (!playerInRange)
            return;

        if (Time.time >= cooldownTimer && Input.GetKeyDown(KeyCode.Q))
        {
            ActivatePush();
            Debug.Log("activated");
            cooldownTimer = Time.time + cooldown;
        }
    }

    void ActivatePush()
    {
        
        GameObject vfx = Instantiate(pushVFXPrefab, transform.position, Quaternion.identity);
        vfx.transform.localScale = Vector3.one * abilityRadius *0.1f;

        
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position,
            abilityRadius,
            enemyLayer
        );

        foreach (Collider2D enemy in enemies)
        {
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (enemy.transform.position - transform.position).normalized;
                rb.AddForce(direction * pushForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }

    // Optional: visualize radius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, abilityRadius);
    }
}
