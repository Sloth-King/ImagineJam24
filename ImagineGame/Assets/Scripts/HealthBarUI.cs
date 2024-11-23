using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthBarSlider;

    public void SetMaxHealth(int maxHealth)
    {
        Debug.Log("Setting max health: " + maxHealth);
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
    }

    public void UpdateHealthBar(int currentHealth)
    {
        healthBarSlider.value = currentHealth;
    }
}
