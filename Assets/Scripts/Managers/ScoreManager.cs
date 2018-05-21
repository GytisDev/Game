using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public int ScoreGoal;
    public float ScoreCurrent;
    public float TimeLimit;
    public float TimeCurrent;
    public Slider scoreslider;
    public Slider timeslider;

    public GameObject VictoryScreen;
    public GameObject DefeatScreen;

    // Use this for initialization
    void Start()
    {
        ScoreCurrent = 0;
        TimeCurrent = 0;
        //timemin = 0;
    }

    // Update is called once per frame
    void Update()
    {

        TimeCurrent += Time.deltaTime;

        if ((TimeCurrent >= TimeLimit) || (ScoreCurrent >= ScoreGoal))
            GameOver();
        scoreslider.value = ScoreCurrent / ScoreGoal;
        timeslider.value = 1 - (TimeCurrent) / (TimeLimit);
    }

    public void AddScore(int points)
    {
        ScoreCurrent += points;
        ScoreCurrent = Mathf.Clamp(ScoreCurrent, 0, ScoreGoal);
    }

    public void AddTime(float time)
    {
        TimeCurrent += time;
        TimeCurrent = Mathf.Clamp(TimeCurrent, 0, TimeLimit);
    }

    public void GameOver()
    {
        if (ScoreCurrent < ScoreGoal)
        {
            Time.timeScale = 0f;
            DefeatScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 0f;
            VictoryScreen.SetActive(true);
        }
    }
}
