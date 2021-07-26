using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllAchievement : Achievement
{
    [SerializeField] private AchievementSystem _achievements;

    private void OnEnable()
    {
        _achievements.AchievementListChenged += OnStatusChenged;
    }

    private void OnDisable()
    {
        _achievements.AchievementListChenged -= OnStatusChenged;
    }

    private void OnStatusChenged()
    {
        if (_achievements.Achievements.Where(x => x.IsAchieved).Count() == _achievements.Achievements.Length-1)
            AddCurrentValue(1);
    }
}
