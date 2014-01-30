using System.ComponentModel.Design;
using UnityEngine;
using System.Collections;

public class ViewChanger : MonoBehaviour
{
    public enum Rotatoion
    {
        Up,
        Down
    }

    private Camera _Camera;

    public Player[] _Players =new Player[0];
    
	// Use this for initialization
	void Start ()
    {
        _Camera = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Switch();
        }
	}


    public void Switch()
    {
        if (!Orientation.Instance.InTransition)
            ChengeVieuw((Orientation.Instance.OrientationAxis == Axis.Top) ? Rotatoion.Down : Rotatoion.Up);
    }


    public void ChengeVieuw(Rotatoion pRotationDir)
    {
        Vector3 rotationDir;

        switch (pRotationDir)
        {
            case Rotatoion.Up:
                if (!Orientation.Instance.Up())
                    return;
                rotationDir = Vector3.right;
                break;
            case Rotatoion.Down:
                if (!Orientation.Instance.Down())
                    return;
                rotationDir = Vector3.left;
                break;
            default:
                 rotationDir = Vector3.right;
                break;

        }

        StartCoroutine(RotateArroundRoutine(rotationDir, 0.1f, 10f));
        
    }

    private IEnumerator RotateArroundRoutine(Vector3 pDirection, float pSpeed, float pAngle)
    {
        Orientation.Instance.Lock();

        float Angle = 0;
        while (Angle < 90)
        {
            camera.transform.RotateAround(_Players[0].transform.position, pDirection, pAngle * pSpeed);
            Angle += pAngle*pSpeed;
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("end");
        Orientation.Instance.Free();
    }
}
