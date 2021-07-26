using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private PlayerJumper _jumper;
    [SerializeField] private PlayerAttacker _attacker;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject _gameHood;
    [SerializeField] private TMP_Text _lableScore;
    [SerializeField] private GameObject _platformTamplate;
    [SerializeField] private AchievementSystem _achievementSystem;
    [SerializeField] private BackgroundMusicSwitcher _musicSwitcher;
    [SerializeField] private TestConnection _testConnection;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Wallet _wallet;

    private int _attemptCount;
    private Enemy _enemy;
    private int _score;
    private int _maxScore;

    public event UnityAction NewRecord;

    private void OnDied(Enemy enemy)
    {
        _enemy = enemy;
    }

    private void OnEnded()
    {
        if (_testConnection.IsConnection)
        {
            if (_attemptCount == 0)
            {
                _buttons[0].gameObject.SetActive(true);
                _buttons[1].gameObject.SetActive(false);
            }
            else if (_attemptCount == 1)
            {
                _buttons[0].gameObject.SetActive(true);
                if (_wallet.Money >= 30)
                    _buttons[1].gameObject.SetActive(true);
            }
            else
            {
                _buttons[0].gameObject.SetActive(false);
                _buttons[1].gameObject.SetActive(false);
            }
        }
        else
        {
            if (_attemptCount == 0 && _wallet.Money >= 30)
            {
                _buttons[0].gameObject.SetActive(false);
                _buttons[1].gameObject.SetActive(true);
            }
            else
            {
                _buttons[0].gameObject.SetActive(false);
                _buttons[1].gameObject.SetActive(false);
            }
        }

        _gameHood.SetActive(false);
        _endPanel.SetActive(true);

        Time.timeScale = 0f;

        _score = (_gameHood.GetComponent<GameUI>()).Score;
        _maxScore = PlayerPrefs.GetInt("MaxScore");

        _lableScore.text = $"Конец игры!\nТвой результат: {_score}\nЛучший результат: {_maxScore}";
    }

    public void ToMainMenu()
    {
        if (_score > _maxScore)
        {
            NewRecord?.Invoke();
            PlayerPrefs.SetInt("MaxScore", _score);
        }
        SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        _jumper.Failed += OnEnded;
        _attacker.Failed += OnEnded;
        _attacker.Die += OnDied;
    }

    private void OnDisable()
    {
        _jumper.Failed -= OnEnded;
        _attacker.Failed -= OnEnded;
        _attacker.Die -= OnDied;
    }

    public void ReturnToGame(bool returnForCoins)
    {
        if (returnForCoins)
            if(!_wallet.Buy(30)) return;
       
        _attemptCount++;

        _gameHood.SetActive(true);
        _endPanel.SetActive(false);

        foreach (var achievement in _achievementSystem.Achievements.Where(x => x is AchievementBoss))
            achievement.gameObject.SetActive(false);

        _jumper.transform.position = new Vector2(_jumper.transform.position.x, 0);
        _jumper.enabled = true;

        if (_enemy)
            _enemy.TakeDamage(_enemy.MaxHealth);
        else
            Instantiate(_platformTamplate, new Vector2(_jumper.transform.position.x, _jumper.transform.position.y - 1), Quaternion.identity);

        _musicSwitcher.StartGame();

        Time.timeScale = 1f;
    }
}
