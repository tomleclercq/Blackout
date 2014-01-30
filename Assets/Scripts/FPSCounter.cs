using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour {
   
 static public float fFrameCount = 0;    
    private int iFramerate ;
    private float fInterval = 0.5f;
    private float fFpsAccumulated = 0;
    private float fTimeLeft = 0;
	
    void Start()
    {
        fTimeLeft = fInterval;
    }
    
	// Update is called once per frame
	void Update ()
    {
	    fTimeLeft -= Time.deltaTime;
        fFpsAccumulated += Time.timeScale / Time.deltaTime;
        ++ iFramerate;
         if( fTimeLeft <= 0.0f ){
            fFrameCount = fFpsAccumulated/iFramerate;
            fTimeLeft = fInterval;
            fFpsAccumulated = 0;
            iFramerate = 0;
        }
	}
}
