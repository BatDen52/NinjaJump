using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementJump : Achievement
{
    [SerializeField] private PlayerJumper _targetJump;
    [SerializeField] private int _targetValue;

    private void OnEnable()
    {
        _targetJump.Failed += CheckDistance;
    }

    private void OnDisable()
    {
        _targetJump.Failed -= CheckDistance;
    }

    private void CheckDistance()
    {
        if ((int)_targetJump.transform.position.x <= _targetValue)
            AddCurrentValue(1);
    }
}
