using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate;
    public float damage;
    public AudioClip shootAudio;
    public AudioSource audiosource;

    public int cargadorMax;
    public int cargadorCount;
    [HideInInspector]
    public WeaponManager manager;
    public GameObject bala;
    public GameObject holePrefab;
    public Transform spawnPoint;
    public bool canShoot;
    public Sprite image;
    public bool isAutomatic;
    private void Awake()
    {
        canShoot = true;
        audiosource = GetComponent<AudioSource>();
    }
    public bool CanReload() 
    {
        return cargadorCount <= cargadorMax;
    }
    public void Reload() 
    {
       int balasGastadas = cargadorMax - cargadorCount;
        if (manager.bullets >= balasGastadas)
        {
            cargadorCount += balasGastadas;
        }
        else
        {
            balasGastadas = manager.bullets;
            cargadorCount += manager.bullets;
        }
        manager.bullets -= balasGastadas;
    }
    public string GetAmmoDataString()
    {
        return "Balas" + " " + cargadorMax.ToString() + "\n" + "Cargador" + " " + cargadorCount.ToString() + "\n" + "Total" + " " + manager.bullets.ToString();
    }
    public void PlayAudio() 
    {
        audiosource.PlayOneShot(shootAudio);
    }
    public virtual void Shoot() 
    {
        if (canShoot)
        {
            if (cargadorCount > 0)
            {
                PlayAudio();
                Swat s = FindObjectOfType<Swat>();

                Vector3 screenPoint = new Vector3(Screen.width / 2f, Screen.height / 2f, 5f); // Centro de la pantalla
                Ray raycast = Camera.main.ScreenPointToRay(screenPoint);
                raycast.origin += s.transform.forward * 1.5f;
                foreach(RaycastHit hit in Physics.RaycastAll(raycast, 5000))
                {
                    if (hit.collider.isTrigger)
                        continue;
                    //Cambiar a herencia en un futuro
                    Maria maria = hit.collider.gameObject.GetComponent<Maria>();
                    if (maria != null) 
                    {
                        maria.hpSystem.GetDamage();
                        break;
                    }
                    enemigo enemigo = hit.collider.gameObject.GetComponent<enemigo>();
                    if (enemigo != null)
                    {
                        enemigo.hpSystem.GetDamage();
                        //spawnear particulas de sangre
                        break;
                    }
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.back, hit.normal);
                    GameObject bulletHoleClone = Instantiate(holePrefab, hit.point, rotation);
                    print(bulletHoleClone.name);
                    Destroy(bulletHoleClone, 5.5f);
                }

                cargadorCount--;
                HudController.instance.SetWeaponData(this);
                canShoot = false;
                Invoke("ReactivateShoot", fireRate);
            }
        }
    }
    public void ReactivateShoot() 
    {
        canShoot = true;
    }
    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Swat s = FindObjectOfType<Swat>();
        ray.origin += s.transform.forward * 3;
        Gizmos.DrawRay(ray);
    }
}
