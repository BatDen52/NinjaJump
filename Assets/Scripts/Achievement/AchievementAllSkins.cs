using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementAllSkins : Achievement
{
    [SerializeField] private PlayerSkinSwitcher _skins;
    [SerializeField] private Store _store;

    private void OnEnable()
    {
        _store.Buyed += OnBuyed;
    }

    private void OnDisable()
    {
        _store.Buyed -= OnBuyed;
    }

    private void OnBuyed()
    {
        if (_skins.Skins.Where(x => x.IsBought).Count() == _skins.Skins.Count)
            AddCurrentValue(1);
    }
}