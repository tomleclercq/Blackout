using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public Transform tSelected;
    public Transform tUnSelected;
    public bool bDisplayed = true ;
    public string sName = "BtnName";

    void Start () {
        this.SetSelected(false);
        this.SetVisible( bDisplayed );
    }
    
    // Update is called once per frame
    public void SetSelected(bool _selected)
    {
        this.tSelected.renderer.enabled = !_selected;
        this.tUnSelected.renderer.enabled = _selected;
    }

    // Update is called once per frame
    public void SetVisible( bool _visible )
    {
        bDisplayed = _visible;
        this.tSelected.renderer.enabled = this.bDisplayed;
        this.tUnSelected.renderer.enabled = this.bDisplayed;
    }
}
