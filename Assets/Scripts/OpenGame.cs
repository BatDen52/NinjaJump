using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OpenGame : MonoBehaviour
{
    [SerializeField] private PlayerSkinSwitcher _player;
    [SerializeField] private AudioMixerGroup Mixer;

    private void Awake()
    {
        Time.timeScale = 0f;
        _player.ChooseSkin(PlayerPrefs.GetInt("CurrentSkin"));
    }

    private void Start()
    {
        Mixer.audioMixer.SetFloat("ValueMusic", PlayerPrefs.GetInt("EnableMusic") * -80);
        Mixer.audioMixer.SetFloat("ValueSound", PlayerPrefs.GetInt("EnableSound") * -80);
    }
}
