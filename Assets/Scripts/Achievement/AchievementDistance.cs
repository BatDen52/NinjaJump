using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementDistance : Achievement
{
    [SerializeField] private PlayerJumper _targetJump;
    [SerializeField] private PlayerAttacker _targetAttack;

    private void OnEnable()
    {
        _targetAttack.Failed += CheckDistance;
        _targetJump.Failed += CheckDistance;
    }

    private void OnDisable()
    {
        _targetAttack.Failed -= CheckDistance;
        _targetJump.Failed -= CheckDistance;
    }

    private void CheckDistance()
    {
        if (_targetJump.transform.position.x >= TargetValue)
            AddCurrentValue((int)_targetJump.transform.position.x);
    }
}
