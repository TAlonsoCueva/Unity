using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemigo : Energizable
{
    NavMeshAgent agent;
    public Transform[] points;
    int point;
    public float distanceMin;
    public float attackRange;
    public float followRange;
    public Transform player;
    bool persiguiendo;
    public float DisPlayer;
    Animator anim;

    public GameObject bala;
    public Transform instanceBala;
    float timePass;
    public float firerate;
    public float balaSpeed;

    public Vector3 Ray;
    int vida;
    public GameObject Explosion;
    Transform transfor;

    public vidaEnemigo hpSystem;
    int balas;
    bool disparo;
    bool attacking = false;
    public List<Collider> disableOnDead = new List<Collider>();

    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        anim = this.gameObject.GetComponent<Animator>();
        transfor = this.gameObject.GetComponent<Transform>();
        hpSystem = FindObjectOfType<vidaEnemigo>();
        point = 0;
        player = FindObjectOfType<Swat>().transform;
        vida = 5;
    }
    void Start()
    {
        persiguiendo = false;
        balas = 20;
        disparo = true;
        ChangeRagDollState(false);
    }

    public override void Update()
    {
        base.Update();

        float distancia = Vector3.Distance(player.position ,transform.position);
        if (distancia <= followRange) 
        {
            if (distancia <= attackRange)
            {
                anim.SetBool("Shoot", true);
                agent.isStopped = true;
                attacking = true;
                persiguiendo = false;
                this.transform.LookAt(player.position);
                Shoot(player);
            }
            else 
            {
                anim.SetBool("Shoot", false);
                agent.SetDestination(player.position);
                agent.isStopped = false;
                attacking = false;
                persiguiendo = true;
            }
        }
        else 
        {
            anim.SetBool("Shoot", false);
            agent.SetDestination(points[point].position);
            this.transform.LookAt(transform.forward);

            Vector3 d = points[point].position - this.transform.position;
            float magnitude = d.magnitude;

            if (distanceMin >= magnitude)
            {
                point = point + 1;
                if (point >= points.Length)
                {
                    point = 0;

                }
            }
        }
        timePass = timePass + 1 * Time.deltaTime;
    }
  
    private void Shoot(Transform target)
    {
        if (disparo == true)
        {
            if (timePass >= firerate)
            {

                GameObject bulletClone = Instantiate(bala);
                bulletClone.transform.position = instanceBala.position;
                balas = balas - 1;
                Vector3 dir = target.transform.position - bulletClone.transform.position;
                bulletClone.transform.rotation = new Quaternion();

                bulletClone.GetComponent<Rigidbody>().AddForce(dir.normalized * balaSpeed);

                timePass = 0;

                Destroy(bulletClone, 5f);
                if (balas <= 0)
                {
                    disparo = false;
                    anim.SetTrigger("recarga");
                }
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            hpSystem.GetDamage();
        }
    }
    public void Activar()
    {
        disparo = true;
        balas = 20;
    }
    public void RagDoll()
    {
        enabled = false;
        anim.enabled = false;
        agent.speed = 0;
        agent.enabled = false;
        ChangeRagDollState(true);
        foreach (Collider c in disableOnDead)
        {
            c.enabled = false;
        }
    }
    public void ChangeRagDollState(bool state) 
    {
        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.isKinematic = !state;
            rigidbody.useGravity = state;
        }
    }
}

