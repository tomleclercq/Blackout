using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {

    // Use this for initialization
    void Awake ()
    {
        if (!Inputs.bInit ) Inputs.Init();
    }
}
