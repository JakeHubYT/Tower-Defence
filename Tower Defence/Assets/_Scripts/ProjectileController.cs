using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed; // the speed at which the projectile moves
    public float damage; // the amount of damage the projectile does
    public float destroyDelay; // the delay before the projectile is destroyed after impact

    private Transform target;
    Vector3 targetPos;
    // set the projectile's target and damage
    public void SetTarget(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
             targetPos = target.position;

        // move towards the target
        if (targetPos != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        }
      
          
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Health>().TakeDamage(damage);

            Destroy(gameObject, destroyDelay);
        }

        if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
