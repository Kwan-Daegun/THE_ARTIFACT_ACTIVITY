using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserAi : MonoBehaviour
{
   public string targetTag = "Player";
    public float moveSpeed = 1f;
    public int attackDamage = 5;
    public float attackCooldown = 1f;
    public float stopDistance = 1f;

    private Transform target;
    private float attackTimer;

    private void Start()
    {
        GameObject targetObj = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObj != null)
            target = targetObj.transform;
    }

    private void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            if (Time.time >= attackTimer)
            {
                Attack();
                attackTimer = Time.time + attackCooldown;
            }
        }
    }

    private void Attack()
    {
        var health = target.GetComponent<PlayerHealth>(); // Replace with your damage system
        if (health != null)
        {
            health.TakeDamage(attackDamage);
        }
    }
}
