using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public int ScoreGoal;
    public int ScoreCurrent;
    public float TimeLimit;
    public float TimeCurrent;
    public Text Scoretext;
    public int timemin;
    // Use this for initialization
	void Start () {
        ScoreCurrent = 0;
        TimeCurrent = 0;
        timemin = 0;
	}
	
	// Update is called once per frame
	void Update () {

        TimeCurrent += Time.deltaTime;
        if (TimeCurrent >= 60)
        {
            timemin += 1;
            TimeCurrent -= 60;
        }

        if (timemin >= TimeLimit)
            GameOver();
        //Debug.Log("Score:   " + ScoreCurrent + "/" + ScoreGoal + "\n" + "Time:  " + TimeCurrent + "/" + TimeLimit);
        Scoretext.text = "Score: " + ScoreCurrent + " / " + ScoreGoal + " \n" + "Time: " + timemin + " min " + Mathf.Round(TimeCurrent) + " sec " + "/" + TimeLimit + " min ";
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
            Debug.Log("Game Lost!");
        } else
        {
            Debug.Log("You win!");
        }
    }
}
