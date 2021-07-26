using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementWinEmpty : Achievement
{
    [SerializeField] private Arsenal _arsenal;
    [SerializeField] private SwitchState _state;

    private int _takedCount;
    private Enemy _enemy;

    private void OnEnable()
    {
        _takedCount = 0;
        _arsenal.TakeAmmunition += OnTakeAmmunition;
        _state.SpawnEnemy += OnEnemySpawned;
    }

    private void OnDisable()
    {
        _arsenal.TakeAmmunition -= OnTakeAmmunition;
        _state.SpawnEnemy -= OnEnemySpawned;
        if (_enemy)
            _enemy.Die -= OnEnemyDie;
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.Die += OnEnemyDie;
    }

    private void OnTakeAmmunition()
    {
        _takedCount++;
    }

    private void OnEnemyDie()
    {
        _enemy.Die -= OnEnemyDie;
        if (_takedCount == 0)
        {
            AddCurrentValue(1);
            gameObject.SetActive(false);
        }
        _takedCount = 0;
    }
}
