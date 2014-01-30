using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public float BlinkSpeed = 0.2f;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.anyKeyDown)
	    {
            Application.LoadLevel(1);
	    }
    }
}
