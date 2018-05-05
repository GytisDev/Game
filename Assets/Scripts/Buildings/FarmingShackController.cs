using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingShackController : MonoBehaviour {

    public GameObject InitialPosition;      // The position where citizen should initially come at work
    public GameObject EmptyField;
    public int Radius = 1;
    public List<FieldScript> Fields;
    private bool citizenAsigned = false;
    private bool landOccupied = false;
    GameObject[] AsignedCitizens = new GameObject[1];
    Grid grid;
    ObjectOnGrid oog;

    private void Start() {
        grid = FindObjectOfType<Grid>();
        Fields = new List<FieldScript>();
        oog = gameObject.GetComponent<ObjectOnGrid>();
    }

    // Update is called once per frame
    void Update() {
        if (!oog.placed) return;

        if (!landOccupied && oog.placed) {
            landOccupied = true;

            foreach (GameObject gameObject in FindPossibleFields()) {
                Fields.Add(gameObject.GetComponent<FieldScript>());
            }
        }

        if (!citizenAsigned) {
            AsignCitizens();
        }
    }

    public void AsignCitizens() {
        foreach (GameObject citizen in GameObject.FindGameObjectsWithTag("Citizen")) {
            if (!citizenAsigned) {
                CitizenScript citizenScript = citizen.GetComponent<CitizenScript>();
                if (citizenScript.available) {
                    citizenAsigned = true;

                    citizenScript.available = false;

                    // Add ForesterScript componenet to asigned citizen
                    citizen.AddComponent<FarmerScript>();
                    FarmerScript fs = citizen.GetComponent<FarmerScript>();
                    fs.fsc = this;
                    fs.ChangeState_GoingToWork();

                    // Citizen goes to his workplace
                    Unit unit = citizen.GetComponent<Unit>();
                    unit.MoveTo(InitialPosition.transform.position);
                }
            }
        }
    }

    List<GameObject> FindPossibleFields() {
        List<GameObject> list = new List<GameObject>();

        Node HutNode = grid.NodeFromWorldPoint(transform.position);
        Node currentTargetNode;

        for (int i = -Radius; i <= Radius; i++) {
            for (int j = -Radius; j <= Radius; j++) {
                int x = HutNode.gridX + i;
                int y = HutNode.gridY + j;
                if (HutNode.worldPosition.x < 0)
                    x++;
                if (HutNode.worldPosition.z < 0)
                    y++;
                currentTargetNode = grid.GetNode(x, y);
                if (currentTargetNode.walkable) {
                    list.Add(Instantiate(EmptyField, currentTargetNode.worldPosition += new Vector3(0, 0.5f, 0), Quaternion.identity));
                    currentTargetNode.walkable = false;
                    
                }    
            }
        }

        return list;
    }
}
