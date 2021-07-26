using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _title;
    [SerializeField] private int _price;
    [SerializeField] private bool _isChosen;
    [SerializeField] private bool _isBought;

    public Sprite Icon => _icon;
    public string Title => _title;
    public int Price => _price;
    public bool IsChosen => _isChosen;
    public bool IsBought => _isBought;

    public void Buy()
    {
        PlayerPrefs.SetInt(gameObject.name, 1);
        _isBought = true;
    }

    public void SetChoice(bool isChosen)
    {
        _isChosen = isChosen;
    }
}
