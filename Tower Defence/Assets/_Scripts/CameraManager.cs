using MilkShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public ShakeInstance shakeInstance;
    public ShakePreset shakePreset;
    public ShakePreset explosionPreset;


    #region Singleton
    public static CameraManager Instance { get; private set; }
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
        Actions.OnEnemyKilled += ShakeCamera;
    }
    private void OnDisable()
    {
        Actions.OnEnemyKilled -= ShakeCamera;

    }

    void Start()
    {
        shakeInstance = Shaker.ShakeAll(shakePreset);
    }



    void ShakeCamera()
    {

      //  shakeInstance = Shaker.ShakeAll(explosionPreset);
    }

    public void PunchShake(float strength)
    {
        ShakePreset thisShake = explosionPreset;
        thisShake.Strength = strength;

        shakeInstance = Shaker.ShakeAll(thisShake);
    }

}
