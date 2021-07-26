using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementAllShuriken : Achievement
{
    [SerializeField] private CoinSpawn _spawner;
    [SerializeField] private Arsenal _arsenal;
    [SerializeField] private SwitchState _state;

    private int _takedCount;
    private int _spawnedCount;

    private void OnEnable()
    {
        _spawner.CoinSpawned += OnSpawnCoin;
        _arsenal.TakeAmmunition += OnTakeAmmunition;
        _state.SpawnEnemy += OnSpawnEnemy;
    }

    private void OnDisable()
    {
        _spawner.CoinSpawned -= OnSpawnCoin;
        _arsenal.TakeAmmunition -= OnTakeAmmunition;
        _state.SpawnEnemy -= OnSpawnEnemy;
    }

    private void OnSpawnCoin(int count)
    {
        _spawnedCount = count;
        _takedCount = 0;
    }

    private void OnSpawnEnemy(Enemy enemy)
    {
        if (_takedCount == _spawnedCount)
        {
            AddCurrentValue(1);
            gameObject.SetActive(false);
        }
    }

    private void OnTakeAmmunition()
    {
        _takedCount++;
    }
}
