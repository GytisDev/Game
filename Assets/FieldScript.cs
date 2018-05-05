using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour {

    public enum Stage { None, Empty, Planted, Growing, Grown }
    public GameObject[] models;

    public Stage stage;
    public int growTime;

    public float timer;
    public int foodYield;

    // Use this for initialization
    void Start() {
        stage = Stage.None;
        timer = 0;
        growTime = Random.Range(40, 46);
    }

    // Update is called once per frame
    void Update() {

        if (stage != Stage.Grown && stage >= Stage.Planted)
            timer += Time.deltaTime;

        if (timer >= growTime && stage < Stage.Grown && stage != Stage.None) {
            timer = 0;
            stage++;
            SwitchModel(stage);
        }
    }

    void SwitchModel(Stage stage) {
        switch (stage) {
            case Stage.None:
                
                break;
            case Stage.Empty:
                ChangeMesh(models[0]);
                transform.localScale = new Vector3(2f, 0.1f, 2f);
                break;
            case Stage.Planted:
                ChangeMesh(models[1]);
                transform.localScale = new Vector3(2f, 0.4f, 2f);
                break;
            case Stage.Growing:
                ChangeMesh(models[2]);
                transform.localScale = new Vector3(2f, 0.6f, 2f);
                break;
            case Stage.Grown:
                foodYield = Random.Range(5, 8);
                ChangeMesh(models[3]);
                transform.localScale = new Vector3(2f, 1f, 2f);
                break;
            default:
                
                break;
        }
    }

    public void SetCultivated() {
        stage = Stage.Empty;
        SwitchModel(stage);
        
    }

    public void SetPlanted() {
        stage = Stage.Planted;
        SwitchModel(stage);

    }

    public void SetCut() {
        stage = Stage.Empty;
        SwitchModel(stage);
    }

    private void ChangeMesh(GameObject pMesh) {
        if (pMesh == null) return;

        GetComponent<MeshFilter>().sharedMesh = pMesh.GetComponent<MeshFilter>().sharedMesh;
        GetComponent<MeshRenderer>().sharedMaterial = pMesh.GetComponent<MeshRenderer>().sharedMaterial;

    }

}
