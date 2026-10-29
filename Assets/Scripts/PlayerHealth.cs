using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer sr;

    public float invincibilityTime = 0.5f;
    private bool isInvincible = false;

    public int maxHealth = 80;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        healthBar.SetHealth(currentHealth);
    }


    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        CameraShake.Instance.Shake(0.2f, 0.08f);

        StartCoroutine(InvincibilityRoutine());

        if (currentHealth <= 0)
        {
            GameStateManager.Instance.ShowGameOver();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }
    void Awake()
    {
        Debug.Log("PLAYER SCRIPT LOADED");
    }
    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        float elapsed = 0f;
        float blinkInterval = 0.1f;

        while (elapsed < invincibilityTime)
        {
            sr.enabled = !sr.enabled; 
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        sr.enabled = true; 
        isInvincible = false;
    }




}