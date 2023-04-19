using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
    public LayerMask groundLayerMask;
    public LayerMask towerLayerMask;
    public GameObject objectToSpawn;
    public GameObject towerPanel;


    private GameObject spawnedObject;
    private bool isDragging = false;
    bool hasntDragged = true;
    bool selected = false;
    private SelectionHandler selectedObject;

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
        if (isDragging){ SpawnObjectAtPoint(); }

        if (Input.GetMouseButtonDown(0)) // left click released
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                DeselectTowerUi();
               
            }

            if (isDragging)
            {
                isDragging = false;
                hasntDragged = false;
            }

        }

        if (isDragging) { return; }


        // Check if mouse is over a tower
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, towerLayerMask))
        {
            if (!selected) { 

            selectedObject = hit.collider.gameObject.GetComponentInParent<SelectionHandler>();

            if (selectedObject != null)
            {
                selectedObject.MouseOver();
            }
            if (Input.GetMouseButtonDown(1))
            {
                selected = true;
                selectedObject.Selected();
                towerPanel.SetActive(true);
                towerPanel.transform.position = Input.mousePosition;
            }

            }
        }


        else // Mouse is not over a tower
        {
            if (selectedObject != null && !selected) // If there was a selected object before
            {
                selectedObject.Deselected();
                selectedObject = null; // Reset selected object
            }
        }
    }

    private void DeselectTowerUi()
    {
        selected = false;
        towerPanel.SetActive(false);
    }

    private void SpawnObjectAtPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
        {
            spawnedObject.transform.position = hit.point;
        }
    }

    public void SpawnTowerPrefab(Tower towerToSpawn)
    {
     
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


    public void Sell()
    {
        Destroy(selectedObject.gameObject);
        DeselectTowerUi();
    }
}
