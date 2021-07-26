using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkinView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private Button _action;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _buyClip;

    private Skin _skin;

    public event UnityAction<Skin> ChooseSkin;
    public event UnityAction<Skin> BuySkin;

    private void OnEnable()
    {
        _action.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _action.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Skin skin)
    {
        _skin = skin;

        _icon.sprite = _skin.Icon;

        _action.interactable = true;


        if (_skin.IsBought)
        {
            if (_skin.IsChosen)
            {
                _buttonText.text = "Выбрано";
                _action.interactable = false;
            }
            else
            {
                _buttonText.text = "Выбрать";
            }
        }
        else
        {
            _buttonText.text = _skin.Price.ToString();
        }
    }

    private void OnButtonClick()
    {
        if (_skin.IsBought)
        {
            if (!_skin.IsChosen)
            {
                ChooseSkin?.Invoke(_skin);
                _action.interactable = false;
            }
        }
        else
        {
            BuySkin?.Invoke(_skin);
            if (_skin.IsBought)
                _source.PlayOneShot(_buyClip);
        }
        Render(_skin);
    }
}
