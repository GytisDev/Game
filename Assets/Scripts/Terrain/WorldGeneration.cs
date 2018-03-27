using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour {

    public Grid grid;
    public GameObject LandBlocks;
    public GameObject tree;
    public Transform parent;

    private int width;
    private int height;

	public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

	public bool autoUpdate;

	public Vector2Int ForestNumber;
	public Vector2Int TreesPerForest;

	public TerrainType[] regions;
	public GameObject[,] Objects;

    float[,] fallOff;

	[System.Serializable]
	public struct TerrainType {
		public string name;
		public Vector2 height;
		public GameObject gObject;
	}
        
	// Use this for initialization
	void Awake () {
        fallOff = FallOffGenerator.Generate(grid.gridSizeX);

        width = grid.Width;
        height = grid.Height;

        GenerateIsland();
		GenerateForests (Random.Range(ForestNumber.x, ForestNumber.y), TreesPerForest.x, TreesPerForest.y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Gnerates Multiple forests
	public void GenerateForests(int n, int minTrees, int MaxTrees)
	{
		Vector2Int CurrentPos = new Vector2Int();
		CurrentPos.x = Random.Range (0, width);
		CurrentPos.y = Random.Range (0, height);

		while(grid.IsWalkable(CurrentPos.x, CurrentPos.y)){
			CurrentPos.x = Random.Range (0, width);
			CurrentPos.y = Random.Range (0, height);
		}

		for (int i = 0; i < n; i++) {
			GenerateTrees (Random.Range(minTrees, MaxTrees), CurrentPos);
		}
	}

    //Generates One forest
	public void GenerateTrees(int size, Vector2Int currentPos)
    {
        int n = 0;
        int move = 0;

        while(n < size)
        {
            if (grid.IsWalkable(currentPos.x, currentPos.y))
            {
                grid.SetOccupied(currentPos.x, currentPos.y);
				Instantiate(tree, grid.GetNodePosition (currentPos.x, currentPos.y), Quaternion.Euler(new Vector3(-90, 0,0)));
                n++;
            }
            move = Random.Range(0, 4);
            switch (move)
            {
                //////////////--up--///////////////////
                case 0:
                    if (currentPos.y + 1 < height)
                        currentPos.y++;
                    else move++;
                    break;
                //////////////--left--/////////////////
                case 1:
                    if (currentPos.x - 1 > 0)
                        currentPos.x--;
                    else move++;
                    break;
                //////////////--down--/////////////////
                case 2:
                    if (currentPos.y - 1 > 0)
                        currentPos.y--;
                    else move++;
                    break;
                //////////////--right--////////////////
                case 3:
                    if (currentPos.x + 1 < width)
                        currentPos.x++;
                    else move = 0;
                    break;
            }
        }
    }

    //generates island
    void GenerateIsland()
    {
		float[,] noiseMap = Noise.GenerateNoiseMap (width, height, seed, noiseScale, octaves, persistance, lacunarity, offset);

		Objects = new GameObject [width, height];
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {

                noiseMap[x, y] -= fallOff[x, y];
				float currentHeight = noiseMap [x, y];

				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight >= regions [i].height.x && currentHeight <= regions [i].height.y) {
						Objects [x, y] = regions [i].gObject;
						break;
					}
				}
			}
		}

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (Objects [x, y] != null) {
					Instantiate (LandBlocks, grid.GetNodePosition (x, y), Quaternion.identity, parent);
					grid.SetWalkable(x, y);
				}
			}
		}
    }

    //Create land
    void CreateLand()
    {

    }
}
