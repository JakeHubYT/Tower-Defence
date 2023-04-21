using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool didWin = false;
    public GameObject endScreen;
    public TextMeshProUGUI winText;




    #region Singleton
    public static GameManager Instance { get; private set; }
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
        Actions.OnLose += ShowEndScreen;

        Actions.OnWin += SetWinText;
        Actions.OnWin += ShowEndScreen;

    }

    private void OnDisable()
    {
        Actions.OnLose -= ShowEndScreen;

        Actions.OnWin -= SetWinText;
        Actions.OnWin -= ShowEndScreen;


    }

    private void Start()
    {
        endScreen.SetActive(false);
    }

    void SetWinText()
    {
        winText.text = "YOU WIN!!";
    }

    void ShowEndScreen()
    {
        endScreen.SetActive(true);

    }

}
