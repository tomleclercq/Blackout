using UnityEngine;
using System.Collections;

public class ShieldTest : MonoBehaviour
{
    private float _Slider;
    private Material _ShieldMat;

    private bool _CanPlay;
    private bool _ShieldUp;

	// Use this for initialization
	void Start ()
	{
	    _ShieldMat = this.renderer.material;
	}
	
	// Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!_ShieldUp)
                _ShieldUp = true;
            else
                _ShieldUp = false;
        }
        if (_ShieldUp)
        {
            _Slider += Time.deltaTime;
            if (_Slider > 1)
                _Slider = 1;
        }
        else
        {
            _Slider -= Time.deltaTime;
            if (_Slider < 0)
                _Slider = 0;
        }
        _ShieldMat.SetFloat("_DesolveSlider", _Slider);
    }
}
