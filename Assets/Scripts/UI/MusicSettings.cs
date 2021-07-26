using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup Mixer;

    [SerializeField] private Image _musicImage;
    [SerializeField] private Image _soundImage;

    [SerializeField] private Sprite[] _musicIcons;
    [SerializeField] private Sprite[] _soundIcons;

    private int _enableMusic;
    private int _enableSound;

    private void OnEnable()
    {
        _enableMusic = PlayerPrefs.GetInt("EnableMusic");
        _enableSound = PlayerPrefs.GetInt("EnableSound");

        _musicImage.sprite = _musicIcons[_enableMusic];
        _soundImage.sprite = _soundIcons[_enableSound];
    }

    public void ChangeEnableMusic()
    {
        ChangeEnable("Music", _musicImage,_musicIcons,ref _enableMusic);
    }

    public void ChangeEnableSound()
    {
        ChangeEnable("Sound", _soundImage, _soundIcons, ref _enableSound);
    }

    private void ChangeEnable(string parameter, Image image, Sprite[] icons, ref int enabled)
    {
        if (enabled == 1)
        {
            enabled = 0;
            Mixer.audioMixer.SetFloat("Value" + parameter, 0);
        }
        else
        {
            enabled = 1;
            Mixer.audioMixer.SetFloat("Value" + parameter, -80);
        }

        image.sprite = icons[enabled];
        PlayerPrefs.SetInt("Enable" + parameter, enabled);
    }
}
