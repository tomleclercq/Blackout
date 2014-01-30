using System;
using UnityEngine;
using System.Collections;

public class BlackOutStateChangedEventArgs: EventArgs
{
    public bool StateChange
    {
        get;
        set;
    }

    public BlackOutStateChangedEventArgs(bool stateChange)
    {
        StateChange = stateChange;
    }
}

public class Blackout : MonoBehaviour 
{
    public Light _DirectionalLight = null;
    public Light _PointLight = null;

    private float _BlackoutCoolTime = 3;

    public float BlackoutValue = 0;
    public bool _CoolDown = false;

    //public bool BlackoutValue { get; private set; }
    private bool _IsInBlackout;
    public bool IsInBlackout 
    {
        get
        {
            return _IsInBlackout;
        }
        set
        {
            _IsInBlackout = value;
            EventHandler<BlackOutStateChangedEventArgs> temp = BlackedOutStateChange;
            if (temp != null)
            {
                temp(this, new BlackOutStateChangedEventArgs(_IsInBlackout));
            }
        }
    }

    private float BlackoutFillRate = 50;
    private float BlackoutFillMultiplier = 1;
    private float BlackoutDrainMultiplier= 3;

    public static event EventHandler<BlackOutStateChangedEventArgs> BlackedOutStateChange;
    

    public void Start()
    {
        IsInBlackout = false;
    }

    public void Update()
    {
        FillBlackout();
        DrainBlackout();

        UiManager.Instance.SetBlackout(BlackoutValue/1000f);
    }

    private void FillBlackout()
    {
        if (IsInBlackout) return;

        BlackoutValue += (Time.deltaTime *(BlackoutFillRate * BlackoutFillMultiplier));
        if (BlackoutValue > 1000)
            BlackoutValue = 1000;
    }

    private void DrainBlackout()
    {
        if (!IsInBlackout) return;

        BlackoutValue -= (Time.deltaTime *(BlackoutFillRate * BlackoutDrainMultiplier));
        if (BlackoutValue <= 0)
        {
            BlackoutValue = 0;

            _DirectionalLight.enabled = true;
            _PointLight.enabled = false;
            RenderSettings.ambientLight = new Color(51f / 255, 51f / 255, 51f / 255, 1);
            IsInBlackout = false;

            StartCoroutine(CoolDownRoutine());
        }
    }

    private IEnumerator CoolDownRoutine()
    {
        _CoolDown = true;
        yield return new WaitForSeconds(_BlackoutCoolTime);
        _CoolDown = false;
    }


    public void StartBlackout()
    {
        
        MakeDark(true);
    }

    public void EndBlackout()
    {
        MakeDark(false);
    }

    public void Switch()
    {
        if (IsInBlackout)
            EndBlackout();
        else
            StartBlackout();
    }


    private void MakeDark(bool pValue)
    {
        if (_CoolDown) return;
        StatisticsManager.Instance.WaveName = "Blackout!";
        if (pValue)
        {
            _DirectionalLight.enabled = false;
            _PointLight.enabled = true;
            RenderSettings.ambientLight = new Color(19/255,19/255,19/255,1);
            IsInBlackout = true;
        }
        else
        {
            _DirectionalLight.enabled = true;
            _PointLight.enabled = false;
            RenderSettings.ambientLight = new Color(51f / 255, 51f / 255, 51f / 255, 1);
            IsInBlackout = false;
        }

        StartCoroutine(CoolDownRoutine());
    }
    
}
