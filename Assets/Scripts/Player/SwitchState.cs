using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchState : MonoBehaviour
{
    [SerializeField] private int _bossDistance;
    [SerializeField] private PlatformSpawn _spawnPlatform;
    [SerializeField] private GameObject _platformTamplate;
    [SerializeField] private GameObject _bigPlatform;
    [SerializeField] private GameObject _enemyTamplate;
    [SerializeField] private CoinSpawn _coinSpawner;

    private PlayerJumper _jumper;
    private PlayerAttacker _attacker;
    private BackgroundMover _background;
    private Rigidbody2D _rigidbody;

    private float _lastPos;
    private Enemy _enemy;

    private int _enemyHP;
    private int EnemyHP
    {
        get { return _enemyHP; }
        set
        {
            if (value <= 10)
                _enemyHP = value;
        }
    }

    public int BossDistance => _bossDistance;

    public event UnityAction<Enemy> SpawnEnemy;
    public event UnityAction SwitchToJump;
    public event UnityAction SwitchToAttak;

    private void Start()
    {
        _background = GetComponent<BackgroundMover>();
        _jumper = GetComponent<PlayerJumper>();
        _attacker = GetComponent<PlayerAttacker>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _lastPos = 0;
        _enemyHP = 3;
    }

    private void Update()
    {
        if (transform.position.x - _lastPos > _bossDistance)
            ActiveAttack();
        if (_enemy?.Health <= 0)
            ActiveJump();
    }

    private void ActiveJump()
    {
        _enemy = null;

        _bigPlatform.SetActive(false);

        _rigidbody.velocity = Vector2.zero;

        Instantiate(_platformTamplate, new Vector2(transform.position.x, transform.position.y - 1), Quaternion.identity);

        _attacker.enabled = false;
        _background.enabled = true;
        _jumper.enabled = true;
        _spawnPlatform.enabled = true;
        _coinSpawner.enabled = true;

        SwitchToJump?.Invoke();
    }

    private void ActiveAttack()
    {
        _lastPos = transform.position.x;

        _background.enabled = false;
        _jumper.enabled = false;
        _spawnPlatform.enabled = false;
        _coinSpawner.enabled = false;
        _attacker.enabled = true;

        _rigidbody.velocity = Vector2.zero;

        _bigPlatform.SetActive(true);

        float enemyX = transform.position.x + Camera.main.orthographicSize * 2 * Screen.width / Screen.height - 3;
        float enemyY = Camera.main.transform.position.y + Random.Range(
             -Camera.main.orthographicSize + _enemyTamplate.transform.localScale.y * 6,
             Camera.main.orthographicSize - _enemyTamplate.transform.localScale.y * 2);

        _enemy = Instantiate(_enemyTamplate, new Vector2(enemyX, enemyY), Quaternion.identity).GetComponent<Enemy>();
        _enemy.transform.SetParent(Camera.main.transform);

        float enemySpeed = (_enemy.transform.position.x - transform.position.x) / 17f;

        _enemy.Init(EnemyHP++, enemySpeed);

        SpawnEnemy?.Invoke(_enemy);
        SwitchToAttak?.Invoke();
    }
}
