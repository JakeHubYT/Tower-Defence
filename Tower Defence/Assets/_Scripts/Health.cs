using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    public float GetCurrentHealth() => currentHealth;
  
    private void Start()
    {
        currentHealth = maxHealth;
       
       
    }

    public void TakeDamage(float damageAmount)
    {
        if (GetComponent<HealthUI>())
            GetComponent<HealthUI>().UpdateHealthUI();

        currentHealth -= damageAmount;
       
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle death logic here
        Destroy(gameObject);
    }

   

}
