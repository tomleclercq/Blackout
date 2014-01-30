using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using System.Collections;

public class EnemyEventArgs : EventArgs
{
    public EnemyEventArgs(EnemyBase enemy)
    {
        Enemy = enemy;
    }
    public EnemyBase Enemy;
}

public class EnemyBase : MonoBehaviour
{
    public delegate void EnemyDelegate(object sender, EnemyEventArgs e);

    public static event EnemyDelegate OnDeath;
	public static event EnemyDelegate OnHit;

    private float Health;
    public float StartingHealth = 2.0f;
    public float ColideDamage = 2.0f;
	public float ExplosionRange = 2.0f;
	public float ExplosionDamage = 1.0f;
	public Transform Bounds;
    public int RewardPoints = 10;

    public bool IsUpgraded = false;

    public int multi;

    public Transform[] Slots = new Transform[3];
    private IList<Weapon> EquippedWeapons = new List<Weapon>();
    private bool _IsInitialized;

    public EnemyEffects Effects;
    /// <summary>
    /// Disabled for blackout?
    /// </summary>
    public bool Disabled
    {
        get;
        set;
    }

	// Use this for initialization
	void Start () {
		//iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath("EnemyPath1"), "time", 5, "loopType", "loop", "orientToPath", true, "delay", 0));
	    Health = StartingHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
        if(!Disabled)
	        Fire();
        else
        {
            Debug.Log("can't fire");
        }
	}

    void Fire()
    {
        foreach (Weapon weapon in EquippedWeapons)
        {
            weapon.Fire(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>().transform, multi);
        }
    }

	/// <summary>
	/// The enemybase deals AOE Damage to everyone in a near vicinity
	/// </summary>
	/// <value>The explode and deal AO.</value>
	public void ExplodeAndDealAOE()
	{
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, ExplosionRange);
		foreach (var item in colliders)
		{
			EnemyBase enemyBase = item.GetComponent<EnemyBase>();
			if (enemyBase != null && enemyBase.Health > 0)
			{
				enemyBase.TakeDamage(ExplosionDamage);
				return;
			}

			Player player = item.GetComponent<Player>();
			if(player != null)
			{
				player.TakeDamage(ExplosionDamage);
			}
		}
	}

    void OnTriggerEnter(Collider collision)
    {

		if (Bounds.collider.bounds.Contains(transform.position) && collision.gameObject && collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bul = collision.gameObject.GetComponent<Bullet>();
            if (!bul)
            {
                Debug.Log("bul is null");
                return;
            }
            if (bul.gameObject.layer == 10) return;
            TakeDamage(bul.Damage);
            Destroy(collision.gameObject);

			if (OnHit != null)
				OnHit(this, new EnemyEventArgs(this));
        }
    }

    private void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0.0f)
            Destroyed();
        else
            Effects.PlayDamageEffect();
    }

    private void Destroyed()
    {
        if (OnDeath != null) OnDeath(this, new EnemyEventArgs(this));
        Destroy(this.gameObject);
        //TODO: Add explosion + points increase + ....

        Effects.PlayExplosionSound();
		ExplodeAndDealAOE();
    }

    public void LoopAnim()
    {
        //Debug.LogError("Restarting loop");
        GetComponent<iTweenEvent>().Play();
    }

    public float Collision(float damage)
    {
        TakeDamage(damage);
        return ColideDamage;
    }

    public void AddWeapons(List<GameObject> weapons)
    {
        if(weapons.Count()>3) Debug.LogError("Too many weapons");

        if (_IsInitialized)
            Effects.PlayAddonEffect();

        foreach (Weapon equippedWeapon in EquippedWeapons)
        {
            if (equippedWeapon != null)
                Destroy(equippedWeapon.gameObject.transform.parent.gameObject);
        }

        EquippedWeapons = new List<Weapon>();

        //Debug.Log("Adding " + weapons.Count().ToString() + " weapons to an enemy");
        int i = 0;
        foreach (var wp in weapons)
        {
            GameObject weapon = (GameObject)Instantiate(wp, Slots[i].position, Slots[i].rotation);
            weapon.GetComponentInChildren<Weapon>().Bullet.layer = 11;
            Weapon script = weapon.GetComponentInChildren<Weapon>();
            script.FireDelay *= 3;
            EquippedWeapons.Add(script);
            weapon.transform.parent = this.transform;
            weapon.GetComponentInChildren<Weapon>().Slot = Slots[i];
        }

        _IsInitialized = true;
    }

   /* public void Remove(bool bullets)
    {
        foreach (Weapon weapon in EquippedWeapons)
        {
            weapon.Destroy(bullets);
        }
    }*/

}
