using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressScript : MonoBehaviour
{

    public Transform citizen;
    public int citizenCount;
    public Transform SpawnPosition;
    public float[] Ranges = new float[] { 10f, 20f };
    public int Level = 1;
    private PopulationManager PopManager;
    private HappinessManager HappinessManager;

    private bool isplaced = false;
    private float spawnTime;
    public float baseSpawnTime = 5f;
    public float currentSpawnTime = 0f;
    ObjectOnGrid oog;
    public bool spawnCitizens = true;
    private GameObject button;
    bool statisticAdded = false;

    // Use this for initialization
    void Start()
    {
        oog = GetComponent<ObjectOnGrid>();
        PopManager = FindObjectOfType<PopulationManager>();
        HappinessManager = FindObjectOfType<HappinessManager>();
        spawnTime = baseSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        AdjustSpawnRate();
        if (!oog.placed)
            return;

        if (!statisticAdded) { StatisticsManager.FortressCount++; statisticAdded = true; }

        if (!isplaced)
        {
            button = GameObject.FindWithTag("FortressButton");
            button.SetActive(false);
            isplaced = true;
        }

        if (oog.placed && spawnCitizens)
        {
            StatisticsManager.ForestersHutCount++;
            SpawnCitizens(citizenCount);
            spawnCitizens = false;
        }

        if (currentSpawnTime >= spawnTime && !PopManager.isPopLimitReached())
        {
            SpawnCitizens(1);
            currentSpawnTime = 0f;
        }

        currentSpawnTime += Time.deltaTime;
    }

    public void AdjustSpawnRate() {
        int happiness = HappinessManager.happiness;

        if (happiness > 75) {
            spawnTime = baseSpawnTime;
        }
        else if (happiness < 25) {
            spawnTime = baseSpawnTime * 6;
        }
        else {
            spawnTime = baseSpawnTime * 2;
        }
    }

    void SpawnCitizens(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(citizen, SpawnPosition.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
    }
}
