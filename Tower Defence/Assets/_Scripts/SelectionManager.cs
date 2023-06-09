using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    public GameObject towerPanel;
    public LayerMask towerLayerMask;


    public SelectionHandler thisTowerSelectionHandler;
    bool selected = false;


    // Update is called once per frame
    void Update()
    {

       if( PlacementManager.Instance.isDraggingTower) { return; }


        if (Input.GetMouseButtonDown(0)) // left click released
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                DeselectTowerUi();
            }
        }

            // Check if mouse is over a tower
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, towerLayerMask))
        {
            if (!selected)
            {
              
                thisTowerSelectionHandler = hit.collider.gameObject.GetComponentInParent<SelectionHandler>();

                if (thisTowerSelectionHandler != null)
                {
                    thisTowerSelectionHandler.MouseOver();
                    thisTowerSelectionHandler.gameObject.GetComponent<TowerController>().ShowSightRadius();

                }
                if (Input.GetMouseButtonDown(1))
                {
                    TutorialManager.Instance.OnRightClickTower();

                    selected = true;
                    thisTowerSelectionHandler.Selected();

                    TriggerTowerUi();
                }

            }
        }


        else // Mouse is not over a tower
        {
            if (!selected && thisTowerSelectionHandler != null) { 
            thisTowerSelectionHandler.gameObject.GetComponent<TowerController>().HideSightRadius();
            }




            thisTowerSelectionHandler.Deselected();

            if (thisTowerSelectionHandler != null && !selected)
            {
                thisTowerSelectionHandler.Deselected();
                thisTowerSelectionHandler = null;

                
            }
        }
    }

    private void DeselectTowerUi()
    {
        selected = false;
        towerPanel.SetActive(false);
        thisTowerSelectionHandler = null;

        if(thisTowerSelectionHandler != null)
        thisTowerSelectionHandler.gameObject.GetComponent<TowerController>().HideSightRadius();
    }

    private void TriggerTowerUi()
    {
        towerPanel.SetActive(true);
        towerPanel.transform.position = Input.mousePosition;

        thisTowerSelectionHandler.gameObject.GetComponent<TowerController>().ShowSightRadius();

    }


    public void Sell()
    {
        MoneyManager.Instance.AddToMoney(thisTowerSelectionHandler.gameObject.GetComponent<TowerController>().towerScriptableObject.price); 
        Destroy(thisTowerSelectionHandler.gameObject);
        DeselectTowerUi();
    }


    public void Move()
    {
        // PlacementManager.Instance.followMouseTower = thisTowerSelectionHandler.gameObject;
       
        PlacementManager.Instance.ChangeFollowTower(thisTowerSelectionHandler.gameObject);
        PlacementManager.Instance.isDraggingTower = true;
        thisTowerSelectionHandler = null;
        DeselectTowerUi();

    }


}

