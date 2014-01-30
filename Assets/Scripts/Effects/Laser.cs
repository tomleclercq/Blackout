using System.Collections.Generic;
using UnityEngine;

public delegate void LaserHandler(GameObject pHit);
public class Laser : MonoBehaviour
{
    public event LaserHandler OnObjectHit;

    public GameObject _LaserPrefab;
    public Transform[] _LaserPos;
    public Transform _Parent;

    private List<ParticleSystem> _Lasers;


    private bool _IsPlaying;
    public bool IsPlaying
    {
        get { return _IsPlaying; }
    }


	public void ActivateLasers () 
    {
        for(int i = 0; i < _LaserPos.Length; i++)
	    {
            _Lasers.Add(Instantiate(_LaserPrefab, _LaserPos[i].position, Quaternion.identity) as ParticleSystem);
            _Lasers[i].Play(true);
	    }
	    _IsPlaying = true;
    }

	public void DeactivateLasers ()
	{
	    for (int i = 0; i < _Lasers.Count; ++i)
	    {
            _Lasers[i].Stop();

            ParticleSystem obj = _Lasers[i];
	        _Lasers[i] = null;
            Destroy(obj, 5.0f);
        }
	    _IsPlaying = false;
	}

    void Update()
    {
        if (_IsPlaying)
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(_Parent.position, -Vector3.up,out hit, 1000.0f))
            {
                Debug.DrawLine (_Parent.position, hit.point, Color.white);

                if (OnObjectHit != null)
                    OnObjectHit(hit.collider.gameObject);
            }
        }
    }
}
