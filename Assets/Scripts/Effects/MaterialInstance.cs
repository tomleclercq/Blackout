using UnityEngine;
using System.Collections;

public class MaterialInstance : MonoBehaviour
{
	// Use this for initialization
	void Awake () 
    {
        this.renderer.materials[0] = new Material(renderer.sharedMaterial);
	}
}
