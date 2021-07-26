using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinSwitcher : MonoBehaviour
{
    [SerializeField] private List<Skin> _skins;

    public List<Skin> Skins => _skins;

    public void ChooseSkin(int index)
    {
        foreach (var item in _skins)
        {
            item.gameObject.SetActive(false);
            item.SetChoice(false);
        }

        _skins[index].gameObject.SetActive(true);
        _skins[index].SetChoice(true);
    }

    public int ChooseSkin(Skin skin)
    {
        int index = _skins.FindIndex(x => skin == x);

        ChooseSkin(index);

        return index;
    }
}
