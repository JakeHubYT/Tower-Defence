using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
    public LayerMask groundLayerMask;
    public GameObject towerParentPrefab;



    public bool isDraggingTower = false;
    public GameObject followMouseTower;
    private GameObject spawnedTower;
    
   

    #region Singleton
    public static PlacementManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    #endregion

    // Update is called once per frame
    private void Update()
    {
        if (isDraggingTower) {

            TowerFollowMouse(followMouseTower);



          if (Input.GetMouseButtonDown(0)) 
          {
                followMouseTower = null;
                spawnedTower = null;
                isDraggingTower = false;
          }

        }


    }
    public void TowerFollowMouse(GameObject towerToMove)
    {
        isDraggingTower = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
        {
            towerToMove.transform.position = hit.point;
        }
    }

    public void SpawnTowerPrefab(Tower towerToSpawn)
    {
     
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
        {

            spawnedTower = Instantiate(towerParentPrefab, hit.point, Quaternion.identity);
            spawnedTower.GetComponent<TowerController>().towerScriptableObject = towerToSpawn;
            isDraggingTower = true;
            followMouseTower = spawnedTower;

        }
        else
        {
            spawnedTower = Instantiate(towerParentPrefab, transform.position, Quaternion.identity);
            spawnedTower.GetComponent<TowerController>().towerScriptableObject = towerToSpawn;
            isDraggingTower = true;
            followMouseTower = spawnedTower;
        }

       
    }

    public void ChangeFollowTower(GameObject tower)
    {
        followMouseTower = tower;
    }

}
