using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class HealthBar : MonoBehaviour
{
    
    [SerializeField] Slider _slider;
    [SerializeField] HealthSystem _healthSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _healthSystem.OnDamaged += HealthSystem_OnDamaged;
        _healthSystem.OnHealed += HealthSystem_OnHealed;
       // _healthSystem.OnHealthMaxChange += HealthSystem_OnHealthMaxChange;
    }

    // Update is called once per frame
    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        SetSliderValue();
    }
    
    private void HealthSystem_OnHealed(object sender, EventArgs e)
    {
        SetSliderValue();
    }

    private void SetSliderValue()
    {
        _slider.value = _healthSystem.GetHealthAmountNormalized();
    }
}
