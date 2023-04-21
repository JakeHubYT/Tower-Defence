using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public AudioClip hitSound;
    public AudioClip biteSound;

    bool playedHit = false;


    public float totalTime = .5f;
    public float currentTime = 0f;


    public float GetCurrentHealth() => currentHealth;
  
    private void Start()
    {
        if(GetComponent<EnemyController>())
        {
            maxHealth = GetComponent<EnemyController>().enemyScriptableObj.health;
        }

        currentHealth = maxHealth;
       
       
    }

    private void Update()
    {
      
           currentTime += Time.deltaTime;
            if (currentTime >= totalTime)
            {
                // Timer has finished, do something here
                Debug.Log("Timer finished!");
                currentTime = 0f;
               playedHit = false;

             }
        

       
    }

    public void TakeDamage(float damageAmount)
    {
        if (!playedHit)
        {
            AudioManager.Instance.PlaySound(hitSound);
            playedHit = true;
            currentTime = 0;
        }
       
        if (tag == "Player")
        {
            AudioManager.Instance.PlaySound(biteSound);
            CameraManager.Instance.PunchShake(.1f);
        }

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

            var enemyController = GetComponent<EnemyController>();

            AudioManager.Instance.PlaySound(enemyController.enemyScriptableObj.dieSound);
           
            GetComponent<ParticleHandler>().PlayDeathParticle(transform);
            MoneyManager.Instance.AddToMoney(enemyController.enemyScriptableObj.moneyOnDeath);

            CameraManager.Instance.PunchShake(enemyController.enemyScriptableObj.cameraShakeStrength);
            Actions.OnEnemyKilled();
        }

        // Handle death logic here
        Destroy(gameObject);
    }

   

}
