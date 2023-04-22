using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool didWin = false;
    public GameObject endScreen;
    public TextMeshProUGUI winText;

    public GameObject StartScreen;


    bool normalSpeed = true;
    bool started = false;


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
        StartScreen.SetActive(true);
        endScreen.SetActive(false);

        Time.timeScale = 0;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && started == false) { StartScreen.SetActive(false); Time.timeScale = 1; started = true; }    
    }


    void SetWinText()
    {
        winText.text = "YOU WIN!!";
    }

    void ShowEndScreen()
    {
        endScreen.SetActive(true);

    }

    public void ToggleTime()
    {
        normalSpeed= !normalSpeed;


        if(normalSpeed) { Time.timeScale = 1f; }
        else if (!normalSpeed) { Time.timeScale = 2f; }

    }

}
