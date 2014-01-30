using UnityEngine;
using System.Collections;

public enum PowerUpType
{
    Minigun,Shield,MissileLauncher,Repair
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType PowerUpType;
    public Transform _Bounds;
    public float speed = 0.5f;

    /// <summary>
    /// materials in order of poweruptype
    /// </summary>
    public Material[] _Materials;

	// Use this for initialization
	void Start ()
	{
	    GameObject mainCamera = GameObject.Find("Main Camera");
	    _Bounds = mainCamera.transform.FindChild("Bounds");
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.transform.Translate(new Vector3(-speed*Time.deltaTime,0,0));

	    if (!_Bounds.collider.bounds.Contains(this.transform.position))
	    {
            Destroy(this.gameObject);
	    }
    }

    internal void PickRightMaterial()
    {
        if ((int)PowerUpType < _Materials.Length)
            this.renderer.material = _Materials[(int)PowerUpType];
    }
}
