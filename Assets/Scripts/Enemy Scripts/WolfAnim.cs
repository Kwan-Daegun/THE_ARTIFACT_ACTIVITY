using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnim : MonoBehaviour
{
    [SerializeField]
    private Sprite[] wolfAnimSprites;

    [SerializeField]
    private Sprite[] wolfAttackSprites; // Add your attack sprites here in Inspector

    [SerializeField]
    private float animTimeTreshold = 0.15f;

    [SerializeField]
    private float attackAnimTimeTreshold = 0.1f; // Attack can be faster

    private WolfAI wolfAI;
    private SpriteRenderer sr;

    private int state = 0;
    private float animTimer;

    private bool isPlayingAttack = false;
    private int attackState = 0;

    private void Awake()
    {
        wolfAI = GetComponent<WolfAI>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sr.flipX = !wolfAI.left;

        // If attack animation is playing, finish it first
        if (isPlayingAttack)
        {
            if (Time.time > animTimer)
            {
                if (attackState < wolfAttackSprites.Length)
                {
                    sr.sprite = wolfAttackSprites[attackState];
                    attackState++;
                    animTimer = Time.time + attackAnimTimeTreshold;
                }
                else
                {
                    // Attack animation done
                    isPlayingAttack = false;
                    attackState = 0;
                    state = 0;
                }
            }
            return;
        }

        if (wolfAI.isMoving)
        {
            if (Time.time > animTimer)
            {
                sr.sprite = wolfAnimSprites[state];
                state++;
                if (state == wolfAnimSprites.Length)
                    state = 0;

                animTimer = Time.time + animTimeTreshold;
            }
        }
        else
        {
            sr.sprite = wolfAnimSprites[0];
        }
    }

    // Call this from WolfAI when attacking
    public void TriggerAttackAnim()
    {
        if (!isPlayingAttack)
        {
            isPlayingAttack = true;
            attackState = 0;
            animTimer = Time.time;
        }
    }

} // class