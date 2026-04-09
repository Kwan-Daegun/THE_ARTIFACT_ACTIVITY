using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField]
    public Sprite[] walkSprites;

    [SerializeField]
    public Sprite[] attackSprites;

    [SerializeField]
    private float walkAnimSpeed = 0.15f;

    [SerializeField]
    private float attackAnimSpeed = 0.1f;

    private EnemyChaserAi enemyAI;
    private SpriteRenderer sr;

    private int walkState = 0;
    private int attackState = 0;
    private float animTimer;

    private bool isPlayingAttack = false;
    private bool wasMoving = false;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyChaserAi>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        FlipSprite();

        if (isPlayingAttack)
        {
            PlayAttackAnim();
            return;
        }

        bool isMoving = IsMoving();

        if (isMoving)
            PlayWalkAnim();
        else
            sr.sprite = walkSprites[0]; // idle frame
    }

    private bool IsMoving()
    {
        if (enemyAI == null || enemyAI.target == null) return false;

        float distance = Vector2.Distance(transform.position, enemyAI.target.position);
        return distance > enemyAI.stopDistance;
    }

    private void FlipSprite()
    {
        if (enemyAI == null || enemyAI.target == null) return;

        // false = facing right, true = facing left
        // Remove ! if your sprite's default direction is right
        sr.flipX = !(enemyAI.target.position.x < transform.position.x);
    }

    private void PlayWalkAnim()
    {
        if (walkSprites.Length == 0) return;

        if (Time.time > animTimer)
        {
            sr.sprite = walkSprites[walkState];
            walkState = (walkState + 1) % walkSprites.Length;
            animTimer = Time.time + walkAnimSpeed;
        }
    }

    private void PlayAttackAnim()
    {
        if (attackSprites.Length == 0)
        {
            isPlayingAttack = false;
            return;
        }

        if (Time.time > animTimer)
        {
            if (attackState < attackSprites.Length)
            {
                sr.sprite = attackSprites[attackState];
                attackState++;
                animTimer = Time.time + attackAnimSpeed;
            }
            else
            {
                // Attack anim done, reset
                isPlayingAttack = false;
                attackState = 0;
                walkState = 0;
            }
        }
    }

    public void TriggerAttackAnim()
    {
        if (!isPlayingAttack)
        {
            isPlayingAttack = true;
            attackState = 0;
            animTimer = Time.time;
        }
    }
}