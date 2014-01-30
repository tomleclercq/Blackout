using UnityEngine;
using System.Collections;

public class CameraAnimation : MonoBehaviour {
    public float fSpeed = 1;
    public float fAmplitude = 2;

    private float fTimer;
    private Vector3 position_current, position_origin;


	// Use this for initialization
	void Start () {
        this.position_origin = this.transform.position;
        this.position_current = this.position_origin;
        fTimer = 0f;
    }

	
	// Update is called once per frame
	void Update () {
        this.fTimer += Time.deltaTime;
        this.position_current.x = this.position_origin.x + Mathf.Sin( this.fTimer * this.fSpeed ) * this.fAmplitude;
        this.transform.position = this.position_current;
	}
}
