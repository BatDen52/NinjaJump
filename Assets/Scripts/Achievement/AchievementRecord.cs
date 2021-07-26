using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementRecord : Achievement
{
    [SerializeField] private EndGame _endGame;

    private void OnEnable()
    {
        _endGame.NewRecord += OnNewRecord;
    }

    private void OnDisable()
    {
        _endGame.NewRecord -= OnNewRecord;
    }

    private void OnNewRecord()
    {
        AddCurrentValue(1);
    }
}
