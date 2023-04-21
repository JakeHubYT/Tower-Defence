using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int money = 0;
    public TextMeshProUGUI urMoneyText;

    int oldMoney = 0;

    #region Singleton
    public static MoneyManager Instance { get; private set; }
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

    private void OnEnable()
    {
        Actions.OnWaveEnded += AddMoneyAfterRound;
    }
    private void OnDisable()
    {
        Actions.OnWaveEnded -= AddMoneyAfterRound;

    }

    private void Start()
    {
        UpdateMoneyUi();
        oldMoney = money;
    }

    private void Update()
    {
        DetectMoneyChange(oldMoney, money);
        oldMoney = money;
    }

    public void AddToMoney(int moneyAmount)
    {
        money += moneyAmount; 
    }
    public void SubtractFromMoney(int moneyAmount)
    {
        money -= moneyAmount;
    }

    public void DetectMoneyChange(int oldValue, int newValue)
    {
        if (oldValue != newValue)
        {
            UpdateMoneyUi();
            oldMoney = money;
        }
    }

    public void UpdateMoneyUi()
    {
        urMoneyText.text = "Money $" +  money.ToString();
    }

    void AddMoneyAfterRound()
    {
       // money += 20;

        
    }
}