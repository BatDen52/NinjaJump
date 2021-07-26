using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpTutorial : MonoBehaviour
{
    [SerializeField] private Button _jumpClickPointer;
    [SerializeField] private PlayerJumper _player;
    [SerializeField] private PlatformSpawn _spawner;

    private bool _firstJump;
    private bool _secondJump;

    private void Update()
    {
        if (_player.transform.position.x >= 4 && !_firstJump)
        {
            Time.timeScale = 0f;
            _jumpClickPointer.gameObject.SetActive(true);
            _firstJump = true;
        }
        if (_player.transform.position.x >= 15 && !_secondJump)
        {
            Time.timeScale = 0f;
            _jumpClickPointer.gameObject.SetActive(true);
            _secondJump = true;
        }
    }

    public void SpawnPlatfotm()
    {
        _spawner.Spawn(_jumpClickPointer.transform.position);
        Time.timeScale = 1f;
        _jumpClickPointer.gameObject.SetActive(false);
        if (_secondJump)
        {
            PlayerPrefs.SetInt("JumpTutorial", 1);
            gameObject.SetActive(false);
        }
    }
}
