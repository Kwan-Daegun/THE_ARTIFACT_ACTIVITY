using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPSystem : MonoBehaviour
{
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public float xpMultiplier = 1.5f;
    public int arrowDamageBonus = 0;

    private bool levelUpPending = false;

    private PlayerBow playerBow;
    private PlayerBackpack playerBackpack;

    [Header("UI")]
    public GameObject levelUpPanel;
    public Button damageButton;
    public Button speedButton;
    public Button backpackButton;
    public TMP_Text xpDisplayText;

    private void Start()
    {
        playerBackpack = FindObjectOfType<PlayerBackpack>();

        playerBow = FindObjectOfType<PlayerBow>();

        levelUpPanel.SetActive(false);

        damageButton.onClick.AddListener(() => LevelUp(1));
        speedButton.onClick.AddListener(() => LevelUp(2));
        backpackButton.onClick.AddListener(() => LevelUp(3));

        UpdateXPText();
    }

    private void Update()
    {
        if (currentXP >= xpToNextLevel && !levelUpPending)
        {
            levelUpPending = true;
            levelUpPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void GainXP(int amount)
    {
        if (levelUpPending) return;

        currentXP += amount;
        Debug.Log("EXP: " + currentXP + " / " + xpToNextLevel);
        UpdateXPText();
    }

    private void LevelUp(int choice)
    {
        currentXP -= xpToNextLevel;
        level++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpMultiplier);
        levelUpPending = false;

        switch (choice)
        {
            case 1:
                IncreaseAttackDamage();
                break;
            case 2:
                IncreaseAttackSpeed();
                break;
            case 3:
                IncreaseBackpackCapacity();
                break;
        }

        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;

        UpdateXPText();

        Debug.Log("Now Level " + level + " | Next level at " + xpToNextLevel + " XP");
    }

    private void IncreaseAttackDamage()
    {
        arrowDamageBonus += 5;
        Debug.Log("Attack Damage bonus increased! Bonus: " + arrowDamageBonus);
    }

    private void IncreaseAttackSpeed()
    {
        if (playerBow != null)
        {
            playerBow.attackCooldown = Mathf.Max(0.05f, playerBow.attackCooldown - 0.05f);
            Debug.Log("Attack Speed increased! Cooldown now: " + playerBow.attackCooldown);
        }
    }

    private void IncreaseBackpackCapacity()
    {
        if (playerBackpack != null)
        {
            playerBackpack.maxNumberOfFruitsToStore += 10;
            playerBackpack.UpdateBackpackText();
            Debug.Log("Backpack Capacity increased! Now: " + playerBackpack.maxNumberOfFruitsToStore);
        }
    }

    private void UpdateXPText()
    {
        if (xpDisplayText != null)
            xpDisplayText.text = currentXP + "/" + xpToNextLevel;
    }
}