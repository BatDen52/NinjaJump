using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private SwitchState _player;

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, _player.transform.position)<-15)
        {
            Destroy(gameObject);
        }
    }

    public void Init(SwitchState player)
    {
        _player = player;
    }
}
