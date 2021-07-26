using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameHood;
    [SerializeField] private GameObject _coinSpawner;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private JumpTutorial _jumpTutorial;
    [SerializeField] private AttackTutorial _attackTutorial;

    public event UnityAction StartGame;
    public event UnityAction StopGame;

    private void Start()
    {
        StopGame?.Invoke();

        _score.text = PlayerPrefs.GetInt("MaxScore").ToString();
    }

    public void Play()
    {
        StartGame?.Invoke();

        if (PlayerPrefs.GetInt("JumpTutorial") == 0)
            _jumpTutorial.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("AttackTutorial") == 0)
            _attackTutorial.gameObject.SetActive(true);

        _mainMenu.SetActive(false);
        _gameHood.SetActive(true);
        _coinSpawner.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenPanel(GameObject panel)
    {
        _mainMenu.SetActive(false);
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        _mainMenu.SetActive(true);
    }
}
