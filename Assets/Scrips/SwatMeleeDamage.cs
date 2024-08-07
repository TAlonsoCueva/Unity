using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatMeleeDamage : Energizable
{
    public Swat s;
    private void OnTriggerEnter(Collider other)
    {
        enemigo e = other.GetComponent<enemigo>();
        if (e != null)
            e.hpSystem.GetDamage(s.weaponManager.CurrentWeapon().damage);
        VidaMaria m = other.GetComponent<VidaMaria>();
        if (m != null)
            m.GetDamage(s.weaponManager.CurrentWeapon().damage);
    }
}
