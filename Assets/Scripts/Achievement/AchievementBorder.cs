using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementBorder : Achievement
{
    [SerializeField] private Border _border;
    [SerializeField] private SwitchState _stateSwitcher;

    private void OnEnable()
    {
        _border.Touch += OnTouched;
        _stateSwitcher.SwitchToAttak += BorderDeactivator;
        _stateSwitcher.SwitchToJump += BorderActivator;
    }

    private void OnDisable()
    {
        _border.Touch -= OnTouched;
        _stateSwitcher.SwitchToAttak -= BorderDeactivator;
        _stateSwitcher.SwitchToJump -= BorderActivator;
    }

    private void OnTouched()
    {
        AddCurrentValue(1);
    }

    private void BorderActivator()
    {
        _border.gameObject.SetActive(true);
    }

    private void BorderDeactivator()
    {
        _border.gameObject.SetActive(false);
    }
}