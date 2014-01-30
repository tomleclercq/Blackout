using System.Security.Cryptography;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public float LifeTime = 2.0f;
    public float Damage = 1.0f;
	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(DestroyAfter(LifeTime));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
