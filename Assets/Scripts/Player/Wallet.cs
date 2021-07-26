using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    private int _money;

    public int Money => _money;

    public event UnityAction MoneyChenged;

    private void OnEnable()
    {
        _money = PlayerPrefs.GetInt(nameof(Money));
    }

    public void AddMoney(int coinCount)
    {
        _money += coinCount;
        MoneyChenged?.Invoke();
        PlayerPrefs.SetInt(nameof(Money), Money);
    }

    public bool Buy(int coinCount)
    {
        if (coinCount <= _money)
        {
            _money -= coinCount;
            MoneyChenged?.Invoke();
            PlayerPrefs.SetInt(nameof(Money), Money);

            return true;
        }

        return false;
    }
}
