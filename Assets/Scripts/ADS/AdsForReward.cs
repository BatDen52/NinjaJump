using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(EndGame))]
public class AdsForReward : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private GameObject _buttonStartWatching;

    private EndGame _endGame;

    private string _gameID = "3894117";
    private string _myPlacementId = "rewardedVideo";
    private bool _testMode = false;

    private void OnEnable()
    {
        _endGame = GetComponent<EndGame>();
        _buttonStartWatching.SetActive(false);

        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameID, _testMode);

        _buttonStartWatching.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Advertisement.IsReady(_myPlacementId))
                Advertisement.Show(_myPlacementId);
            else
                _buttonStartWatching.SetActive(false);
        });
    }

    private void OnDisable()
    {
        _buttonStartWatching.GetComponent<Button>().onClick.RemoveAllListeners();
        Advertisement.RemoveListener(this);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
            _endGame.ReturnToGame(false);

        Time.timeScale = 1;
    }

    public void OnUnityAdsDidError(string message) { }

    public void OnUnityAdsDidStart(string placementId) { }

    public void OnUnityAdsReady(string placementId) { }
}
