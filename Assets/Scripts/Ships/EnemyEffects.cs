using UnityEngine;
using System.Collections;

public class EnemyEffects : MonoBehaviour
{
    public GameObject _Ship;
    public GameObject _Shield;
    private Material _ShipMaterial;
    private Material _ShieldMaterial;

    public float _PickUpBlinkInSpeed = 4.0f;
    public float _PickUpBlinkOutSpeed = 1.5f;
    public float _ShieldActivateSpeed = 3.0f;

    private bool _IsShieldActivated;

    void Start()
    {
        _ShipMaterial = _Ship.renderer.material;
        _ShieldMaterial = _Shield.renderer.material;
    }

    public void PlayExplosionSound()
    {
        AudioManager.Instance.PlaySound("Explosion", 1.0f, 0.65f);
    }
    public void PlayGunSound()
    {
        AudioManager.Instance.PlaySound("Spacegun", 1.0f, 0.25f);
    }

    public void PlayDamageEffect()
    {
        if (!_IsShieldActivated)
        {
            AudioManager.Instance.PlaySound("Damage", 1.0f, 0.35f);    
        }
        else
        {
            AudioManager.Instance.PlaySound("ShieldHit", 1.0f, 0.35f);            
        }
    }

    public void PlayAddonEffect()
    {
        AudioManager.Instance.PlaySound("Addon", 1.0f, 0.15f);

        if (_ShipMaterial != null)
            StartCoroutine(PickupAnim(_PickUpBlinkInSpeed, _PickUpBlinkOutSpeed));
    }
    IEnumerator PickupAnim( float pBlinkInSpeed, float pBlinkOutSpeed)
    {
        _ShipMaterial.SetFloat("_DamgeSlider", 0.0f);
        _ShipMaterial.SetFloat("_PowerUpSlider", 0.0f);

        float progress = 0.0f;

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
        AudioManager.Instance.PlaySound("ShieldDown_Instrumental", 1.0f, 0.15f);

        if (_ShieldMaterial != null)
            StartCoroutine(ShieldpAnim(true, _ShieldActivateSpeed)); 

        _IsShieldActivated = true;
    }
    public void DeactivateShieldEffect()
    {
        AudioManager.Instance.PlaySound("ShieldUp", 1.0f, 0.15f);

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

	// Update is called once per frame
	void Update ()
	{

	}
}
