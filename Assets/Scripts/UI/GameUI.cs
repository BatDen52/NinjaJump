using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _ammunition;
    [SerializeField] private Arsenal _arsenal;
    [SerializeField] private Image _pauseImage;
    [SerializeField] private Sprite _pauseIcon;
    [SerializeField] private GameObject _pausePanel;

    private bool _isPlay;
    private Sprite _tempPauseIcon;

    public int Score { get; private set; }

    private void OnEnable()
    {
        _isPlay = true;
        _arsenal.ChangeAmmunition += OnTakeCoin;
    }

    private void OnDisable()
    {
        _arsenal.ChangeAmmunition -= OnTakeCoin;
    }

    private void OnTakeCoin(int money)
    {
        _ammunition.text = money.ToString();
    }

    private void FixedUpdate()
    {
        Score = (int)_arsenal.transform.position.x;
        _score.text = Score.ToString();
    }

    public void Pause()
    {
        if (_isPlay)
        {
            Time.timeScale = 0f;
            _pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            _pausePanel.SetActive(false);
        }

        _tempPauseIcon = _pauseImage.sprite;
        _pauseImage.sprite = _pauseIcon;
        _pauseIcon = _tempPauseIcon;

        _isPlay = !_isPlay;
    }

    private void OnApplicationFocus(bool focus)
    {
        _isPlay = true;
        Pause();
    }
}
