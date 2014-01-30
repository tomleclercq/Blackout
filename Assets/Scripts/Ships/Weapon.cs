using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ArtilleryType
{
    NormalCannon,
    RocketLauncer,
    Laser
}


public class Weapon : MonoBehaviour 
{
	public GameObject Bullet;
    public float LaunchSpeed = 50.5f;//
    public Vector3 PlayerLaunch = new Vector3(10.0f, 0.0f);
    public float FireDelay = .5f;

    public Transform Slot;

    private bool CanFire = true;
    //private Bullet BulletScript;

    void Start()
    {
        //BulletScript = Bullet.GetComponent<Bullet>();
    }

	private IList<Bullet> SpawnedBullet = new List<Bullet>();

	public void Fire(Transform target, int multi)
	{
	    if (!CanFire)
	        return;
	    CanFire = false;
	    
		GameObject bul = (GameObject)Instantiate (Bullet, this.transform.position, Slot.rotation);
	    bul.layer = 10;
        SpawnedBullet.Add(bul.GetComponent<Bullet>());
	    Vector3 direction = (Slot.position - target.position);
        direction.Normalize();
        bul.GetComponentInChildren<Bullet>().gameObject.rigidbody.AddForce(direction*LaunchSpeed,ForceMode.Impulse);
        StartCoroutine(WaitForFire(FireDelay));
        //TODO: faster shooting with multi
	}

    public bool PlayerFire()
    {
        if (!CanFire)
            return false;
        CanFire = false;

        GameObject bul = (GameObject)Instantiate(Bullet, this.transform.position, Slot.rotation);
        SpawnedBullet.Add(bul.GetComponent<Bullet>());
        bul.rigidbody.AddForce(PlayerLaunch, ForceMode.Impulse);
        StartCoroutine(WaitForFire(FireDelay));
        return true;
    }

    public IEnumerator WaitForFire(float time)
    {
        yield return new WaitForSeconds(time);
        CanFire = true;
    }

    /*public void Destroy(bool bullets)
    {
        if(!bullets)
            return;
        foreach (var bullet in SpawnedBullet)
        {
            if(bullet)
                Destroy(bullet);
        }
    }*/
}
