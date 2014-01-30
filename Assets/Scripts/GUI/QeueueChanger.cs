using UnityEngine;
using System.Collections;

public class QeueueChanger : MonoBehaviour {
	
	public int _Qeueu = 1;
	// Use this for initialization
	void Start () {
		this.renderer.material.renderQueue = _Qeueu;
	}
}
