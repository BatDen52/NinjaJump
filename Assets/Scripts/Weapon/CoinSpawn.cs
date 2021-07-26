using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinSpawn : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private GameObject _coin;
    [SerializeField] private SwitchState _player;

    public event UnityAction<int> CoinSpawned;

    private void OnEnable()
    {
        float yDelay = Camera.main.orthographicSize/2f;

        int count = _player.BossDistance / 15;
        count += (int)System.Math.Ceiling(Random.Range(-count * _delay, count * _delay));

        float steep = _player.BossDistance / count;

        float position = _player.transform.position.x;
        for (int i = 0; i < count; i++)
        {
            position += steep;
            Instantiate(_coin, 
                new Vector2(position + Random.Range(-steep*0.35f, steep * 0.35f), transform.position.y + Random.Range(-yDelay, yDelay)),
                Quaternion.identity).GetComponent<Coin>().Init(_player);
        }

        CoinSpawned?.Invoke(count);
    }
}
