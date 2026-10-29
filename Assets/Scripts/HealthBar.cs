using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        
        
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        

        
    }
    private void Awake()
    {
        if (slider == null)
            slider = GetComponentInChildren<Slider>();

        if (fill == null && slider != null)
            fill = slider.fillRect.GetComponent<Image>();
    }


}