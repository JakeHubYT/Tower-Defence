using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public Tower tower;
    public TextMeshProUGUI priceText;
    public AudioClip buySound;
    public AudioClip errorSound;


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

            AudioManager.Instance.PlaySound(errorSound);



        }
        else
        {
            anim.SetTrigger("Buy");

            MoneyManager.Instance.SubtractFromMoney(tower.price);
            PlacementManager.Instance.SpawnTowerPrefab(tower);

            AudioManager.Instance.PlaySound(buySound);

        }




    }
}
