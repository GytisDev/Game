using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Achievements:
//  0 - Score bonus:  100 | Time bonus:      0.00 | Reach population: 7
//  1 - Score bonus:  100 | Time bonus:     30.00 | Build all types of buildings
//  2 - Score bonus:  200 | Time bonus:     10.00 | Reach population: 9

public class AchievementManager : MonoBehaviour {

    public Achievement[] Achievements = new Achievement[] {
        /* 0*/ new Achievement(100, 0f),
        /* 1*/ new Achievement(100, 30f),
        /* 2*/ new Achievement(200, 10f)
    };

    ScoreManager scoreManager;
    PopulationManager populationManager;

    // Use this for initialization
    void Start () {
        scoreManager = FindObjectOfType<ScoreManager>();
        populationManager = FindObjectOfType<PopulationManager>();
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
            if (!Achievements[0].Done) Check0(); yield return null;
            if (!Achievements[1].Done) Check1(); yield return null;
            if (!Achievements[2].Done) Check2(); yield return null;
        }
    }

    public void Check0()
    {
        if (populationManager.population >= 7)
            AchievementComplete(Achievements[0]);
    }
    public void Check1()
    {
        if (StatisticsManager.CivilianBuildingCount > 0 &&
            StatisticsManager.FarmingShackCountr > 0 &&
            StatisticsManager.ForestersHutCount > 0 &&
            StatisticsManager.GatherersHutCount > 0 &&
            StatisticsManager.MasonsHutCount > 0 &&
            StatisticsManager.WarehouseCount > 0 &&
            StatisticsManager.WoodCutterHutCount > 0)
            AchievementComplete(Achievements[1]);
    }
    public void Check2()
    {
        if (populationManager.population >= 9)
            AchievementComplete(Achievements[2]);
    }
}

public class Achievement
{
    public int ScoreBonus;
    public float TimeBonus;
    public bool Done;
    public Achievement(int ScoreBonus, float TimeBonus)
    {
        this.ScoreBonus = ScoreBonus;
        this.TimeBonus = TimeBonus;
        this.Done = false;
    }
}
