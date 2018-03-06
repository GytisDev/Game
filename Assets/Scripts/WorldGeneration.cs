﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour {

    public Grid grid;
    public GameObject LandBlocks;

    private int width;
    private int height;
    private Vector3Int currentPos;
        
	// Use this for initialization
	void Start () {
        width = grid.Width;
        height = grid.Height;

        GenerateIsland();
        CreateLand();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PlaceIsland()
    {
        while(currentPos.y < height)
        {
            
        }
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
        int n;
        for (int i = 0; i < width; i++)
        {
            n = Random.Range(3, 6);
            for (int j = 0; j < n; j++)
            {
                grid.SetOccupied(i, j);
            }

            n = Random.Range(4, 7);
            for (int j = 1; j < n; j++)
            {
                grid.SetOccupied(i, height-j);
            }
        }

        for(int i = 0; i < height; i++)
        {
            n = Random.Range(3, 6);
            for (int j = 0; j < n; j++)
            {
                grid.SetOccupied(j, i);
            }

            n = Random.Range(4, 7);
            for (int j = 1; j < n; j++)
            {
                grid.SetOccupied(width - j, i);
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
                Instantiate(LandBlocks, grid.GetNodePosition(currentPos.x, currentPos.y), Quaternion.identity);
            }
            currentPos.x++;
            if(currentPos.x == width)
            {
                currentPos.y++;
                currentPos.x = 0;
            }
        }
    }
}
