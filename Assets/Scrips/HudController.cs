using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudController : MonoBehaviour
{
    public static HudController instance;
    public Text bulletsText;
    public Text cargadorBullets;
    public Image currentWeaponImage;
    public Image energyImage;
    public Image LifeImage;

    public GameObject CanvasWin;
    public GameObject CanvasOver;

    private void Awake()
    {
        instance = this;
    }
    public void SetWeaponData(Weapon w) 
    {
        currentWeaponImage.sprite = w.image;
        bulletsText.text = w.manager.bullets.ToString();
        cargadorBullets.text = w.cargadorCount.ToString();
    }
    public void SetEnergyFillAmount(float percentage)
    {
        energyImage.fillAmount = percentage;
    }
    public void SetHpFillAmount(float percentage)
    {
        LifeImage.fillAmount = percentage;
    }
}
