using PathCreation;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public Tower towerScriptableObject;
    public GameObject projectilePrefab;
    public Transform firePosition;
    public GameObject thisTower;
    public Animator thisAnim;


    public PathCreator path;

    private GameObject target;
    private float lastShotTime;

   
    bool spawned = false;

    
       
    

    //START IS TOO QUICK
    private void Start()
    {
        thisTower = Instantiate(towerScriptableObject.model, transform.position, Quaternion.identity, transform);

        path = GameObject.FindGameObjectWithTag("Path").GetComponent<PathCreator>();
       
         thisAnim = thisTower.transform.FindChildWithTag("Animator").GetComponent<Animator>();

        if(thisAnim == null)
        {
            Debug.LogError("NO ANIM TAG ON TOWER! "+ thisTower.name);
        }

    }

    void Update()
    {
        if(towerScriptableObject == null) { return; }

        if(spawned == false)
        {
            lastShotTime = -towerScriptableObject.fireRate;
            spawned = true;
        }

        FindTarget();

        // fire a projectile at the target if it's within range and enough time has elapsed since the last shot
        if (target != null && Time.time - lastShotTime > towerScriptableObject.fireRate)
        {
            FireProjectile();
        }

        if (towerScriptableObject.sightRadius > 20)
        {
            LookAtEntrance(path.path.GetPoint(1));
        }
        else
        {
            LookAt(path.path.GetClosestPointOnPath(transform.position), true);
        }

    }

    void FindTarget()
    {
        // find the closest enemy target within range
        Collider[] targets = Physics.OverlapSphere(transform.position, towerScriptableObject.sightRadius);
        float closestDistance = Mathf.Infinity;
        foreach (Collider col in targets)
        {
            if (col.CompareTag("Enemy"))
            {
                Debug.Log("Sees Enemy");
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = col.gameObject;
                }
            }
        }
    }

    void FireProjectile()
    {
        thisAnim.SetTrigger("Fire");

        lastShotTime = Time.time; 

        GameObject proj = Instantiate(projectilePrefab, firePosition.position, Quaternion.identity); // create the projectile

        proj.GetComponent<ProjectileController>().SetTarget(target.transform, towerScriptableObject.damage); // set the projectile's target and damage


       
        Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        LookAt(targetPos);



    }

    void LookAt(Vector3 targetPosition, bool rotateEntireMesh = false)
    {
        Transform rotatePoint = thisTower.transform.FindChildWithTag("RotatePoint");
       
       // Debug.Log(thisTower.transform);
        if (rotatePoint != null && !rotateEntireMesh) { rotatePoint.LookAt(targetPosition); }
        else if (rotatePoint == null || rotateEntireMesh) { thisTower.transform.LookAt(targetPosition); }
    }

    void LookAtEntrance(Vector3 targetPosition)
    {
        Debug.Log(thisTower.transform);
        thisTower.transform.LookAt(targetPosition); 
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, towerScriptableObject.sightRadius);
    }
}
