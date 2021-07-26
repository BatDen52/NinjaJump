using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementWitoutMiss : Achievement
{
    [SerializeField] private Arsenal _arsenal;
    [SerializeField] private SwitchState _state;

    private Enemy _enemy;
    private int _thrownCount;

    private void OnEnable()
    {
        _thrownCount = 0;
        _arsenal.ChangeAmmunition += OnChangeAmmunition;
        _state.SpawnEnemy += OnEnemySpawned;
    }

    private void OnDisable()
    {
        _arsenal.ChangeAmmunition -= OnChangeAmmunition;
        _state.SpawnEnemy -= OnEnemySpawned;
        if (_enemy)
            _enemy.Die -= OnEnemyDie;
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        _thrownCount = 0;
        _enemy = enemy;
        _enemy.Die += OnEnemyDie;
    }

    private void OnChangeAmmunition(int count)
    {
        _thrownCount++;
    }

    private void OnEnemyDie()
    {
        _enemy.Die -= OnEnemyDie;
        if (_thrownCount == _enemy.MaxHealth)
        {
            AddCurrentValue(1);
            gameObject.SetActive(false);
        }
    }
}
