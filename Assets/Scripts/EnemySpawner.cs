using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
    //public GameObject[] enemy;
    public GameObject[] Waves;
    public Player Player;

    public ParticleSystem _DeathParticle;
    public ParticleSystem _HitParticle;
    public Transform Bounds;

    public IList<GameObject> Enemies = new List<GameObject>();

    //Upgrading enemies
    private bool UpgradesRunning = false;

    public static event EventHandler WaveFinished;

    private int NextWave = 0;
    private bool CancelWave = false;

    /// <summary>
    /// For use with black out
    /// </summary>
    private bool DisableNewEnemies = false;
    private bool IsInitialized = false;

	// Use this for initialization
	void Start ()
	{
        Player.PlayerDied += PlayerOnPlayerDied;
        Player.OnPowerUp += Player_OnPowerUp;
	   	EnemyBase.OnHit += HandleOnHit;
		EnemyBase.OnDeath += HandleOnDeath;
        Blackout.BlackedOutStateChange += Blackout_BlackedOutStateChange;

		if (Bounds == null)
		{
			Debug.LogError("No bounds was set in enemy spawner");
		}
	}

    void OnDestroy()
    {
        Player.PlayerDied -= PlayerOnPlayerDied;
        Player.OnPowerUp -= Player_OnPowerUp;
        EnemyBase.OnHit -= HandleOnHit;
        EnemyBase.OnDeath -= HandleOnDeath;
        Blackout.BlackedOutStateChange -= Blackout_BlackedOutStateChange;
    }

    void Player_OnPowerUp(object sender, EventArgs e)
    {
        //Debug.Log("Powerup, upgrading " +UpgradedEnemies.Count +" enemies");
        foreach (var enemyBase in Enemies)
        {
            EnemyBase script = enemyBase.GetComponent<EnemyBase>();
                if(script.IsUpgraded)
                    script.AddWeapons(Player.GetEquippedWeapons());
        }
    }

    void Blackout_BlackedOutStateChange(object sender, BlackOutStateChangedEventArgs e)
    {
        if(e.StateChange)
            Debug.Log("disabling enemies");
        else
            Debug.Log("enabling enemies");
        foreach (var enem in Enemies)
        {
            enem.GetComponent<EnemyBase>().Disabled = e.StateChange;
        }

        DisableNewEnemies = e.StateChange;
    }

    private void PlayerOnPlayerDied(object sender, EventArgs eventArgs)
    {
        foreach (var enem in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //Debug.Log("Destroying enemy");
            Destroy(enem);
        }
        foreach (var bullit in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            //Debug.Log("Destroying enemy");
            Destroy(bullit);
        }
        foreach (var POWWAHHHH in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            //Debug.Log("Destroying enemy");
            Destroy(POWWAHHHH);
        }
        Enemies = new List<GameObject>();
        --NextWave;
    }

    void HandleOnDeath (object sender, EnemyEventArgs e)
	{
        //Debug.Log("Enemy died");
		if (_DeathParticle != null)
		{
			ParticleSystem particle = Instantiate(_DeathParticle,e.Enemy.transform.position, _DeathParticle.transform.rotation)as ParticleSystem;
			Destroy(particle.gameObject, _DeathParticle.duration);
		}
		
		Enemies.Remove(e.Enemy.gameObject);
        //UpgradedEnemies.Remove(e.Enemy);
	}

	void HandleOnHit (object sender, EnemyEventArgs e)
	{
		if (_HitParticle != null)
		{
			ParticleSystem particle = Instantiate(_HitParticle,e.Enemy.transform.position, _HitParticle.transform.rotation) as ParticleSystem;
			Destroy(particle.gameObject, _HitParticle.duration);
		}
	}

	// Update is called once per frame
	void Update ()
	{
	    if (!UpgradesRunning)
	        StartCoroutine(Upgrade());

	    int waveId = NextWave%Waves.Count();
	    if (waveId < 0)
	        waveId = 0;

	    int multi = NextWave/(waveId+1);
        if (Enemies.Count == 0 && Waves[waveId])
	    {
	        EventHandler temp = WaveFinished;
	        if (temp != null)
	        {
	            temp(this, new EventArgs());
	        }
	        StartCoroutine(SpawnWave(waveId, multi));
            
	        ++NextWave;

	        if (Player.PlayerLives > 0)
	        {
                StartCoroutine(DelayWaveText());
	        }
	    }
	}
    IEnumerator DelayWaveText()
    {
        if (IsInitialized)
            yield return new WaitForSeconds(1.0f);

        string name = "Wave " + NextWave.ToString();
        StatisticsManager.Instance.WaveName = name;
        Debug.Log("Starting wave: " + name);

        IsInitialized = true;
        yield return null;
    }

    IEnumerator Upgrade()
    {
        UpgradesRunning = true;
        yield return new WaitForSeconds(3.0f);
        UpgradeRandomShip();
        UpgradesRunning = false;
    }

    void UpgradeRandomShip()
    {
        if (Enemies.Count == 0) return;
        EnemyBase script = Enemies[UnityEngine.Random.Range(0, Enemies.Count - 1)].GetComponent<EnemyBase>();
        if(script.IsUpgraded) return;
        script.AddWeapons(Player.GetEquippedWeapons());
        //UpgradedEnemies.Add(script);
        script.IsUpgraded = true;
    }

	void SpawnEnemy(GameObject enemy, int multi)
	{
		GameObject enem = (GameObject) Instantiate (enemy, this.transform.position, this.transform.rotation);
	    //iTweenEvent script = enem.GetComponent<iTweenEvent>();
		EnemyBase script = enem.GetComponent<EnemyBase>();
        script.Bounds = Bounds;
	    script.StartingHealth += multi;
        
        if (enem.GetComponent<iTweenEvent>().Values["looktarget"]==null)
            enem.GetComponent<iTweenEvent>().Values["looktarget"] = GameObject.Find("PlayerOne").transform;

	    enem.GetComponent<EnemyBase>().Disabled = DisableNewEnemies;
        Enemies.Add(enem);
        //script.Values.ContainsValue()
	}

    IEnumerator SpawnWave(int waveId, int multi)
    {
        Wave wave = Waves[waveId].GetComponent<Wave>();

        int count = wave.Enemy.Count();
        for (int i = 0; i < wave.NumberOfEnemies + 2 * multi; ++i)
        {
            if (CancelWave)
            {
                CancelWave = false;
                break;
            }
            SpawnEnemy(wave.Enemy[i%count], multi);
            //Debug.Log((i%count).ToString()+", "+i.ToString());
            yield return new WaitForSeconds(wave.Spacing);
        }
    }
}
