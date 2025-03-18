using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject _oldestParent;

    private ICharacterHealth _characterHealth;
    private Slider _slider;


    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _characterHealth = _oldestParent.GetComponent<ICharacterHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = (float) _characterHealth.GetCurrentHealth() / (float) _characterHealth.GetMaxHealth();
    }
}
