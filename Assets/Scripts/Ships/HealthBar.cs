using UnityEngine;
using System.Collections;
using System;

public class HealthBar 
{
    public delegate void DiedDelegate(object sender, EventArgs e);

    public float Health { get; set; }

    private float StartHealth;

    public event DiedDelegate Dei;

    public HealthBar(float pHealth)
    {
        Health = pHealth;
        StartHealth = pHealth;
    }

    public void TakeDamage(float pDamage)
    {
        if ((Health -= pDamage)<= 0)
        {
            if (Dei != null)
                Dei(this, new EventArgs());
        }
    }

    public void Repair()
    {
        Health += StartHealth/5;
    }

}
