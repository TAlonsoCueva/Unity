using UnityEngine;

public class bala : MonoBehaviour
{
    public GameObject holePrefab;
    public float speed;
    public Vector3 direction;
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else 
        {
            Vector3 normal = collision.GetContact(0).normal;
            Vector3 contactoPoint = collision.GetContact(0).point;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.back, normal);
            GameObject bulletHoleClone = Instantiate(holePrefab, contactoPoint, rotation);
            print(bulletHoleClone.name);
            Destroy(bulletHoleClone, 5.5f);
        }
        Destroy(gameObject);
    }
}
