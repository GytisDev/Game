using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortressScript : MonoBehaviour
{

    public GameObject info;
    public Text infobar;
    string message;
    public int upcostwood;
    public int upcoststone;

    public Transform citizen;
    public int citizenCount;
    public Transform SpawnPosition;
    public float[] Ranges = new float[] { 10f, 20f };
    public int Level = 1;
    private PopulationManager PopManager;
    public GameObject ActionsMenu;
    private bool aac = false;

    private bool isplaced = false;
    public float spawnTime = 5f;
    public float currentSpawnTime = 0f;
    ObjectOnGrid oog;
    public bool spawnCitizens = true;
    private GameObject button;
    ResourceManager rm;

    // Use this for initialization
    void Start()
    {
        oog = GetComponent<ObjectOnGrid>();
        rm = FindObjectOfType<ResourceManager>();
        PopManager = FindObjectOfType<PopulationManager>();
        message = "Fortress Upgrade\n" + upcostwood + " Wood " + upcoststone + " Stone";
    }

    // Update is called once per frame
    void Update()
    {

        if (!oog.placed)
            return;

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

    public void OnMouseDown()
    {
        if (!oog.placed)
            return;
        if ((!aac) && (Level < 3))
        {
            ActionsMenu.SetActive(true);
            aac = true;
        }
        else
        {
            ActionsMenu.SetActive(false);
            aac = false;
        }
    }

    void SpawnCitizens(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(citizen, SpawnPosition.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
    }

    public void Upgrade()
    {
        if ((Level < 3))
        {
            if (rm.isEnough(upcostwood, upcoststone, 0))
            {
                rm.DecreaseResources(ResourceManager.Resources.Wood, upcostwood);
                rm.DecreaseResources(ResourceManager.Resources.Stone, upcoststone);
                Level++;
            }

        }
    }

    public void HoverEnter()
    {
        info.transform.localScale = new Vector3(3, 1, 1);
        infobar.text = message;
    }

    public void HoverExit()
    {
        info.transform.localScale = new Vector3(0, 0, 0);
        infobar.text = "";
    }
}
