using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Enemy))]
public class EnemyDamageSound : MonoBehaviour
{
    private Enemy _enemy;
    private AudioSource _audio;

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _audio = GetComponent<AudioSource>();

        _enemy.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _enemy.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int current,int max)
    {
        _audio.Play();
    }
}
