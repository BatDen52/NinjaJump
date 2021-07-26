using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Border : MonoBehaviour
{
    public event UnityAction Touch;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enabled && collision.GetComponent<PlayerJumper>() != null)
        {
            Touch?.Invoke();
        }
    }
}
