using UnityEngine;
using System.Collections;

public class Visibility : MonoBehaviour {
	// Use this for initialization
    private bool bVisible = false;
    
    void Awake()
    {
        if( renderer == null )bVisible = T.GetHierarchyVisibility( transform );
        else bVisible = renderer.enabled;
    }
	
    public void Toggle()
	{		
        bVisible = !bVisible;
        if( renderer == null ) T.SetHierarchyVisibility( transform, bVisible );
        else renderer.enabled = bVisible;	
	}

    public void SetVisible()
    {       
        bVisible = true;
        if( renderer == null ) T.SetHierarchyVisibility( transform, bVisible );
        else renderer.enabled = bVisible;   
    }

    public void SetInvisible()
    {       
        bVisible = false;
        if( renderer == null ) T.SetHierarchyVisibility( transform, bVisible );
        else renderer.enabled = bVisible;   
    }
}