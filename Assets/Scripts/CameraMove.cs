using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private PlayerJumper player;
    private float _offset;

    void Start()
    {
        player = FindObjectOfType<PlayerJumper>();
        _offset = transform.position.x - player.transform.position.x;
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + _offset, transform.position.y, transform.position.z);
    }
}
