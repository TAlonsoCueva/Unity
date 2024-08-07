using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponTransform;
    public List<Weapon> startingWeapons = new List<Weapon>();
    public List<Weapon> equippedWeapons = new List<Weapon>();
    int equippedWeaponIndex = 0;
    public int bullets;
    public bool canShoot = true;
    private void Awake()
    {
        GenerateStartingWeapons();
    }
    public void GenerateStartingWeapons() 
    {
        foreach (Weapon w in startingWeapons)
        {
            Weapon instanciatedW = Instantiate(w, weaponTransform);
            instanciatedW.gameObject.transform.localPosition = Vector3.zero;
            AddWeapon(instanciatedW);
            w.gameObject.SetActive(false);
        }
        SwapWeapon(0);
    }
    public void AddWeapon(Weapon w) 
    {
        equippedWeapons.Add(w);
        w.manager = this;
    }
    public void AddBullets(int extraBullets) 
    {
        bullets += extraBullets;
        HudController.instance.SetWeaponData(CurrentWeapon());
    }
    public void SwapWeapon(int dir) 
    {
        CurrentWeapon().gameObject.SetActive(false);

        equippedWeaponIndex += dir;
        if (equippedWeaponIndex >= equippedWeapons.Count)
            equippedWeaponIndex = 0;
        if (equippedWeaponIndex < 0)
            equippedWeaponIndex = equippedWeapons.Count - 1;

        CurrentWeapon().gameObject.SetActive(true);
        canShoot = false;
        CancelInvoke();
        Invoke("ResetShoot",3.2f);
        HudController.instance.SetWeaponData(CurrentWeapon());
        GetComponent<Swat>().animator.SetBool("knife", CurrentWeapon().GetComponent<Knife>() != null);
    }
    public Weapon CurrentWeapon() 
    {
        return equippedWeapons[equippedWeaponIndex];
    }
   
    public bool CanReload() 
    {
       return CurrentWeapon().CanReload();
    }
    public void ResetShoot()
    {
        canShoot = true; ;
    }

}
