using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressScript : MonoBehaviour {

    public Transform citizen;
    public int citizenCount;
    public Transform SpawnPosition;
    ObjectOnGrid oog;
    public bool spawnCitizens = true;

	// Use this for initialization
	void Start () {
        oog = GetComponent<ObjectOnGrid>();    
	}
	
	// Update is called once per frame
	void Update () {
        print(oog.placed);
		if(oog.placed && spawnCitizens) {
            SpawnCitizens(citizenCount);
            spawnCitizens = false;
        }
        
	}

    void SpawnCitizens(int count) {
        for (int i = 0; i < count; i++) {
            Instantiate(citizen, SpawnPosition.position, Quaternion.identity);
        }
    }
}
