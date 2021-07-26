using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ArsenalSound : MonoBehaviour
{
    [SerializeField] private PlayerAttacker _player;
    [SerializeField] private Arsenal _arsenal;

    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private AudioClip _takeClip;

    private AudioSource _source;

    private void OnEnable()
    {
        _player.Shooting += OnShooting;
        _arsenal.TakeAmmunition += OnTaking;
    }

    private void OnDisable()
    {
        _player.Shooting -= OnShooting;
        _arsenal.TakeAmmunition -= OnTaking;
    }

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnShooting()
    {
        _source.clip = _shootClip;
        _source.Play();
    }

    private void OnTaking()
    {
        _source.clip = _takeClip;
        _source.Play();
    }
}
