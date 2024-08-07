using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vidaEnemigo : MonoBehaviour
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
        Vida = Vida - 0.1f;
        Life.fillAmount = Vida;

        if(Vida <= 0)
        {
            enemigo.GetComponent<enemigo>().RagDoll();
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
