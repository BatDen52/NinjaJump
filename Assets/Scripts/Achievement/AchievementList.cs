using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementList : MonoBehaviour
{
    [SerializeField] private AchievementSystem _achievements;
    [SerializeField] private AchievementView _template;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameObject _container;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private Image _rewardAlert;

    private List<AchievementView> _achievementViews;

    private void OnEnable()
    {
        OnMoneyChenged();
        _wallet.MoneyChenged += OnMoneyChenged;

        _achievementViews = new List<AchievementView>();

        for (int i = 0; i < _achievements.Achievements.Length; i++)
            AddItem(_achievements.Achievements[i]);
    }

    private void OnMoneyChenged()
    {
        _money.text = _wallet.Money.ToString();
    }

    private void RefreshStore()
    {
        for (int i = 0; i < _achievements.Achievements.Length; i++)
            _achievementViews[i].Render(_achievements.Achievements[i]);
    }

    public void OnTakeReward(Achievement achievement)
    {
        achievement.TakeReward();

        int rewardCount = _achievements.Achievements.Where(x => x.IsRewardAvailavle).Count();
        if (_rewardAlert)
            _rewardAlert.gameObject.SetActive(rewardCount != 0);

        RefreshStore();
    }

    private void AddItem(Achievement achievement)
    {
        var view = Instantiate(_template, _container.transform);

        view.Render(achievement);
        view.TakeReward += OnTakeReward;

        _achievementViews.Add(view);
    }

    private void OnDisable()
    {
        foreach (var item in _achievementViews)
        {
            item.TakeReward -= OnTakeReward;
            Destroy(item.gameObject);
        }
        _wallet.MoneyChenged -= OnMoneyChenged;
    }
}
