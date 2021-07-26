using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveUp : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Update()
    {
        transform.Translate(new Vector3(0, _speed * Time.deltaTime, 0));
    }
}
