using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy enemyScriptableObj;
    public PathCreator pathCreator;
    float distanceTraveled;

    private void Start()
    {
        pathCreator = GameObject.FindGameObjectWithTag("Path").GetComponent<PathCreator>();
        Instantiate(enemyScriptableObj.model, transform.position, Quaternion.identity, transform);
    }
    private void Update()
    {
        distanceTraveled += enemyScriptableObj.speed * Time.deltaTime;

        transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled);

     
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled);
      
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                Destroy(gameObject);
                playerHealth.TakeDamage(enemyScriptableObj.damage);
                

            }
        }
    }

}
