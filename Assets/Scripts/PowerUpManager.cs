using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PowerUpManager : MonoBehaviour
{
    private readonly int LuckyNumber = 3;
    private readonly int MaxValue = 10;

    public PowerUp PowerUpPrefab = null;
    public Transform AfterWavePowerUpSpawn = null;

    // Use this for initialization
	void Start () 
    {
        EnemyBase.OnDeath += EnemyBase_OnDeath;
        EnemySpawner.WaveFinished += EnemySpawner_WaveFinished;
	}

    void OnDestroy()
    {
        EnemyBase.OnDeath -= EnemyBase_OnDeath;
        EnemySpawner.WaveFinished -= EnemySpawner_WaveFinished;
    }

    void EnemySpawner_WaveFinished(object sender, EventArgs e)
    {
        if(AfterWavePowerUpSpawn != null)
            SpawnRandomPowerUp(AfterWavePowerUpSpawn.position);
        else
        {
            SpawnRandomPowerUp(gameObject.transform.position);
        }
    }

    #region Singleton
    private static object _Syncroot = new object();
    private static PowerUpManager _Singleton = null;
    public static PowerUpManager Singleton
    {
        get
        {
            lock (_Syncroot)
            {
                if (_Singleton == null)
                {
                    _Singleton = FindObjectOfType<PowerUpManager>();
                }

                return _Singleton;
            }
        }
    } 
    #endregion

    /// <summary>
    /// Event gets called whenever an enemy dies
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void EnemyBase_OnDeath(object sender, EnemyEventArgs e)
    {
        int number = Random.Range(0, MaxValue);
        if (number == LuckyNumber)
        {
            SpawnRandomPowerUp(e.Enemy.transform.position);
        }
        //powerUp.
    }

    private void SpawnRandomPowerUp(Vector3 position)
    {
        PowerUp powerUp = Instantiate(PowerUpPrefab, position, Quaternion.identity) as PowerUp;
        powerUp.PowerUpType = (PowerUpType) Random.Range(0, Enum.GetValues(typeof (PowerUpType)).Length);
        powerUp.PickRightMaterial();
    }

    // Update is called once per frame
	void Update ()
    {
	
	}
}
