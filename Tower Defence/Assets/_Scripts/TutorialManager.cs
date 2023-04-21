using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutPanel;
    public bool showTut = true;
    public Animator anim;
    public static bool didTutorial = false;

    public TutSteps currentStep;
    public enum TutSteps
    {
        BuyTower,
        PlaceTower,
        StartWave,
        WaitTillWaveEnd,
        RightClickTower,
        HaveFun
    }

    #region Singleton
    public static TutorialManager Instance { get; private set; }
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

    private void Start()
    {
        if (showTut && !didTutorial)
        {
            tutPanel.SetActive(true);
        }
        else if(!showTut || didTutorial){ tutPanel.SetActive(false); }
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) { tutPanel.SetActive(false); }
    }
    public void ContinueTut()
    {
        anim.SetTrigger("Continue");
    }

    public void OnBuyTower()
    {
        if (currentStep == TutSteps.BuyTower)
        {
            ContinueTut();

            currentStep = TutSteps.PlaceTower;
        }
    }

    public void OnPlaceTower()
    {
        if (currentStep == TutSteps.PlaceTower)
        {
            ContinueTut();

            currentStep = TutSteps.StartWave;
        }
    }

    // This method is called when the player starts the wave
    public void OnStartWave()
    {
        if (currentStep == TutSteps.StartWave)
        {
            ContinueTut();

            currentStep = TutSteps.WaitTillWaveEnd;
        }
    }

    // This method is called when the player starts the wave
    public void OnWaitTillWaveEnd()
    {
        if (currentStep == TutSteps.WaitTillWaveEnd)
        {
            ContinueTut();

            currentStep = TutSteps.RightClickTower;
        }
    }

    // This method is called when the player right-clicks on a tower
    public void OnRightClickTower()
    {
        if (currentStep == TutSteps.RightClickTower)
        {
            ContinueTut();
            didTutorial = true;

            currentStep = TutSteps.HaveFun;
        }
    }

}

