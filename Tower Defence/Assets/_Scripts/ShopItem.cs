using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public Tower tower;
    public TextMeshProUGUI priceText;


    private void Start()
    {
        priceText.text = "$" + tower.price.ToString();
    }
    public void SelectTower()
    {
        if(tower == null) { Debug.LogError("NO TOWER SCRIPTABLE OBJECT SELECTED FOR " + this.name); return; }

        PlacementManager.Instance.SpawnTowerPrefab(tower);
    }
}
