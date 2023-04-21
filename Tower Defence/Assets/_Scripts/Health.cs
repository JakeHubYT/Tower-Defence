using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;

    public float GetCurrentHealth() => currentHealth;
  
    private void Start()
    {
        if(GetComponent<EnemyController>())
        {
            maxHealth = GetComponent<EnemyController>().enemyScriptableObj.health;
        }

        currentHealth = maxHealth;
       
       
    }

    public void TakeDamage(float damageAmount)
    {
     

        currentHealth -= damageAmount;

        if (GetComponent<HealthUI>())
            GetComponent<HealthUI>().UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(tag == "Player")
        {
            Actions.OnLose();
        }

        else if(tag == "Enemy")
        {
            GetComponent<ParticleHandler>().PlayDeathParticle(transform);
            MoneyManager.Instance.AddToMoney(GetComponent<EnemyController>().enemyScriptableObj.moneyOnDeath);
        }

        // Handle death logic here
        Destroy(gameObject);
    }

   

}
