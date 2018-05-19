using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    public void Superfast()
    {
        Time.timeScale = 4f;
    }

    public void Fastforward()
    {
        Time.timeScale = 2f;
    }

    public void Normalspeed()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

}
