using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _hpBar;

    private int _health;
    private int _currentHealth;
    private float _speed;

    public int Health => _currentHealth;
    public int MaxHealth => _health;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction Die;

    private void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime/*, Space.World*/);
    }

    public void Init(int health, float speed)
    {
        _health = health;
        _currentHealth = _health;
        _speed = speed;
        Instantiate(_hpBar, FindObjectOfType<Canvas>()?.transform.GetChild(0)).GetComponent<EnemyHealthBar>().Init(this, new Vector3(0,-1.2f,0));
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth == 0)
        {
            Die?.Invoke();
            Destroy(gameObject);
        }
    }
}
