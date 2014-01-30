using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public Transform tEnable;
    public Transform tDisable;
    public string sName = "BtnName";

    void Start () {
        SetSelected( false );
    }
    
    // Update is called once per frame
    public void SetSelected(bool _enable)
    {
        tEnable.renderer.enabled = !_enable;
        tDisable.renderer.enabled = _enable;
    }
}
