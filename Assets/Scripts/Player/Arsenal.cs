using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arsenal : MonoBehaviour
{
    private int _ammunition;

    public int AmmunitionCount => _ammunition;
    public bool CanAttack => _ammunition > 0;

    public event UnityAction<int> ChangeAmmunition;
    public event UnityAction TakeAmmunition;

    void Start()
    {
        _ammunition = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled && collision.GetComponent<Coin>() != null)
        {
            SetAmmunitionCount(_ammunition+1);
            Destroy(collision.gameObject);
        }
    }

    public void SetAmmunitionCount(int count)
    {
        _ammunition=count;
        TakeAmmunition?.Invoke();
        ChangeAmmunition?.Invoke(_ammunition);
    }

    public void Attack()
    {
        _ammunition--;
        ChangeAmmunition?.Invoke(_ammunition);
    }
}
