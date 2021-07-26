using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public abstract class Achievement : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private AchievementStep[] _steps;
    [SerializeField] private bool _isMultiAchievement;

    private bool _isAchieved;
    private bool _isRewardAvailavle;
    private int _currentValue;
    private int _currentStep;
    private int _countReward;

    public Sprite Icon => _steps[_currentStep].Icon;
    public int TargetValue => _steps[_currentStep].TargetValue;
    public int CurrentValue => _currentValue;
    public bool IsAchieved => _isAchieved;
    public bool IsRewardAvailavle => _isRewardAvailavle;
    public int Reward => _steps[_currentStep-1].Reward;
    public bool IsMultiAchievement => _isMultiAchievement;
    public int StepCount => _steps.Length - 1;
    public int CurrentStep => _currentStep;
    public int CountReward => _countReward;

    public event UnityAction StatusChenged;

    public virtual void Init(int currentValue, int currentStep, int countReward)
    {
        _isAchieved = currentStep==StepCount;
        _isRewardAvailavle = countReward != 0;
        _currentValue = currentValue;
        _currentStep = currentStep;
        _countReward = countReward;
    }

    public void TakeReward()
    {
        _isRewardAvailavle = false;
        _wallet.AddMoney(Reward * _countReward);
        _countReward = 0;

        StatusChenged?.Invoke();
    }

    public void AddCurrentValue(int value)
    {
        if (!_isMultiAchievement && _isAchieved)
            return;

        if (_isMultiAchievement)
        {
            Step();
        }
        else
        {
            _currentValue += value;

            while (_currentValue >= TargetValue && _currentStep < StepCount)
                Step();
        }

        StatusChenged?.Invoke();
    }

    private void Step()
    {
        _currentStep = !_isMultiAchievement ? _currentStep + 1 : 1;

        if (_currentStep == StepCount)
            _isAchieved = true;

        if (_currentStep <= StepCount)
        {
            _isRewardAvailavle = true;
            _countReward++;
        }
    }

    [System.Serializable]
    private class AchievementStep
    {
        public Sprite Icon;
        public int TargetValue;
        public int Reward;
    }
}