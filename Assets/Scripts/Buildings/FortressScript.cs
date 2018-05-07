using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressScript : MonoBehaviour {

    public Transform citizen;
    public int citizenCount;
    public Transform SpawnPosition;
    public float[] Ranges = new float[] { 10f, 20f };
    public int Level = 1;
    private PopulationManager PopManager;

    public float spawnTime = 5f;
    public float currentSpawnTime = 0f;
    ObjectOnGrid oog;
    public bool spawnCitizens = true;

	// Use this for initialization
	void Start () {
        oog = GetComponent<ObjectOnGrid>();
        PopManager = FindObjectOfType<PopulationManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!oog.placed)
            return;

		if(oog.placed && spawnCitizens) {
            SpawnCitizens(citizenCount);
            spawnCitizens = false;
        }

        if(currentSpawnTime >= spawnTime && !PopManager.isPopLimitReached()) {
            SpawnCitizens(1);
            currentSpawnTime = 0f;
        }

        currentSpawnTime += Time.deltaTime;
	}

    void SpawnCitizens(int count) {
        for (int i = 0; i < count; i++) {
            Instantiate(citizen, SpawnPosition.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
    }
}
