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
        if (_healthSystem != null)
        {
            _healthSystem.OnDamaged += HealthSystem_OnDamaged;
            _healthSystem.OnHealed += HealthSystem_OnHealed;
        }
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
    
    private void SetBarVisibility(bool show)
    {
        gameObject.SetActive(show);
    }
    
    public void SetHealthSystem(HealthSystem healthSystem)
    {
        _healthSystem = healthSystem;
        Debug.Log(healthSystem.GetHealthAmountNormalized());

        if (_healthSystem == null)
        {
            Debug.Log("NULL");
            SetBarVisibility(false);
        }
        else
        {
            Debug.Log("NOT NULL");
            _healthSystem.OnDamaged += HealthSystem_OnDamaged;
            _healthSystem.OnHealed += HealthSystem_OnHealed;
            SetBarVisibility(true);
            SetSliderValue();
        }
            
    }
}
