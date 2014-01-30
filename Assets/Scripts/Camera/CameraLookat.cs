using UnityEngine;
using System.Collections;

public class CameraLookat : MonoBehaviour {

    public Transform target;
    
    // Rotate the camera every frame so it keeps looking at the target 
    void Update()
    {
        this.transform.LookAt(target);
    }
}
