using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {

    public Text AchievementsText;

    public Achievement[] Achievements = new Achievement[] {
        new Achievement(20, 0f, "Build a Gatherer's Hut", () => {
            return StatisticsManager.GatherersHutCount > 0;
        }),
        new Achievement(20, 0f, "Build a Wood Cutter's Hut", () => {
            return StatisticsManager.WoodCutterHutCount > 0;
        }),
        new Achievement(20, 0f, "Build a Farming Shack", () => {
            return StatisticsManager.FarmingShackCountr > 0;
        }),
        new Achievement(20, 0f, "Build a House", () => {
            return StatisticsManager.CivilianBuildingCount > 0;
        }),
        new Achievement(100, 0f, "Reach population: 9", () => {
            return StatisticsManager.CivilianCount >= 9;
        }),
        new Achievement(100, 30f, "Build all types of buildings", () => {
            return   StatisticsManager.CivilianBuildingCount > 0 &&
                    StatisticsManager.FarmingShackCountr > 0 &&
                    StatisticsManager.ForestersHutCount > 0 &&
                    StatisticsManager.GatherersHutCount > 0 &&
                    StatisticsManager.MasonsHutCount > 0 &&
                    StatisticsManager.WarehouseCount > 0 &&
                    StatisticsManager.WoodCutterHutCount > 0;
        }),
        new Achievement(200, 10f, "Reach population: 15", () => {
            return StatisticsManager.CivilianCount >= 15;
        })
    };

    void Update()
    {
        AchievementsText.text = "";
        int entrycount = 0;
        foreach (Achievement ach in Achievements)
        {
            if ((ach.Done == false) && (entrycount <= 9))
            {
                AchievementsText.text += "-" + ach.Description + "\n";
                if (ach.ScoreBonus != 0) AchievementsText.text += "Score Bonus: " + ach.ScoreBonus + "\n";
                if (ach.TimeBonus != 0) AchievementsText.text += "Time Bonus: " + ach.TimeBonus + "\n";
                entrycount++;
            }
        }


    }

    ScoreManager scoreManager;

    // Use this for initialization
    void Start () {
        scoreManager = FindObjectOfType<ScoreManager>();
        StartCoroutine(CheckAll());
    }

    public void AchievementComplete(Achievement achievement)
    {
        scoreManager.AddScore(achievement.ScoreBonus);
        scoreManager.AddTime(-achievement.TimeBonus);
        achievement.Done = true;
    }

    IEnumerator CheckAll()
    {
        while (true)
        {
            for (int i = 0; i < Achievements.Length; i++)
            {
                //Debug.Log("Veikia");
                if (!Achievements[i].Done)
                    if (Achievements[i].Check())
                        AchievementComplete(Achievements[i]);
                yield return null;
            }
        }
    }
}

public class Achievement
{
    public string Description;
    public int ScoreBonus;
    public float TimeBonus;
    public bool Done;
    Func<bool> _Check;

    public Achievement(int ScoreBonus, float TimeBonus, string Description, Func<bool> Check)
    {
        this.Description = Description;
        this.ScoreBonus = ScoreBonus;
        this.TimeBonus = TimeBonus;
        _Check = Check;
        this.Done = false;
    }

    public bool Check()
    {
        return _Check();
    }
}
