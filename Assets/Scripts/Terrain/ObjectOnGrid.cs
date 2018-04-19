using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnGrid : MonoBehaviour{

    [HideInInspector]
    public Grid grid;

    [HideInInspector]
    public int gridPosX, gridPosY;

    public string objectName;
    public int takesSpaceX, takesSpaceY;
    public int occupiesSpaceX, occupiesSpaceY;
    public int costWood, costStone;

    public bool placed = false;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Update() {
        
    }

}
