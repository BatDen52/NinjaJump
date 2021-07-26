using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private Transform _ground;

    public event UnityAction Failed;

    void Start()
    {
        _ground = FindObjectOfType<PlatformSpawn>().transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.y < _ground.transform.position.y)
        {
            Failed?.Invoke();
            enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.GetComponent<PlatformMoveUp>() != null)
        {
            _rigidbody.velocity = new Vector2(0, 0);

            if (-Camera.main.orthographicSize / 2 > collision.gameObject.transform.position.y)//60
                _rigidbody.AddForce(new Vector2(_jumpForce / 2, Mathf.Sqrt(3) / 2 * _jumpForce));
            else
            if (Camera.main.orthographicSize / 2 < collision.gameObject.transform.position.y)//30
                _rigidbody.AddForce(new Vector2(Mathf.Sqrt(3) / 2 * _jumpForce, _jumpForce / 2));
            else
                _rigidbody.AddForce(new Vector2(Mathf.Sqrt(2) / 2 * _jumpForce, Mathf.Sqrt(2) / 2 * _jumpForce));//45
        }
    }
}
