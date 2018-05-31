using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour
{

    public static void SaveWorld(Grid grid)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(Application.persistentDataPath + "/Save.sav", FileMode.Create);

        GameData data = new GameData();

        CollectData(ref data, grid);

        bf.Serialize(fs, data);
        fs.Close();
    }

    public static GameData LoadWorld(Grid grid)
    {
        GameData data = null;
        if (File.Exists(Application.persistentDataPath + "/Save.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + "/Save.sav", FileMode.Open);

            data = bf.Deserialize(fs) as GameData;

            fs.Close();
            return data;
        }
        return data;
    }

    static void CollectData(ref GameData data, Grid grid)
    {
        //Colecting Occupied grid
        data.width = grid.gridSizeX;
        data.height = grid.gridSizeY;

        data.OcGrid = new int[3, grid.gridSizeX * grid.gridSizeY];

        int lenght = 0;

        for (int x = 0; x < data.width; x++)
        {
            for (int y = 0; y < data.height; y++)
            {
                if(grid.GetNode(x, y).walkable == false)
                {
                    data.OcGrid[0, lenght] = x;
                    data.OcGrid[1, lenght++] = y;
                }
            }
        }

        data.OcGrid[2, lenght] = -1000;

        //Colecting resources
        ResourceManager resManager = FindObjectOfType<ResourceManager>();
        Daycount dayManager = FindObjectOfType<Daycount>();
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        data.Food = resManager.food;
        data.Wood = resManager.wood;
        data.Stone = resManager.stone;
        data.Score = scoreManager.ScoreCurrent;
        data.Time = scoreManager.TimeCurrent;
        data.Daycount = dayManager.day;

        //Colecting Grass object info
        GameObject[] WorldObjects = GameObject.FindGameObjectsWithTag("Grass");
        double[,] world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.grassArray = world;

        //Colecting Sand object info
        WorldObjects = GameObject.FindGameObjectsWithTag("Sand");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.sandArray = world;

        //Colecting Trees object info
        WorldObjects = GameObject.FindGameObjectsWithTag("Tree");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.treeArray = world;

        //Colecting BerryBushes object info
        WorldObjects = GameObject.FindGameObjectsWithTag("BerryBush");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.berryBushArray = world;

        //Colecting EmptyBerryBushes object info
        WorldObjects = GameObject.FindGameObjectsWithTag("EmptyBush");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.emptyBushArray = world;

        //Colecting Rock object info
        WorldObjects = GameObject.FindGameObjectsWithTag("Rock");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.rockArray = world;

        //Colecting Rock object info
        WorldObjects = GameObject.FindGameObjectsWithTag("CivilHouse");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.CivilHouseArray = world;

        //Colecting Rock object info
        WorldObjects = GameObject.FindGameObjectsWithTag("FarmShelter");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.FarmShelterArray = world;

        //Colecting Rock object info
        WorldObjects = GameObject.FindGameObjectsWithTag("MainBuilding");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.MainBuildingArray = world;

        //Colecting Rock object info
        WorldObjects = GameObject.FindGameObjectsWithTag("Gatherer");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.GathererArray = world;

        //Colecting Rock object info
        WorldObjects = GameObject.FindGameObjectsWithTag("Forester");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.ForesterArray = world;

        //Colecting Rock object info
        WorldObjects = GameObject.FindGameObjectsWithTag("Mason");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.MasonArray = world;

        //Colecting Rock object info
        WorldObjects = GameObject.FindGameObjectsWithTag("WareHouse");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.WareHouseArray = world;

        //Colecting Rock object info
        WorldObjects = GameObject.FindGameObjectsWithTag("WoodCutter");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.WoodCutterArray = world;

        //Colecting Rock object info
        WorldObjects = GameObject.FindGameObjectsWithTag("Citizen");
        world = new double[3, WorldObjects.Length];
        for (int i = 0; i < WorldObjects.Length; i++)
        {
            world[0, i] = WorldObjects[i].transform.position.x;
            world[1, i] = WorldObjects[i].transform.position.y;
            world[2, i] = WorldObjects[i].transform.position.z;
        }
        data.CitizensArray = world;
    }
}



[Serializable]
public class GameData
{
    public int[,] OcGrid;

    public double[,] grassArray;
    public double[,] sandArray;
    public double[,] treeArray;
    public double[,] berryBushArray;
    public double[,] emptyBushArray;
    public double[,] rockArray;

    public double[,] CivilHouseArray;
    public double[,] FarmShelterArray;
    public double[,] MainBuildingArray;
    public double[,] GathererArray;
    public double[,] ForesterArray;
    public double[,] MasonArray;
    public double[,] WareHouseArray;
    public double[,] WoodCutterArray;
    public double[,] CitizensArray;

    public int Food;
    public int Wood;
    public int Stone;   
    public int Daycount;
    public float Score;
    public float Time;

    public int width;
    public int height;

    public GameData() { }

    public GameData(double[,] GrassArray, double[,] SandArray, double[,] TreeArray, double[,] BerryBushArray, double[,] EmptyBushArray
                  , double[,] RockArray)
    {
        grassArray = GrassArray;
        sandArray = SandArray;
        treeArray = TreeArray;
        berryBushArray = BerryBushArray;
        emptyBushArray = EmptyBushArray;
        rockArray = RockArray;
    }
}
