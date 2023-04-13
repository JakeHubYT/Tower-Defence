using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health healthScript;

    public Slider healthSlider;

    public void UpdateHealthUI()
    {

        healthSlider.value = healthScript.GetCurrentHealth();
    }
}
