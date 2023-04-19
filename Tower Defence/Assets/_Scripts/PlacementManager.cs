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

          if (Input.GetMouseButtonDown(0)) // left click released
          {
                isDraggingTower = false;
          }

        }


    }
    private void TowerFollowMouse(GameObject towerToMove)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
        {
            spawnedTower.transform.position = hit.point;
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

        }
        else
        {
            spawnedTower = Instantiate(towerParentPrefab, transform.position, Quaternion.identity);
            spawnedTower.GetComponent<TowerController>().towerScriptableObject = towerToSpawn;
            isDraggingTower = true;
        }

        followMouseTower = spawnedTower;
    }

}
