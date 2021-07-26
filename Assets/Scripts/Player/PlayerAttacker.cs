using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Arsenal))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _background;
    [SerializeField] private GameObject _weapon;

    private Transform _ground;
    private Arsenal _arsenal;
    private Rigidbody2D _rigidbody;
    private float _delay;

    public event UnityAction Failed;
    public event UnityAction<Enemy> Die;
    public event UnityAction Shooting;

    private void Start()
    {
        _ground = FindObjectOfType<PlatformSpawn>().transform;
        _arsenal = GetComponent<Arsenal>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_delay < 0 && _arsenal.CanAttack && Input.GetMouseButton(0))
        {
            _delay = 0.2f;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray))
            {
                Shoot();
            }
        }
        
        _delay -= Time.deltaTime;
        
        if (transform.position.y < _ground.transform.position.y)
        {
            Failed?.Invoke();
            enabled = false;
        }
    }

    public void Shoot()
    {
        Shooting?.Invoke();
        Destroy(Instantiate(_weapon, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity), 10);
        _arsenal.Attack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.GetComponent<PlatformMoveUp>() != null)
            Destroy(collision.gameObject);

        if (enabled && collision.gameObject.GetComponent<Platform>() != null)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(new Vector2(0, _speed * 100));
        }

        if (enabled && collision.gameObject.GetComponent<Enemy>() != null)
        {
            Die?.Invoke(collision.gameObject.GetComponent<Enemy>());
            Failed?.Invoke();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.GetComponent<PlatformMoveUp>() != null)
            Destroy(collision.gameObject);
    }
}
