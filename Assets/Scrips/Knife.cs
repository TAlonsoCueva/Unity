using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void Shoot()
    {
        if (canShoot)
        {
            PlayAudio();
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            //logica daño melee
            transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<Animator>().SetTrigger("Attack");
             Invoke("ReactivateShoot", fireRate);
         }
    }
}
