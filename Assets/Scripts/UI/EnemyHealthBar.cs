using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : Bar
{
    private Enemy _target;
    private Vector3 _offset;
    private RectTransform _rectTransform;

    private void Update()
    {
        _rectTransform.position = (_target?.transform?.position ?? Vector2.zero) + _offset;
    }

    public void Init(Enemy target, Vector3 offset)
    {
        _target = target;
        _offset = offset;
        _rectTransform = GetComponent<RectTransform>();

        Slider = gameObject.GetComponent<Slider>();
        Slider.value = 1;

        _target.HealthChanged += OnValueChenged;
        _target.Die += OnDie;
    }

    private void OnDisable()
    {
        _target.HealthChanged -= OnValueChenged;
        _target.Die -= OnDie;
    }

    private void OnDie()
    {
        Destroy(gameObject);
    }
}
