using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public int bullets;
    public void PickPack(WeaponManager manager) 
    {
        manager.AddBullets(bullets);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Swat swat = collision.gameObject.GetComponent<Swat>();
        if (swat != null) 
        {
            PickPack(swat.weaponManager);
        }
    }
}
