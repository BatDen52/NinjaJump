using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AchievementSystem : MonoBehaviour
{
    [SerializeField] private Image _rewardAlert;

    private Achievement[] _achievements;
    
    public Achievement[] Achievements => _achievements;

    public event UnityAction AchievementListChenged;
    
    public void Save()
    {
        StringBuilder content = new StringBuilder(100);

        foreach (Achievement achieve in _achievements)
        {
            if (content.Length > 0)
                content.Append("|");

            content.Append(string.Format("{0};{1};{2}", achieve.CurrentValue, achieve.CurrentStep, achieve.CountReward));
        }

        PlayerPrefs.SetString("Achievements", content.ToString());
        PlayerPrefs.Save();

    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey("Achievements")) return;

        string[] content = PlayerPrefs.GetString("Achievements").Split(new char[] { '|' });

        if (content.Length == 0 || content.Length != _achievements.Length) return;

        string[] item;

        for (int i = 0; i < _achievements.Length; i++)
        {
            item = content[i].Split(new char[] { ';' });

            _achievements[i].Init(Parse(item[0]), Parse(item[1]), Parse(item[2]));

            if (_achievements[i].IsAchieved && !_achievements[i].IsMultiAchievement)
                _achievements[i].gameObject.SetActive(false);
        }
    }

    private int Parse(string text)
    {
        int value;
        if (int.TryParse(text, out value)) return value;
        return -1;
    }

    private void OnEnable()
    {
        _achievements = GetComponentsInChildren<Achievement>();

        foreach (var achievement in _achievements)
            achievement.StatusChenged += OnStatusChenged;
    
        Load();

        OnStatusChenged();
    }

    private void OnDisable()
    {
        foreach (var achievement in _achievements)
            achievement.StatusChenged -= OnStatusChenged;
    }

    private void OnStatusChenged()
    {
        int rewardCount = _achievements.Where(x => x.IsRewardAvailavle).Count();
        if (_rewardAlert)
            _rewardAlert.gameObject.SetActive(rewardCount != 0);

        Save();

        AchievementListChenged?.Invoke();
    }

    public void ClearProgres()
    {
        PlayerPrefs.DeleteAll();
    }
}