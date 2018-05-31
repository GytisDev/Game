using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour {

    public Grid grid;
    public GameObject LandBlocks;
    public GameObject tree;
    public GameObject rock;
    public GameObject bush;
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

	public Vector2 ForestsHeights;
	public Vector2Int TreesPerForest;

	public TerrainType[] regions;
	public GameObject[,] Objects;
	public GameObject[,] Trees;

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
		seed = Random.Range(100, 999);

        width = grid.Width;
        height = grid.Height;

        GenerateIsland();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

    //generates island
    void GenerateIsland()
    {
		float[,] WorldNoiseMap = Noise.GenerateNoiseMap (width, height, seed, noiseScale, octaves, persistance, lacunarity, offset);
		float[,] ForestsNoiseMap = Noise.GenerateNoiseMap (width, height, seed+1, noiseScale, octaves, persistance, lacunarity, offset);

        Random rnd = new Random();

		Objects = new GameObject [width, height];
		Trees = new GameObject[width, height];
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {

				WorldNoiseMap[x, y] -= fallOff[x, y];
				float worldCurrentHeight = WorldNoiseMap [x, y];
				float forestCurrentHeight = ForestsNoiseMap [x, y];

				if (forestCurrentHeight >= ForestsHeights.x && forestCurrentHeight <= ForestsHeights.y) {
					if(Random.Range(1, 10) > 2)
						Trees [x, y] = tree;
				}

				for (int i = 0; i < regions.Length; i++) {
					if (worldCurrentHeight >= regions [i].height.x && worldCurrentHeight <= regions [i].height.y) {
						Objects [x, y] = regions [i].gObject;
						break;
					}
				}
			}
		}

		//Generator
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				//Creates land blocks
				if (Objects [x, y] != null) {
					Instantiate (Objects[x, y], grid.GetNodePosition (x, y), Quaternion.identity, parent);
					grid.SetWalkable(x, y);
				}

				//Creates trees
				if (Trees [x, y] != null) {
					if (grid.IsWalkable (x, y)) 
					{
						Instantiate(Trees[x, y], grid.GetNodePosition (x, y) + new Vector3(0, 0.5f, 0), Quaternion.Euler(new Vector3(-90, 0,0)), parent).GetComponent<ObjectOnGrid>().SetNodes(new Node[,] { { grid.GetNode(x,y)} });
						grid.SetOccupied (x, y);
					}
				}

                //Creates rocks
                if(Random.Range(0, 1000) > 990) {
                    if((x - 1 > 0 && x + 1 < width) && (y - 1 > 0 && y + 1 < height)) {
                        for (int i = -1; i <= 1; i++) {
                            for (int j = -1; j <= 1; j++) {
                                if(grid.IsWalkable(x + i, y + j) && Random.Range(0, 100) > 50) {
                                    Instantiate(rock, grid.GetNodePosition(x + i, y + j) + new Vector3(0, 0.5f, 0), Quaternion.Euler(new Vector3(0, Random.Range(0f, 180f), 0)), parent).GetComponent<ObjectOnGrid>().SetNodes(new Node[,] { { grid.GetNode(x + i, y + j) } });
                                    grid.SetOccupied(x + i, y + j);
                                }
                            }
                        }
                    }
                }

                //Creates bushes
                if (Random.Range(0, 1000) > 990) {
                    if ((x - 1 > 0 && x + 1 < width) && (y - 1 > 0 && y + 1 < height)) {
                        for (int i = -1; i <= 1; i++) {
                            for (int j = -1; j <= 1; j++) {
                                if (grid.IsWalkable(x + i, y + j) && Random.Range(0, 100) > 50) {
                                    Instantiate(bush, grid.GetNodePosition(x + i, y + j) + new Vector3(0, 0.5f, 0), Quaternion.Euler(new Vector3(0, Random.Range(0f, 180f), 0)), parent);
                                    grid.SetOccupied(x + i, y + j);
                                }
                            }
                        }
                    }
                }
            }
		}

    }

    //Create land
    void CreateLand()
    {

    }
}
