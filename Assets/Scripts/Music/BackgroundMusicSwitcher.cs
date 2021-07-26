using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicSwitcher : MonoBehaviour
{
    [SerializeField] private MenuUI _menu;
    [SerializeField] private SwitchState _player;

    [SerializeField] private AudioClip[] _menuClip;
    [SerializeField] private AudioClip[] _gameClip;
    [SerializeField] private AudioClip[] _enemyClip;
    [SerializeField] private AudioClip _faileClip;

    private Enemy _enemy;
    private AudioSource _source;
    private int _currentGameClip;

    private void OnEnable()
    {
        _menu.StartGame += OnStartGame;
        _menu.StopGame += OnStopGame;
        _player.SpawnEnemy += OnSpawnEnemy;
        _player.GetComponent<PlayerAttacker>().Failed += OnFailed;
        _player.GetComponent<PlayerJumper>().Failed += OnFailed;

        _source = GetComponent<AudioSource>();
        _source.loop = true;

        _currentGameClip = Random.Range(0, _gameClip.Length);
    }

    private void OnDisable()
    {
        _menu.StartGame -= OnStartGame;
        _menu.StopGame -= OnStopGame;
        try
        {
            _player.GetComponent<PlayerAttacker>().Failed -= OnFailed;
            _player.GetComponent<PlayerJumper>().Failed -= OnFailed;
        }
        catch { }
    }

    private void OnStartGame()
    {
        StartGame();
    }

    public void StartGame()
    {
        _source.clip = _gameClip[_currentGameClip];
        _source.Play();
    }

    private void OnStopGame()
    {
        _source.clip = _menuClip[Random.Range(0, _menuClip.Length)];
        _source.Play();
    }

    private void OnSpawnEnemy(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.Die += OnEnemyDie;

        _source.clip = _enemyClip[Random.Range(0, _enemyClip.Length)];
        _source.Play();
    }

    private void OnEnemyDie()
    {
        _enemy.Die -= OnEnemyDie;
        OnStartGame();
    }

    private void OnFailed()
    {
        _source.loop = false;
        _source.clip = _faileClip;
        _source.Play();
    }
}
