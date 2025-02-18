using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public int currentShield;
    public int maxShield;

    public Slider healthSlider;
    public Slider shieldSlider;

    public TMP_Text healthNumber;
    public TMP_Text shieldNumber;

    public float shieldRegenDelay = 3f;
    public float shieldRegenRate = 1f;

    private Coroutine shieldRegenCoroutine;

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthNumber.text = currentHealth + "/" + maxHealth;

        shieldSlider.maxValue = maxShield;
        shieldSlider.value = currentShield;
        shieldNumber.text = currentShield + "/" + maxShield;
    }

    //public void ChangeHealth(int damage)
    //{
    //    currentHealth += damage;
    //    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    //    healthSlider.value = currentHealth;
    //    healthNumber.text = currentHealth + "/" + maxHealth;

    //    if (currentHealth <= 0)
    //    {
    //        Die();
    //    }
    //}

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        if (currentShield > 0)
        {
            currentShield -= damage;
            if (currentShield < 0)
            {
                currentHealth += currentShield;
                currentShield = 0;
            }

            StartShieldRegen();
        }
        else
        {
            currentHealth -= damage;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);

        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void StartShieldRegen()
    {
        if (shieldRegenCoroutine != null)
        {
            StopCoroutine(shieldRegenCoroutine); 
        }

        shieldRegenCoroutine = StartCoroutine(RegenerateShield());
    }

    private IEnumerator RegenerateShield()
    {
        yield return new WaitForSeconds(shieldRegenDelay); 

        while (currentShield < maxShield)
        {
            currentShield += Mathf.CeilToInt(shieldRegenRate * Time.deltaTime);
            //currentShield = Mathf.Clamp(currentShield, 0, maxShield);
            UpdateUI();
            yield return new WaitForSeconds(shieldRegenRate); ;
        }

        shieldRegenCoroutine = null;
    }

    private void UpdateUI()
    {
        healthSlider.value = currentHealth;
        healthNumber.text = currentHealth + "/" + maxHealth;

        shieldSlider.value = currentShield;
        shieldNumber.text = currentShield + "/" + maxShield;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
