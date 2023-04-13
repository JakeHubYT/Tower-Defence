using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public LayerMask groundLayerMask;
    public GameObject objectToSpawn;

    private GameObject spawnedObject;
    private bool isDragging = false;
    bool hasntDragged = true;

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

    private void Update()
    {



        if (isDragging) // left click held down
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
            {
                spawnedObject.transform.position = hit.point;
            }
        }

        if (Input.GetMouseButtonDown(0) && isDragging) // left click released
        {
            isDragging = false;
            hasntDragged= false;
        }
       /* else if (Input.GetMouseButtonDown(0) && !isDragging && !hasntDragged) // left click released
        {
            Actions.OnPlayerClickNoDragging();
        }*/
    }

    public void SpawnTowerPrefab(Tower towerToSpawn)
    {
     
         Debug.Log("In SpawnTowerPrefab");

       
            Debug.Log("In Mouse down");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
        {
            Debug.Log("Spawn object at position: " + hit.point);
            spawnedObject = Instantiate(objectToSpawn, hit.point, Quaternion.identity);
            spawnedObject.GetComponent<TowerController>().towerScriptableObject = towerToSpawn;
            isDragging = true;
            hasntDragged = true;

        }
        else
        {
            spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            spawnedObject.GetComponent<TowerController>().towerScriptableObject = towerToSpawn;
            isDragging = true;
            hasntDragged = true;
            
        }




    }
}
