using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] private GameObject[] _background;

    private GameObject _tempBackground;
    private float _offsetDistance;

    private void Start()
    {
        _offsetDistance = _background[1].transform.position.x - _background[0].transform.position.x;
    }

    void Update()
    {
        if (_background[1].transform.position.x < transform.position.x)
        {
            _background[0].transform.position = new Vector3(_background[0].transform.position.x + _offsetDistance * 3, _background[0].transform.position.y, _background[0].transform.position.z);

            _tempBackground = _background[2];
            _background[2] = _background[0];
            _background[0] = _background[1];
            _background[1] = _tempBackground;
        }
    }
}
