using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Daycount : MonoBehaviour {
    public GameObject Sun;
    public Text Count;
    private int day;
    private bool end;

	// Use this for initialization
	void Start () {
        day = 1;
        end = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Sun.transform.position.y < 0) end = true;
        if (end && Sun.transform.position.y > 0)
        {
            day++;
            end = false;
        }
        Count.text = day.ToString();
    }
}
