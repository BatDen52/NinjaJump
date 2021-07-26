using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEnter : Achievement
{
    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("LastEnter"))
            PlayerPrefs.SetString("LastEnter", DateTime.MinValue.Date.ToString());


        if (DateTime.Parse(PlayerPrefs.GetString("LastEnter")) < DateTime.Now.Date)
        {
            PlayerPrefs.SetString("LastEnter", DateTime.Now.Date.ToString());
            AddCurrentValue(1);
        }
    }
}
