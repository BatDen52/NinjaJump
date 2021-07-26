using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AchievementView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _action;

    private Achievement _achievement;

    public event UnityAction<Achievement> TakeReward;

    private void OnEnable()
    {
        _action.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _action.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Achievement achievement)
    {
        _achievement = achievement;

        _icon.sprite = _achievement.Icon;

        if (_achievement.IsRewardAvailavle)
            _action.gameObject.SetActive(true);
        else
            _action.gameObject.SetActive(false);
    }

    private void OnButtonClick()
    {
        if (_achievement.IsRewardAvailavle)
        {
            TakeReward?.Invoke(_achievement);
            Render(_achievement);
        }
    }
}
