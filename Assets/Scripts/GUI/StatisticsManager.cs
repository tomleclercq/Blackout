using System.IO;
using UnityEngine;
using System.Collections;

public class StatisticsManager : MonoBehaviour
{
    public int Score { get; private set; }
    public float PlayerHealth { get; private set; }
    public float Blackout { get; private set; }
    public int Wave { get; private set; }
    public int Lives { get; private set; }
    public int[] Slots { get; private set; }
    public bool Sield { get; private set; }
    public float ScoreMultiplier { get; private set; }
    public float DamageMultiplier { get; private set; }
    public float WeaponDamageMultiplier { get; private set; }

    private string _WaveName;
    public string WaveName
    {
        get { return _WaveName; }
        set
        {
            _WaveName = value;
            UiManager.Instance.SetWaveName(_WaveName);
            StartCoroutine(SetWaveName(1.0f));
        } 
    }

    private int _HitCounter = 0;

    #region Singleton;
    private static readonly object _Rooth = new object();
    private static StatisticsManager _Instance = null;

    public static StatisticsManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<StatisticsManager>();
            }
            return _Instance;
        }
    }

    #endregion;

    void Start()
    {
        EnemyBase.OnDeath += OnEnemyDie;
    }

    void OnDestroy()
    {
        EnemyBase.OnDeath -= OnEnemyDie;
    }

    IEnumerator SetWaveName(float time)
    {
        yield return new WaitForSeconds(time);
        _WaveName = "";
        UiManager.Instance.SetWaveName(_WaveName);
    }

    public void NewWave()
    {
        ++Wave;
    }

    public float _HitCooldown = 5;
    public float _HitCoolTime = 0;
    public void AddHitCounter()
    {
        ++_HitCounter;
        _HitCoolTime = 0;

        if (_HitCounter%5 == 0)
        {
            float multiplier =  1 +_HitCounter/10f;

            if (multiplier > 5)
                multiplier = 5;

            UiManager.Instance.SetScoreMultiplier("X " + multiplier.ToString());
        }

        if (!_CoolingDown)
            StartCoroutine(HitCooldown());
    }

    public bool _CoolingDown = false;
    private IEnumerator HitCooldown()
    {
        _CoolingDown = true;

        while (_HitCoolTime < _HitCooldown)
        {
            yield return new WaitForEndOfFrame();
            _HitCoolTime += Time.deltaTime;
        }

        _HitCounter = 0;
        ScoreMultiplier = 1;

        UiManager.Instance.SetScoreMultiplier("X " + ScoreMultiplier.ToString());
        _CoolingDown = false;
    }

    public void UpdateBlackout(float pBlackout)
    {
        Blackout = pBlackout;

        UiManager.Instance.SetBlackout(Blackout*0.001f);
    }

    public void AddToScore(int pScore)
    {
        Score += pScore;
        UiManager.Instance.SetScore(Score.ToString());
    }

    public void OnEnemyDie(object sender, EnemyEventArgs e)
    {
        AddToScore(e.Enemy.RewardPoints);
        AddHitCounter();
    }

    public void SetLifes(int pLives)
    {
        Lives = pLives;
        UiManager.Instance.SetActiveLifes(Lives);
    }
    public void SetHealth(float pHealth)
    {
        PlayerHealth = pHealth;
        UiManager.Instance.SetHealth(pHealth);
    }

    public void DisplayText(string text)
    {
        UiManager.Instance.SetWaveName(text);
        StartCoroutine(SetWaveName(1.0f));
    }
    public void DisplayText(string text, float showTime)
    {
        UiManager.Instance.SetWaveName(text);

        if (showTime != 0.0f)
        {
            StartCoroutine(SetWaveName(showTime));
        }
    }
}
