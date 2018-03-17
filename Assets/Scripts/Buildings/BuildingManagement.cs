using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManagement : MonoBehaviour {

    public GameObject[] buildings;
    private BuildingPlacement buildingPlacement;
    ResourceManager rm;

    // Use this for initialization
    void Start() {
        buildingPlacement = GetComponent<BuildingPlacement>();
        rm = FindObjectOfType<ResourceManager>();
        
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnGUI() {
        for (int i = 0; i < buildings.Length; i++) {
            if (GUI.Button(new Rect(20, 100 + i * 40, 100, 30), buildings[i].name)) {
                ObjectOnGrid ong = buildings[i].GetComponent<ObjectOnGrid>();

                if(rm.isEnough(ong.costWood, ong.costStone, 0))
                    buildingPlacement.SetItem(buildings[i]);
            }
        }
    }
}
