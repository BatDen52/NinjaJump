using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _platform;
    [SerializeField] private Transform _background;

    private float _delay;

    private void Start()
    {
        transform.position = new Vector3(0, -Camera.main.orthographicSize, 0);
    }

    private void FixedUpdate()
    {
        if (_delay < 0 && Input.GetMouseButton(0))
        {
            _delay = 0.5f;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray))
            {
                Vector3 point = ray.GetPoint(_background.position.z - Camera.main.transform.position.z);
                Spawn(point);
            }
        }
        _delay -= Time.deltaTime;
    }

    public void Spawn(Vector3 point)
    {
        Destroy(Instantiate(_platform, new Vector3(point.x, transform.position.y, 0), Quaternion.identity).gameObject, 10);
    }
}
