using PathCreation;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public Tower towerScriptableObject;
    public Transform firePoint;
    public GameObject thisTower;
    public GameObject sightRadiusSphere;

    Animator thisAnim;
    PathCreator path;
   

    GameObject target;
    float lastShotTime;
    bool spawned = false;
    Transform rotatePoint;
    


    //START IS TOO QUICK
    private void Start()
    {
        thisTower = Instantiate(towerScriptableObject.model, transform.position, Quaternion.identity, transform);

        path = GameObject.FindGameObjectWithTag("Path").GetComponent<PathCreator>();
       
        thisAnim = thisTower.transform.FindChildWithTag("Animator").GetComponent<Animator>();

        rotatePoint = thisTower.transform.FindChildWithTag("RotatePoint");
        firePoint = thisTower.transform.FindChildWithTag("FirePoint");

        if (thisAnim == null)
        {
            Debug.LogError("NO ANIM TAG ON TOWER! "+ thisTower.name);
        }

    }

    void Update()
    {
        if (towerScriptableObject == null) { return; }

        if (spawned == false)
        {
            lastShotTime = -towerScriptableObject.fireRate;
            spawned = true;
        }

        FindTarget();

        // fire a projectile at the target if it's within range and enough time has elapsed since the last shot
        if (target != null && Time.time - lastShotTime > towerScriptableObject.fireRate)
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            LookAt(targetPos);
            FireProjectile();
        }

        if (PlacementManager.Instance.isDraggingTower)
        {
            LookAtPath();
        }

        // stop shooting at the target if it leaves the target radius
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget > towerScriptableObject.sightRadius)
            {
                target = null;
                
            }
        }

        if(target == null)
        {
            AudioManager.Instance.FadeOut(1, true);
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
               // Debug.Log("Sees Enemy");
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = col.gameObject;
                }
            }
        }
    }


    void LookAtPath()
    {
        if (towerScriptableObject.sightRadius > 20)
        {
            LookAtEntrance(path.path.GetPoint(1));
        }
        else
        {
            LookAt(path.path.GetClosestPointOnPath(transform.position));
        }

    }
    void FireProjectile()
    {
        if (towerScriptableObject.oneShotAudio)
        {
            AudioManager.Instance.PlaySound(towerScriptableObject.fireSound);


        }
        else if (!towerScriptableObject.oneShotAudio)
        {
            AudioManager.Instance.PlayMusic(towerScriptableObject.fireSound, true, true);

        }



        thisAnim.SetTrigger("Fire");

        lastShotTime = Time.time; 

        GameObject proj = Instantiate(towerScriptableObject.projectilePrefab, firePoint.position, Quaternion.identity); // create the projectile

        if (proj.GetComponent<BoulderController>() != null)
        proj.GetComponent<BoulderController>().SetTarget(target.transform, towerScriptableObject.damage); // set the projectile's target and damage

        else
        proj.GetComponent<ProjectileController>().SetTarget(target.transform, towerScriptableObject.damage); // set the projectile's target and damage



       
      




    }

    void LookAt(Vector3 targetPosition)
    {
        if (rotatePoint != null) { rotatePoint.LookAt(targetPosition); }
        else if (rotatePoint == null ) { transform.LookAt(targetPosition); }
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

    public void ShowSightRadius()
    {
        sightRadiusSphere.transform.localScale = Vector3.one * (2 * towerScriptableObject.sightRadius);
        sightRadiusSphere.SetActive(true);
    }
    public void HideSightRadius() => sightRadiusSphere.SetActive(false);
    
}
