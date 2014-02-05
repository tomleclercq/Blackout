using UnityEngine;
using System.Collections;

public enum ButtonTypes
{
    Play = 0,
    Instructions,
    Credits,
    Back
};

public class Button : MonoBehaviour {

    public ButtonTypes Type;
    public bool bDisplayed = true;
    public bool bSelected = false;
    public Transform tSelected;
    public Transform tUnSelected;

    void Update()
    {
        if( !this.bDisplayed )
            this.Hide();
        else
            this.DisplaySelected();
    }

    public void SetSelected(bool _selected)
    {
        this.bSelected = _selected;
        this.DisplaySelected();
       //if( _selected )T.Log("btn " + this.Type.ToString() + " selected");
    }

    public void SetVisible(bool _visible)
    {
        this.bDisplayed = _visible;
    }

    private void DisplaySelected()
    {
        this.tSelected.renderer.enabled = !this.bSelected;
        this.tUnSelected.renderer.enabled = this.bSelected;
    }

    private void Hide()
    {
        this.tSelected.renderer.enabled = false;
        this.tUnSelected.renderer.enabled = false;
    }
}
