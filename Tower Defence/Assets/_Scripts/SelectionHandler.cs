using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionHandler : MonoBehaviour
{
    public TowerController thisTowerController;
    public GameObject SelectionObject;


    private void Start()
    {
        thisTowerController = GetComponent<TowerController>();
        SelectionObject = thisTowerController.thisTower.transform.FindChildWithTag("SelectionObject").gameObject;
      

        Deselected();
    }

    public void MouseOver()
    {
        //Debug.Log("Over");

        SelectionObject.SetActive(true);

    }

    public void Selected()
    {
      //  Debug.Log("Selected");
    }

    public void Deselected()
    {
        //Debug.Log("Deselected");
        SelectionObject.SetActive(false);

    }
}
