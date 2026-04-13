using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactAbility : MonoBehaviour
{
    [Header("Ability Settings")]
    public float abilityRadius = 3.5f;
    public float pushForce = 8f;
    public float cooldown = 60f;
    public int abilityDamage = 20;
    [Header("VFX")]
    public GameObject pushVFXPrefab;

    [Header("References")]
    public LayerMask enemyLayer;

    public float cooldownTimer;

    public void OnAbilityButton()
    {
        if (Time.time >= cooldownTimer)
        {
            ActivatePush();
            Debug.Log("activated");
            cooldownTimer = Time.time + cooldown;
        }
    }

    void ActivatePush()
    {
        if (pushVFXPrefab != null)
        {
            GameObject vfx = Instantiate(pushVFXPrefab, transform.position, Quaternion.identity);
            vfx.transform.localScale = Vector3.one * abilityRadius * 0.1f;
        }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position,
            abilityRadius,
            enemyLayer
        );

        foreach (Collider2D enemy in enemies)
        {
            WolfHealth wolf = enemy.GetComponent<WolfHealth>();
            if (wolf != null)
                wolf.TakeDamage(abilityDamage);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (enemy.transform.position - transform.position).normalized;
                rb.AddForce(direction * pushForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, abilityRadius);
    }
}