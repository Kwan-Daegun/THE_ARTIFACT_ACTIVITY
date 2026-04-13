using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject healthUI;

    private float scale;

    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    public int xpValue = 10; // XP awarded on death

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        scale = (float)currentHealth / maxHealth;
        healthUI.transform.localScale = new Vector3(scale, healthUI.transform.localScale.y, 1f);

        GetComponent<WolfAnim>()?.FlashRed();

        if (currentHealth <= 0)
        {
            FindObjectOfType<XPSystem>().GainXP(xpValue);
            GetComponent<WolfAI>().Die();
        }
    }


} // class























