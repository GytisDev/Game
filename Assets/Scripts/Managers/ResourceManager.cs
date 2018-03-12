using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    private string resourcesFormat;

    public enum Resources {
        Wood,
        Food, 
        Stone
    }

    public int wood, food, stone;

	// Use this for initialization
	void Start () {
        UpdateFormat();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncreaseResources(Resources resource, int amount) {
        AlterResources(resource, amount);
    }

    public void DecreaseResources(Resources resource, int amount) {
        AlterResources(resource, -amount);
    }

    public bool isEnough(int wood, int stone, int food) {
        return this.wood >= wood && this.stone >= stone && this.food >= food;
    }

    private void AlterResources(Resources resource, int amount) {
        switch (resource) {
            case Resources.Wood:
                wood += amount;
                break;
            case Resources.Food:
                food += amount;
                break;
            case Resources.Stone:
                stone += amount;
                break;
            default:
                return;
        }

        UpdateFormat();
    }

    private void UpdateFormat() {
        resourcesFormat = "Wood: " + wood + " Stone: " + stone + " Food: " + food;
    }

    private void OnGUI() {
        GUI.skin.box.fontSize = 18;
        GUI.Box(new Rect(10, 10, 250, 25), resourcesFormat);
    }
}
