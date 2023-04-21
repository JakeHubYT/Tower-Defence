using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderController : MonoBehaviour
{
    public float speed; // the speed at which the projectile moves
    public float damage; // the amount of damage the projectile does
    public float destroyDelay; // the delay before the projectile is destroyed after impact
    public LayerMask groundMask;

    private Transform target;
    public Vector3 targetPos;
    bool gotDirection = false;
    Vector3 direction;


    // set the projectile's target and damage
    public void SetTarget(Transform target, float damage)
    {

        this.target = target;
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !gotDirection)
        {
            targetPos = target.position;
            direction = (targetPos - transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);
           gotDirection = true;
        }


       
       
        transform.position += direction * speed * Time.deltaTime;

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, groundMask))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + (transform.localScale.y * .5f) - 1 , transform.position.z);
        }

        if (target != null)
        {
           

            // move towards the target
          
            //transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            // transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            // If there is no target, destroy the projectile
            Destroy(gameObject, destroyDelay);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Deal damage to the enemy and destroy the projectile
            other.GetComponent<Health>().TakeDamage(damage);
            //Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            // Destroy the projectile if it hits the ground
           // Destroy(gameObject);
        }
    }
}
