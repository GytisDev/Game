using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public bool displayGridGizmos;

    public GameObject[] Objects;
    private double[,,] Arrays;

    Node[,] grid;
    public Transform playerPosition;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask unwalkableMask;

    [HideInInspector]
    public float nodeDiameter;

    public int gridSizeX;
    public int gridSizeY;

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            LoadData();
            Baker surface = FindObjectOfType<Baker>();
            surface.needsRebaking = true;
            surface.timer = 0;
        }
    }

    //retuns number of cells in the grid
    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    //returns number of cells per row in the grid
    public int Width
    {
        get
        {
            return gridSizeX;
        }
    }

    //returns number of cells per column in the grid
    public int Height
    {
        get
        {
            return gridSizeY;
        }
    }

    //saves current world
    public void SaveData()
    {
        SaveLoad.SaveWorld(this);
    }

    //Load saved world
    public void LoadData()
    {
        DestroyEverything();

        GameData gd;
        gd = SaveLoad.LoadWorld(this);

        //Loading resources
        ResourceManager resManager = FindObjectOfType<ResourceManager>();
        Daycount dayManager = FindObjectOfType<Daycount>();
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        resManager.food = gd.Food;
        resManager.wood = gd.Wood;
        resManager.stone = gd.Stone;
        scoreManager.ScoreCurrent = gd.Score;
        scoreManager.TimeCurrent = gd.Time;
        dayManager.day = gd.Daycount;

        //update grid
        CreateGrid(gd.width, gd.height);

        for (int i = 0; i < gd.OcGrid.Length; i++)
        {
            if(gd.OcGrid[2, i] == -1000)
            {
                break;
            }
            SetOccupied(gd.OcGrid[0, i], gd.OcGrid[1, i]);
        }

        BuildEverything(gd);
    }

    public void DestroyEverything()
    {
        GameObject[] Grass, Sand, BerryBush, EmptyBush, Rock, Trees, CivilHouse, FarmShelter,
                     MainBuilding, Gatherer, Forester, Mason, WareHouse, WoodCutter, Citizens;

        Grass = GameObject.FindGameObjectsWithTag("Grass");
        Sand = GameObject.FindGameObjectsWithTag("Sand");
        BerryBush = GameObject.FindGameObjectsWithTag("BerryBush");
        EmptyBush = GameObject.FindGameObjectsWithTag("EmptyBush");
        Rock = GameObject.FindGameObjectsWithTag("Rock");
        Trees = GameObject.FindGameObjectsWithTag("Tree");
        CivilHouse = GameObject.FindGameObjectsWithTag("CivilHouse");
        FarmShelter = GameObject.FindGameObjectsWithTag("FarmShelter");
        MainBuilding = GameObject.FindGameObjectsWithTag("MainBuilding");
        Gatherer = GameObject.FindGameObjectsWithTag("Gatherer");
        Forester = GameObject.FindGameObjectsWithTag("Forester");
        Mason = GameObject.FindGameObjectsWithTag("Mason");
        WareHouse = GameObject.FindGameObjectsWithTag("WareHouse");
        WoodCutter = GameObject.FindGameObjectsWithTag("WoodCutter");
        Citizens = GameObject.FindGameObjectsWithTag("Citizen");

        for (var i = 0; i < Grass.Length; i++)
            Destroy(Grass[i]);
        for (var i = 0; i < Sand.Length; i++)
            Destroy(Sand[i]);
        for (var i = 0; i < BerryBush.Length; i++)
            Destroy(BerryBush[i]);
        for (var i = 0; i < EmptyBush.Length; i++)
            Destroy(EmptyBush[i]);
        for (var i = 0; i < Rock.Length; i++)
            Destroy(Rock[i]);
        for (var i = 0; i < Trees.Length; i++)
            Destroy(Trees[i]);
        for (var i = 0; i < CivilHouse.Length; i++)
            Destroy(CivilHouse[i]);
        for (var i = 0; i < FarmShelter.Length; i++)
            Destroy(FarmShelter[i]);
        for (var i = 0; i < MainBuilding.Length; i++)
            Destroy(MainBuilding[i]);
        for (var i = 0; i < Gatherer.Length; i++)
            Destroy(Gatherer[i]);
        for (var i = 0; i < Forester.Length; i++)
            Destroy(Forester[i]);
        for (var i = 0; i < Mason.Length; i++)
            Destroy(Mason[i]);
        for (var i = 0; i < WareHouse.Length; i++)
            Destroy(WareHouse[i]);
        for (var i = 0; i < WoodCutter.Length; i++)
            Destroy(WoodCutter[i]);
        for (var i = 0; i < Citizens.Length; i++)
            Destroy(Citizens[i]);
    }

    public void BuildEverything(GameData gd)
    {
        //CreateGrid(gd.width, gd.height);
        //grass
        for (int i = 0; i < gd.grassArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.grassArray[0, i], (float)gd.grassArray[1, i], (float)gd.grassArray[2, i]);
            Instantiate(Objects[0], pos, Quaternion.identity);
        }
        //Sand
        for (int i = 0; i < gd.sandArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.sandArray[0, i], (float)gd.sandArray[1, i], (float)gd.sandArray[2, i]);
            Instantiate(Objects[1], pos, Quaternion.identity);
        }
        //Tree
        for (int i = 0; i < gd.treeArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.treeArray[0, i], (float)gd.treeArray[1, i], (float)gd.treeArray[2, i]);
            Instantiate(Objects[2], pos, Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
        //Berry
        for (int i = 0; i < gd.berryBushArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.berryBushArray[0, i], (float)gd.berryBushArray[1, i], (float)gd.berryBushArray[2, i]);
            Instantiate(Objects[3], pos, Quaternion.identity);
        }
        //emptyBerry
        for (int i = 0; i < gd.emptyBushArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.emptyBushArray[0, i], (float)gd.emptyBushArray[1, i], (float)gd.emptyBushArray[2, i]);
            Instantiate(Objects[4], pos, Quaternion.identity);
        }

        //Rock
        for (int i = 0; i < gd.rockArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.rockArray[0, i], (float)gd.rockArray[1, i], (float)gd.rockArray[2, i]);
            Instantiate(Objects[5], pos, Quaternion.identity);
        }
        //Civilian House
        for (int i = 0; i < gd.CivilHouseArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.CivilHouseArray[0, i], (float)gd.CivilHouseArray[1, i], (float)gd.CivilHouseArray[2, i]);
            GameObject inst = Instantiate(Objects[6], pos, Quaternion.Euler(new Vector3(-90, 0, 0)));
            inst.GetComponent<ObjectOnGrid>().placed = true;
            inst.GetComponent<NavMeshObstacle>().enabled = true;
        }

        //Farm Shelter
        for (int i = 0; i < gd.FarmShelterArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.FarmShelterArray[0, i], (float)gd.FarmShelterArray[1, i], (float)gd.FarmShelterArray[2, i]);
            GameObject inst = Instantiate(Objects[7], pos, Quaternion.Euler(new Vector3(-90, 0, 0)));
            inst.GetComponent<ObjectOnGrid>().placed = true;
            inst.GetComponent<NavMeshObstacle>().enabled = true;
        }
        //Main Building
        for (int i = 0; i < gd.MainBuildingArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.MainBuildingArray[0, i], (float)gd.MainBuildingArray[1, i], (float)gd.MainBuildingArray[2, i]);
            GameObject inst = Instantiate(Objects[8], pos, Quaternion.Euler(new Vector3(-90, 0, 0)));
            inst.GetComponent<ObjectOnGrid>().placed = true;
            inst.GetComponent<NavMeshObstacle>().enabled = true;
        }
        //Gatherer
        for (int i = 0; i < gd.GathererArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.GathererArray[0, i], (float)gd.GathererArray[1, i], (float)gd.GathererArray[2, i]);
            GameObject inst = Instantiate(Objects[9], pos, Quaternion.Euler(new Vector3(-90, 0, 0)));
            inst.GetComponent<ObjectOnGrid>().placed = true;
            inst.GetComponent<NavMeshObstacle>().enabled = true;
        }
        //Forester
        for (int i = 0; i < gd.ForesterArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.ForesterArray[0, i], (float)gd.ForesterArray[1, i], (float)gd.ForesterArray[2, i]);
            GameObject inst = Instantiate(Objects[10], pos, Quaternion.Euler(new Vector3(-90, 0, 0)));
            inst.GetComponent<ObjectOnGrid>().placed = true;
            inst.GetComponent<NavMeshObstacle>().enabled = true;
        }
        //Mason
        for (int i = 0; i < gd.MasonArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.MasonArray[0, i], (float)gd.MasonArray[1, i], (float)gd.MasonArray[2, i]);
            GameObject inst = Instantiate(Objects[11], pos, Quaternion.Euler(new Vector3(-90, 0, 0)));
            inst.GetComponent<ObjectOnGrid>().placed = true;
            inst.GetComponent<NavMeshObstacle>().enabled = true;
        }
        //WareHouse
        for (int i = 0; i < gd.WareHouseArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.WareHouseArray[0, i], (float)gd.WareHouseArray[1, i], (float)gd.WareHouseArray[2, i]);
            GameObject inst = Instantiate(Objects[12], pos, Quaternion.Euler(new Vector3(-90, 0, 0)));
            inst.GetComponent<ObjectOnGrid>().placed = true;
            inst.GetComponent<NavMeshObstacle>().enabled = true;
        }
        //WoodCutter
        for (int i = 0; i < gd.WoodCutterArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.WoodCutterArray[0, i], (float)gd.WoodCutterArray[1, i], (float)gd.WoodCutterArray[2, i]);
            GameObject inst = Instantiate(Objects[13], pos, Quaternion.Euler(new Vector3(-90, 0, 0)));
            inst.GetComponent<ObjectOnGrid>().placed = true;
            inst.GetComponent<NavMeshObstacle>().enabled = true;
        }
        //Citizens
        for (int i = 0; i < gd.CitizensArray.GetLength(1); i++)
        {
            Vector3 pos = new Vector3((float)gd.CitizensArray[0, i], (float)gd.CitizensArray[1, i], (float)gd.CitizensArray[2, i]);
            Instantiate(Objects[14], pos, Quaternion.identity);
        }
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = (Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }
    void CreateGrid(int width, int height)
    {
        gridSizeX = width;
        gridSizeY = height;
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public bool UpdateGrid(int gridX, int gridY, int takesX, int takesY)
    {

        int subX = takesX / 2 - (takesX + 1) % 2;
        int subY = takesY / 2 - (takesY + 1) % 2;

        if (gridX - subX + takesX >= gridSizeX || gridX - subX < 0 || gridY - subY + takesY >= gridSizeY || gridY - subY < 0)
            return false;

        for (int x = gridX - subX; x < gridX - subX + takesX; x++)
        {
            for (int y = gridY - subY; y < gridY - subY + takesY; y++)
            {
                if (!grid[x, y].walkable)
                {
                    return false;
                }
            }
        }
        //Pažymim unwalkable
        for (int x = gridX - subX; x < gridX - subX + takesX; x++)
        {
            for (int y = gridY - subY; y < gridY - subY + takesY; y++)
            {
                grid[x, y].walkable = false;
            }
        }

        return true;
    }

    public bool CheckDistance(int gridX, int gridY, int takesX, int takesY, Vector3 FortressPos, float maxDistance)
    {

        int subX = takesX / 2 - (takesX + 1) % 2;
        int subY = takesY / 2 - (takesY + 1) % 2;

        if (gridX - subX + takesX >= gridSizeX || gridX - subX < 0 || gridY - subY + takesY >= gridSizeY || gridY - subY < 0)
            return false;

        for (int x = gridX - subX; x < gridX - subX + takesX; x++)
        {
            for (int y = gridY - subY; y < gridY - subY + takesY; y++)
            {
                if (Vector3.Distance(FortressPos, GetNode(x, y).worldPosition) > maxDistance)
                    return false;
            }
        }
        return true;
    }

    public Node[,] GetNodes(int gridX, int gridY, int takesX, int takesY)
    {

        Node[,] nodes = new Node[takesX, takesY];
        int subX = takesX / 2 - (takesX + 1) % 2;
        int subY = takesY / 2 - (takesY + 1) % 2;

        int i = 0;
        int j = 0;
        for (int x = gridX - subX; x < gridX - subX + takesX; x++)
        {
            j = 0;
            for (int y = gridY - subY; y < gridY - subY + takesY; y++)
            {
                nodes[i, j] = grid[x, y];
                j++;
            }
            i++;
        }

        return nodes;
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = ((worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float percentY = ((worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        if (worldPosition.x < 0)
            x--;
        if (worldPosition.z < 0)
            y--;

        return grid[x, y];
    }

    public Node GetNode(int x, int y)
    {
        return grid[x, y];
    }

    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null && displayGridGizmos)
        {
            foreach (Node node in grid)
            {

                Gizmos.color = (node.walkable) ? Color.white : Color.red;

                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }

        }
    }

    //returns node position in the world
    public Vector3 GetNodePosition(int x, int y)
    {
        return grid[x, y].worldPosition;
    }

    //sets cell as occupied
    public void SetOccupied(int x, int y)
    {
        grid[x, y].walkable = false;
    }

    //sets cell as walkable
    public void SetWalkable(int x, int y)
    {
        grid[x, y].walkable = true;
    }

    //returns is cell walkable or not
    public bool IsWalkable(int x, int y)
    {
        return grid[x, y].walkable;
    }

}
