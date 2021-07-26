using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementBoss : Achievement
{
    [SerializeField] private SwitchState _stateSwitcher;

    private Enemy _enemy;

    private void OnEnable()
    {
        _stateSwitcher.SpawnEnemy += OnEnemySpawned;
    }

    private void OnDisable()
    {
        _stateSwitcher.SpawnEnemy -= OnEnemySpawned;
        if (_enemy)
            _enemy.Die -= OnEnemyDie;
    }

    private void OnEnemyDie()
    {
        if ((int)_stateSwitcher.transform.position.x >= TargetValue)
            if (CurrentStep == 0)
                AddCurrentValue((int)_stateSwitcher.transform.position.x);
            else
                AddCurrentValue(_stateSwitcher.BossDistance);
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.Die += OnEnemyDie;
    }
}