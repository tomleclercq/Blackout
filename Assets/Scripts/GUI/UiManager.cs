using System.ComponentModel.Design;
using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour 
{
    public Renderer[] WeaponRenderers = new Renderer[0];
    public Renderer[] LiveREnderers = new Renderer[0];

    public Renderer BlackoutRenderer = null;
    public Renderer HealthRenderer = null;

    public TextMesh Score = null;
    public TextMesh ScoreMultiplier = null;
    public TextMesh DamageMultiplier = null;
    public TextMesh Wave = null;

    #region Singleton;
    private static readonly object _Rooth = new object();
    private static UiManager _Instance = null;

    public bool InTransition { get; private set; }

    public static UiManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<UiManager>();
            }
            return _Instance;
        }
    }


    private UiManager()
    {
        
    }

    #endregion;

    private void Start()
    {
        for (int i = 0; i < WeaponRenderers.Length; i++)
        {
            WeaponRenderers[i].sharedMaterial = new Material(WeaponRenderers[i].material);
            LiveREnderers[i].sharedMaterial = new Material(LiveREnderers[i].material);
        }
    }


    public void SetActiveWeapons(int [] pWeapons)
    {
        for (int i = 0; i < pWeapons.Length; i++)
        {
            if(pWeapons[i] > 0)
                WeaponRenderers[i].material.SetFloat("_XOffset",1f);
            else
                WeaponRenderers[i].material.SetFloat("_XOffset", 0f);

        }
    }

    public void SetActiveLifes(int pLives)
    {
        Debug.Log("lives " + pLives + "LiveREnderers.Length " + LiveREnderers.Length);
        int lCounter = pLives;
        for (int i = 0; i < LiveREnderers.Length; i++)
        {
            if (i < lCounter)
            {
                LiveREnderers[i].sharedMaterial.SetFloat("_XOffset", 1f);
                Debug.Log("lives off = 1");
            }
            else
            {
                LiveREnderers[i].sharedMaterial.SetFloat("_XOffset", 0f);
                Debug.Log("lives off = 0");
            }
                

        }
    }

    public void SetHealth(float pValue)
    {
        pValue = 1 - pValue;
        HealthRenderer.material.SetFloat("_XOffset", pValue);
    }

    public void SetBlackout(float pValue)
    {
        pValue = 1 - pValue;
        BlackoutRenderer.material.SetFloat("_XOffset", pValue);
    }

    public void SetScore(string pString)
    {
        Score.text = pString;
    }

    public void SetScoreMultiplier(string pString)
    {
        ScoreMultiplier.text = pString;
    }

    public void SetDamageMultiplier(string pString)
    {
        DamageMultiplier.text = pString;
    }

    public void SetWaveName(string pName)
    {
        Debug.LogError("SET");
        Wave.text = pName;
    }
}
