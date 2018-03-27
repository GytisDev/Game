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
    private Vector3Int currentPos;

	public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

	public bool autoUpdate;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //generates island
    /*public void GenerateIsland()
    {
        currentPos.x = Mathf.RoundToInt(width / 2);
        currentPos.y = Mathf.RoundToInt(height / 2);

        int n = 0;
        int move = 0;

        while(n < size)
        {
            if (grid.IsWalkable(currentPos.x, currentPos.y))
            {
                grid.SetOccupied(currentPos.x, currentPos.y);
                n++;
            }
            move = Random.Range(0, 4);
            switch (move)
            {
                //////////////--up--///////////////////
                case 0:
                    if (currentPos.y + 1 <= height)
                        currentPos.y++;
                    else move++;
                    break;
                //////////////--left--/////////////////
                case 1:
                    if (currentPos.x - 1 >= 0)
                        currentPos.x--;
                    else move++;
                    break;
                //////////////--down--/////////////////
                case 2:
                    if (currentPos.y - 1 >= 0)
                        currentPos.y--;
                    else move++;
                    break;
                //////////////--right--////////////////
                case 3:
                    if (currentPos.x + 1 <= width)
                        currentPos.x++;
                    else move = 0;
                    break;
            }
        }
    }*/

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
        currentPos.x = 0;
        currentPos.y = 0;

        while (currentPos.y < height)
        {
            if (grid.IsWalkable(currentPos.x, currentPos.y))
            {
                Instantiate(LandBlocks, grid.GetNodePosition(currentPos.x, currentPos.y), Quaternion.identity, parent);
            }
            currentPos.x++;
            if(currentPos.x == width)
            {
                currentPos.y++;
                currentPos.x = 0;
            }
        }

        for (int x = -35; x < 35; x++) {
            for (int y = -35; y < 35; y++) {
                if(Random.Range(0, 10) > 8) {
                    Instantiate(tree, new Vector3(x, 0, y), Quaternion.Euler(new Vector3(-90, 0,0)));

                }
            }
        }
    }
}
