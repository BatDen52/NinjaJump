using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkinSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] _skins;

    void Start()
    {
        _skins[Random.Range(0, _skins.Length)].SetActive(true);
    }
}
