﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaMaria : MonoBehaviour
{
    public Transform cam;
    public Image Life;
    float Vida = 1;
    public GameObject enemigo;
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
    public void GetDamage()
    {
        Vida = Vida - 0.2f;
        Life.fillAmount = Vida;

        if (Vida <= 0)
        {
            enemigo.GetComponent<Maria>().RagDoll();
            Destroy(gameObject,5);
            Destroy(enemigo,5);
        }
    }
    public void GetDamage(float d)
    {
        Vida -= d;
        GetDamage();
    }
}
