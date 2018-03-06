using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour {

    public Material[] Material;
    Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = Material[Random.Range(0, Material.Length)];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
