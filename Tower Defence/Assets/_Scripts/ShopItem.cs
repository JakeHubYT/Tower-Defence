using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public Tower tower;
    public TextMeshProUGUI priceText;
    Animator anim;

    private void Start()
    {
        priceText.text = "$" + tower.price.ToString();
        anim = GetComponent<Animator>();
    }

    public void TryBuyItem()
    {
        if(tower == null) { Debug.LogError("NO TOWER SCRIPTABLE OBJECT SELECTED FOR " + this.name); return; }



        if( MoneyManager.Instance.money < tower.price ) 
        {
            anim.SetTrigger("Error");

          //  Debug.Log("CANT BUY!!");
        }
        else
        {
            anim.SetTrigger("Buy");

            MoneyManager.Instance.SubtractFromMoney(tower.price);
            PlacementManager.Instance.SpawnTowerPrefab(tower);


          //  Debug.Log("BOUGHT!!");
        }
       



    }
}
