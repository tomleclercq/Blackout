using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IArtillery
{
    public delegate void PowerupDelegate(object sender, EventArgs e);

    public event PowerupDelegate OnPowerUp;

    public float MAX_HEALTH = 15f;

    public Blackout _Blackout = null;

    public Transform StartPos;

    public int Level { get; private set; }
    public int Strenght { get; private set; }
    public float StrenghtMultiplier { get; private set; }

    public int NrOfAllowedWeapons = 1;

    public Animator _Animator = null;
    public Transform _Bounds = null;
    private HealthBar _HealthBar;
    private HealthBar _ShieldBar = new HealthBar(0);

    float _RefHorizontal = 0;
    float _RefVertical = 0;

    public Transform[] Slots;
    private IList<Transform> FreeSlots;
    public GameObject[] PossibleWeapons;

    private int buttonPressd = 0;
    private float horizontalForce;
    private float verticalForce;

    private float DashMultiplier = 1;
    private bool WasReleased = false;
    private bool IsPressed;
    private int PrevDirectionInput = 0;
    private bool CanDash = false;
    private float DashCooldown = 0;
    private float DashCooldownTime = 0.2f;
    private int _LastDash = 0;
    private int _PrevPressedButton = 0;

    private List<GameObject> EqWeapGos = new List<GameObject>(); 
    private IList<Weapon> EquippedWeapons = new List<Weapon>();
    private List<int> SavedWeapons = new List<int>(); 
    private List<int> CurrentWeapons = new List<int>(); 

	private PlayerEffects _PlayerEffects;
    private bool _IsInilialized;
    public static int PlayerLives = 3;

    public event EventHandler PlayerDied;

    public int Health 
    {
        get
        {
            return (int)_HealthBar.Health;
            
        }
    }


	// Use this for initialization
	void Start ()
	{
        PlayerLives = 3;
        _PlayerEffects = GetComponent<PlayerEffects>();
        _HealthBar = new HealthBar(MAX_HEALTH);

        EnemySpawner.WaveFinished += EnemySpawner_WaveFinished;
	    FreeSlots = Slots.ToList();
        
        AddWeapon(0);
        NextWave();

        _ShieldBar.Dei += _ShieldBar_Dei;
        _HealthBar.Dei += OnPlayerDeath;

	    _IsInilialized = true;
	}

    void EnemySpawner_WaveFinished(object sender, EventArgs e)
    {
        NextWave();
    }

    /// <summary>
    /// Occurs when the player's health bar is depleted
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void OnPlayerDeath(object sender, EventArgs e)
    {
        bool returnToMenu = false;
        if (--PlayerLives <= 0)
            returnToMenu = true;

        StartCoroutine(DelayDeath(returnToMenu));
        //Application.LoadLevel(Application.loadedLevel);   
    }

    IEnumerator DelayDeath(bool pReturnToMenu)
    {
        StatisticsManager.Instance.DisplayText("You died!", 0.0f);

        if (PlayerDied != null)
            PlayerDied(this, new EventArgs());

        _PlayerEffects.Explode();
        _ShieldBar.Health = 0;

        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if(child.renderer != null)
                child.renderer.enabled = false;
            if (child.collider != null)
                child.collider.enabled = false;
        }

        if (!pReturnToMenu)
        {
            yield return new WaitForSeconds(1.0f);
            foreach (Transform child in allChildren)
            {
                if (child.renderer != null)
                    child.renderer.enabled = true;
                if (child.collider != null)
                    child.collider.enabled = true;
            }

            _HealthBar.Health = MAX_HEALTH;
            gameObject.transform.position = StartPos.position;
            StatisticsManager.Instance.SetHealth(MAX_HEALTH);

            ResetWeapons();
        }

        StatisticsManager.Instance.SetLifes(PlayerLives);
        if (pReturnToMenu)
        {
            returnToMain();
        }
    }

    private void returnToMain()
    {
        StatisticsManager.Instance.DisplayText("GAME OVER!",0.0f);
        StartCoroutine(GoBackToMain());
    }

    private IEnumerator GoBackToMain()
    {
        yield return new WaitForSeconds(3.0f);
        Application.LoadLevel(0);
    }

    private void ResetWeapons()
    {
        Debug.Log("resetting weapons");
        var temp = EquippedWeapons.ToArray();
        foreach (var equippedWeapon in temp)
        {
            RemoveWeapon(equippedWeapon);
        }
        foreach (int i in SavedWeapons)
        {
            Debug.Log("Adding weapon: "+i);
            AddWeapon(i);
        }
    }

    private void RemoveWeapon(Weapon equippedWeapon)
    {
        int index = EquippedWeapons.IndexOf(equippedWeapon);
        EquippedWeapons.RemoveAt(index);
        EqWeapGos.RemoveAt(index);
        CurrentWeapons.RemoveAt(index);
        FreeSlots.Add(equippedWeapon.Slot);

        Destroy(equippedWeapon.gameObject.transform.parent.gameObject);
    }

    void _ShieldBar_Dei(object sender, EventArgs e)
    {
        _PlayerEffects.DeactivateShieldEffect();
    }

    public List<GameObject> GetEquippedWeapons()
    {
        return EqWeapGos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale >= 1.0f)
                Time.timeScale = 0.0f;
            else
                Time.timeScale = 1.0f;

            return;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        ProcessInput();
    }

    private void ProcessInput()
    {

        if (_Blackout == null || _Animator == null || _Bounds == null) return;

        if (!Orientation.Instance.InTransition)
        {
            if (Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Space))
                Fire();

            if (Input.GetButton("Fire2") || Input.GetKeyDown(KeyCode.LeftAlt))
                _Blackout.Switch();

            float horizontalForce = Input.GetAxis("Horizontal");
            float verticalForce = Input.GetAxis("Vertical");
            float unitHorizontalForce = horizontalForce * 0.10f;
            float unitVerticalForce = verticalForce * 0.10f;

            Dash();

            Vector3 pos = transform.position;

            if (Orientation.Instance.OrientationAxis == Axis.Top)
                transform.Translate(-unitVerticalForce * DashMultiplier, 0, unitHorizontalForce * DashMultiplier);
            else
                transform.Translate(0, unitVerticalForce * DashMultiplier, unitHorizontalForce * DashMultiplier);

            if (_RefHorizontal == horizontalForce && _RefHorizontal == 0)
                _Animator.SetBool("HorizontalStill", true);
            else
                _Animator.SetBool("HorizontalStill", false);

            if (_RefVertical == verticalForce && _RefVertical == 0)
                _Animator.SetBool("VerticallStill", true);
            else
                _Animator.SetBool("VerticallStill", false);


            if (Orientation.Instance.OrientationAxis == Axis.Top)
            {
                _Animator.SetFloat("VerticalSpeed", verticalForce * DashMultiplier);
                //_Animator.SetFloat("HorizontalSpeed", horizontalForce);
            }
            else
            {
                //_Animator.SetFloat("VerticalSpeed", verticalForce);
                _Animator.SetFloat("HorizontalSpeed", verticalForce * DashMultiplier);
            }

            _RefHorizontal = horizontalForce;
            _RefVertical = verticalForce;

            _PlayerEffects.UpdateThruster(horizontalForce);


            if (!_Bounds.collider.bounds.Contains(transform.position))
                transform.position = pos;
        }

    }

    private void Dash()
    {
        horizontalForce = Input.GetAxisRaw("Horizontal");
        verticalForce = Input.GetAxisRaw("Vertical");

        buttonPressd = 0;

        if (Math.Abs(horizontalForce - (-1f)) < 0.1f) //left button;
            buttonPressd = 1;
        else if (Math.Abs(horizontalForce - 1f) < 0.1f)  //rightButton
            buttonPressd = 3;

        if (Math.Abs(verticalForce - 1f) < 0.1f) //left up;
            buttonPressd = 2;
        else if (Math.Abs(verticalForce - (-1f)) < 0.1f) //down
            buttonPressd = 4;

        if (PrevDirectionInput != buttonPressd)
        {
            if (buttonPressd != 0)
                ButtonPressed(buttonPressd);
            else
                ButtonReleased();
        }
        PrevDirectionInput = buttonPressd;

        UpdateDashwWindow();
    }
    private void UpdateDashwWindow()
    {
        DashCooldown += Time.deltaTime;
        if (DashCooldown < DashCooldownTime)
        {
            CanDash = true;
            _LastDash = 0;
        }
        else
        {
            CanDash = false;
            _LastDash = 0;
        }
    }

    private void ButtonPressed(int pButton)
    {
        if (WasReleased)
        {
            if (CanDash)
            {
                if (_PrevPressedButton == pButton && _LastDash != pButton)
                {
                    Debug.Log("Dasch");
                    StartCoroutine(DashRoutine());
                    _LastDash = pButton;
                }
            }
            WasReleased = false;
            _PrevPressedButton = pButton;
        }
        if (pButton != 0)
            DashCooldown = 0;
    }


    private void ButtonReleased()
    {
        WasReleased = true;
    }


    private IEnumerator DashRoutine()
    {
        DashMultiplier = 3;

        yield return new WaitForSeconds(0.3f);
        DashMultiplier = 1;
    }

    public void Fire()
    {
        bool succes = false;
        foreach (Weapon weapon in EquippedWeapons)
        {
            if(weapon.gameObject != null)
            succes = weapon.PlayerFire();
        }

        if(succes && EquippedWeapons != null)
            _PlayerEffects.PlayGunSound();
    }

    void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag.ToString())
        {
            case "Bullet":
                Bullet bul = collision.gameObject.GetComponent<Bullet>();
                TakeDamage(bul.Damage);
                Destroy(collision.gameObject);
                break;
            case "Enemy":
                 TakeDamage(collision.gameObject.GetComponent<EnemyBase>().Collision(10.0f));
                break;
            case "PowerUp":
                
                ApplyPowerUp(collision.gameObject.GetComponent<PowerUp>());
                if (OnPowerUp != null) OnPowerUp(this, new EventArgs());
                Destroy(collision.gameObject);
                break;
        }
    }

    private void ApplyPowerUp(PowerUp powerup)
    {
        switch (powerup.PowerUpType)
        {
            case PowerUpType.Minigun:
                AddWeapon(1);
                break;
            case PowerUpType.Shield:
                // Activate visual shield
                _PlayerEffects.ActivateShieldEffect();
                // Add shield points
               _ShieldBar.Health = Health;
                break;
            case PowerUpType.MissileLauncher:
                AddWeapon(2);
                break;
            case PowerUpType.Repair:
                _HealthBar.Repair();
                break;
        }

        _PlayerEffects.PlayPickupffect();
    }

    public void TakeDamage(float damage)
    {
        // Shield bar first
        if (_ShieldBar.Health <= 0)
        {
            _HealthBar.TakeDamage(damage);
            UiManager.Instance.SetHealth(_HealthBar.Health/ MAX_HEALTH);
            _PlayerEffects.PlayDamageEffect();
        }
        else
        {
            if (_ShieldBar.Health < 0)
                _HealthBar.TakeDamage(_ShieldBar.Health * -1);
            _ShieldBar.TakeDamage(damage);


        }
    }

    void AddWeapon(int i)
    {
        if (!PossibleWeapons[i])
        {
            Debug.Log("Weapon not found!");
            return;
        }

        if (_IsInilialized)
            _PlayerEffects.PlayAddonEffect();

        Transform slot;
        if (FreeSlots.Count >= NrOfAllowedWeapons)
        {
            slot = FreeSlots[0];
            FreeSlots.RemoveAt(0);
        }
        else
        {
            //Debug.Log("Destroyed old weapon");
            Weapon old = EquippedWeapons[0];
            RemoveWeapon(old);

            slot = FreeSlots[0];
            FreeSlots.RemoveAt(0);
        }

        GameObject weapon = (GameObject)Instantiate(PossibleWeapons[i], slot.position, slot.rotation);
        EqWeapGos.Add(PossibleWeapons[i]);
        CurrentWeapons.Add(i);
        EquippedWeapons.Add(weapon.GetComponentInChildren<Weapon>());
        //Debug.Log("Equipped weapons: " +EqWeapGos.Count.ToString());
        weapon.transform.parent = this.transform;
        weapon.GetComponentInChildren<Weapon>().Slot = slot;

    }

    public void NextWave()
    {
        SavedWeapons = new List<int>();
        foreach (var i in CurrentWeapons)
        {
            SavedWeapons.Add(i);
            Debug.Log("Saving Weapon");
        }

    }
}
