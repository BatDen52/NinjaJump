using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementFail : Achievement
{
    [SerializeField] private PlayerJumper _targetJump;
    [SerializeField] private PlayerAttacker _targetAttack;

    private void OnEnable()
    {
        _targetAttack.Failed += OnFaile;
        _targetJump.Failed += OnFaile;
    }

    private void OnDisable()
    {
        _targetAttack.Failed -= OnFaile;
        _targetJump.Failed -= OnFaile;
    }

    private void OnFaile()
    {
        AddCurrentValue(1);
    }
}
