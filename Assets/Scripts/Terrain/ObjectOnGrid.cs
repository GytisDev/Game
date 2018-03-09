using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnGrid : MonoBehaviour{

    public Grid grid;

    [HideInInspector]
    public int gridPosX, gridPosY;

    public int takesSpaceX, takesSpaceY;

    public bool placed = false;

    private void Update() {
        
    }

}
