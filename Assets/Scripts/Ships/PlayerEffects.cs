using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour
{
    private enum SPEEDSTATE
    {
        idle, speedup,speeddown
    }

    public AudioSource _SoundTrack;
    public ParticleSystem _Thrusters;
    public ParticleSystem _ThrustersCircle;
    public ParticleSystem _Explosion;
    public ParticleSystem _Addon;

    public GameObject _Ship;
    public GameObject _Shield;
    private Material _ShipMaterial;
    private Material _ShieldMaterial;

    public int _DamageBlinkCount = 3;
    public float _DamageBlinkSpeed = 4.0f;
    public float _PickUpBlinkInSpeed = 4.0f;
    public float _PickUpBlinkOutSpeed = 1.5f;
    public float _ShieldActivateSpeed = 3.0f;
    public float _Speed;

    private bool _IsShieldActivated;
    private SPEEDSTATE _CurrentSpeed = SPEEDSTATE.idle;
    // Use this for initialization
    void Start()
    {
        _SoundTrack.Play();

        _ShipMaterial = _Ship.renderer.material;
        _ShieldMaterial = _Shield.renderer.material;
    }
    public void UpdateThruster(float pSpeed)
    {
        _Thrusters.startSpeed = Mathf.Lerp(0.70f, 2.4f, pSpeed);
        _ThrustersCircle.startSize = Mathf.Lerp(0.1f, 0.48f, pSpeed);
    }

    public void Explode()
    {
        ParticleSystem exposion = Instantiate(_Explosion, this.transform.position, Quaternion.identity) as ParticleSystem;
        AudioManager.Instance.PlaySound("Explosion");
        Destroy(exposion.gameObject, exposion.duration);
    }
    public void PlayGunSound()
    {
        AudioManager.Instance.PlaySound("Spacegun", 1.0f, 0.75f);
    }

    public void PlayDamageEffect()
    {
        if (!_IsShieldActivated)
        {
            AudioManager.Instance.PlaySound("Damage", 1.0f, 0.75f);

            if (_ShipMaterial != null)
                StartCoroutine(DamageAnim(_DamageBlinkCount, _DamageBlinkSpeed));
        }
        else
        {
            AudioManager.Instance.PlaySound("ShieldHit", 1.0f, 0.75f);            
        }
    }
    IEnumerator DamageAnim(int pBlinkCount, float pBlinkSpeed)
    {
        _ShipMaterial.SetFloat("_DamgeSlider", 0.0f);
        _ShipMaterial.SetFloat("_PowerUpSlider", 0.0f);

        float progress = 0.0f;

        for (int i = 0; i < pBlinkCount; ++i)
        {
            while (progress < 1.0f)
            {
                progress += Time.deltaTime*pBlinkSpeed;
                _ShipMaterial.SetFloat("_DamgeSlider", progress);
                    
                yield return null;
            }
            progress = 1.0f;

            while (progress > 0.0f)
            {
                progress -= Time.deltaTime * pBlinkSpeed;
                _ShipMaterial.SetFloat("_DamgeSlider", progress);

                yield return null;
            }
             progress = 0.0f;
        }

        _ShipMaterial.SetFloat("_DamgeSlider", 0.0f);
    }

    public void PlayAddonEffect()
    {
        AudioManager.Instance.PlaySound("Addon", 1.0f, 0.75f);

        if (_ShipMaterial != null)
            StartCoroutine(PickupAnim(true, _PickUpBlinkInSpeed, _PickUpBlinkOutSpeed));
    }
    public void PlayPickupffect()
    {
        AudioManager.Instance.PlaySound("Pickup", 1.0f, 0.75f);

        if (_ShipMaterial != null)
            StartCoroutine(PickupAnim(false, _PickUpBlinkInSpeed, _PickUpBlinkOutSpeed));
    }
    IEnumerator PickupAnim(bool pIsAddon, float pBlinkInSpeed, float pBlinkOutSpeed)
    {
        _ShipMaterial.SetFloat("_DamgeSlider", 0.0f);
        _ShipMaterial.SetFloat("_PowerUpSlider", 0.0f);

        float progress = 0.0f;
        if(pIsAddon)
            _Addon.Play(true);

        while (progress < 1.0f)
        {
            progress += Time.deltaTime * pBlinkInSpeed;
            _ShipMaterial.SetFloat("_PowerUpSlider", progress);
                    
            yield return null;
        }
        progress = 1.0f;

        while (progress > 0.0f)
        {
            progress -= Time.deltaTime * pBlinkOutSpeed;
            _ShipMaterial.SetFloat("_PowerUpSlider", progress);

            yield return null;
        }

        _ShipMaterial.SetFloat("_PowerUpSlider", 0.0f);
    }

    public void ActivateShieldEffect()
    {
        AudioManager.Instance.PlaySound("ShieldUp", 1.0f, 0.75f);

        if (_ShieldMaterial != null)
            StartCoroutine(ShieldpAnim(true, _ShieldActivateSpeed));

        _IsShieldActivated = true;
    }
    public void DeactivateShieldEffect()
    {
        AudioManager.Instance.PlaySound("ShieldDown", 1.0f, 0.75f);

        if (_ShieldMaterial != null)
            StartCoroutine(ShieldpAnim(false, _ShieldActivateSpeed));

        _IsShieldActivated = false;
    }
    IEnumerator ShieldpAnim(bool pIsActivation, float pShieldActivateSpeed)
    {
        float progress = 0.0f;
        if (!pIsActivation)
            progress = 1.0f;

        _ShieldMaterial.SetFloat("_DesolveSlider", progress);

        if (pIsActivation)
        {
            while (progress < 1.0f)
            {
                progress += Time.deltaTime * pShieldActivateSpeed;
                _ShieldMaterial.SetFloat("_DesolveSlider", progress);

                yield return null;
            }
            progress = 1.0f;
        }
        else
        {
            while (progress > 0.0f)
            {
                progress -= Time.deltaTime * pShieldActivateSpeed;
                _ShieldMaterial.SetFloat("_DesolveSlider", progress);

                yield return null;
            }
            progress = 0.0f;
        }

        _ShieldMaterial.SetFloat("_DesolveSlider", progress);
    }

    void PlayShipSound()
    {
        AudioManager.Instance.PlaySound("Spaceship",0.5f,0.5f);
    }
    void ShipSpeedUp()
    {
        StartCoroutine(ShipSpeedSound(1.5f, 2.0f));
    }
    void ShipSpeedDown()
    {
        StartCoroutine(ShipSpeedSound(0.5f, 2.0f));
    }
    void ShipSpeedIdle()
    {
        StartCoroutine(ShipSpeedSound(1.0f, 2.0f));
    }
    IEnumerator ShipSpeedSound(float pPitchTo, float pPitchSpeed)
    {
        float pitch = AudioManager.Instance.GetSoundPitch("Spaceship");

        bool pitchUp = false;
        if (pPitchTo > pitch)
            pitchUp = true;

        if (pitchUp)
        {
            while (pitch < pPitchTo)
            {
                pitch += Time.deltaTime*pPitchSpeed;
                AudioManager.Instance.UpdatePitch("Spaceship", pitch);
                yield return null;
            }
            AudioManager.Instance.UpdatePitch("Spaceship", pPitchTo);
        }
        else
        {
            while (pitch > pPitchTo)
            {
                pitch -= Time.deltaTime * pPitchSpeed;
                AudioManager.Instance.UpdatePitch("Spaceship", pitch);
                yield return null;
            }
            AudioManager.Instance.UpdatePitch("Spaceship", pPitchTo);
        }
    }

    public void RoundWon()
    {
        _SoundTrack.Stop();
        AudioManager.Instance.PlaySound("RoundWon", 1.0f, 1.0f);
    }
    public void RoundLost()
    {
        _SoundTrack.Stop();
        AudioManager.Instance.PlaySound("RoundLost", 1.0f, 1.0f);
    }

}
