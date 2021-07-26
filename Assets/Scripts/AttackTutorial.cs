using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackTutorial : MonoBehaviour
{
    [SerializeField] private Button _attackClickPointer;
    [SerializeField] private PlayerAttacker _player;
    [SerializeField] private Arsenal _arsenal;
    [SerializeField] private SwitchState _spawner;

    private Enemy _enemy;
    private float _delay;
    private int _shootCount;

    private void OnEnable()
    {
        _spawner.SpawnEnemy += OnEnemySpawn;
        GetComponent<Image>().enabled = false;
    }

    private void OnEnemySpawn(Enemy enemy)
    {
        _enemy = enemy;
        if (_arsenal.AmmunitionCount < 3)
            _arsenal.SetAmmunitionCount(3);
        GetComponent<Image>().enabled = true;
    }

    private void Update()
    {
        if (_enemy)
        {
            if (_delay <= 0)
                if (Mathf.Abs(_player.transform.position.y - _enemy.transform.position.y) < 0.5f)
                {
                    Time.timeScale = 0f;
                    _attackClickPointer.gameObject.SetActive(true);
                }
            _delay -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        _player.Shoot();
        _delay = 0.5f;
        _shootCount++;
        Time.timeScale = 1f;
        _attackClickPointer.gameObject.SetActive(false);
        PlayerPrefs.SetInt("AttackTutorial", 1);
        if (_shootCount == 3)
            gameObject.SetActive(false);
    }
}