using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Store : MonoBehaviour
{
    [SerializeField] private PlayerSkinSwitcher _skins;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private SkinView _template;
    [SerializeField] private GameObject _container;
    [SerializeField] private TMP_Text _money;

    private List<SkinView> _skinViews;

    public event UnityAction Buyed; 

    private void OnEnable()
    {
        OnMoneyChenged();
        _wallet.MoneyChenged += OnMoneyChenged;

        _skinViews = new List<SkinView>();

        for (int i = 0; i < _skins.Skins.Count; i++)
        {
            if (PlayerPrefs.GetInt(_skins.Skins[i].name) == 1)
                _skins.Skins[i].Buy();

            AddItem(_skins.Skins[i]);
        }
    }

    private void OnMoneyChenged()
    {
        _money.text = _wallet.Money.ToString();
    }

    public void OnChooseSkin(Skin skin)
    {
        PlayerPrefs.SetInt("CurrentSkin", _skins.ChooseSkin(skin));

        RefreshStore();
    }

    private void RefreshStore()
    {
        for (int i = 0; i < _skins.Skins.Count; i++)
            _skinViews[i].Render(_skins.Skins[i]);
    }

    public void OnBuySkin(Skin skin)
    {
        if (_wallet.Buy(skin.Price))
        {
            skin.Buy();
            Buyed?.Invoke();
        }
        RefreshStore();
    }

    private void AddItem(Skin skin)
    {
        var view = Instantiate(_template, _container.transform);

        view.Render(skin);
        view.ChooseSkin += OnChooseSkin;
        view.BuySkin += OnBuySkin;

        _skinViews.Add(view);
    }

    private void OnDisable()
    {
        foreach (var item in _skinViews)
        {
            item.ChooseSkin -= OnChooseSkin;
            item.BuySkin -= OnBuySkin;
            Destroy(item.gameObject);
        }
        _wallet.MoneyChenged -= OnMoneyChenged;
    }
}
