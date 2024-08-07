using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Maria : Energizable
{
    NavMeshAgent agent;
    public Transform[] points;
    int point;
    public float distanceMin;
    Animator anim;

    bool persiguiendo;
    public Transform player;
    public float DisPlayer;

    Transform transfor;
    public GameObject Explosion;
    public VidaMaria hpSystem;
    public GameObject attackTrigger;
    bool attacking = false;
    public AudioSource audioSource;
    public List<Collider> disableOnDead = new List<Collider>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        transfor = GetComponent<Transform>();
        hpSystem = FindObjectOfType<VidaMaria>();
        agent.speed = 1;
        player = FindObjectOfType<Swat>().transform;
        point = 0;
        persiguiendo = false;
        ChangeRagDollState(false);
    }

    public override void Update()
    {
        base.Update();
        if (persiguiendo == true)
        {
            this.transform.LookAt(player.position);
            if (Vector3.Distance(player.position, transform.position) <= DisPlayer)
            {
                if (!attacking)
                {
                    attacking = true;
                    agent.speed = 0;
                    anim.SetTrigger("Atack");
                    audioSource.Play();
                }
            }
            else 
            {
                agent.SetDestination(player.position);
                anim.SetTrigger("move");
                agent.speed = 1;
                attacking = false;
            }
        }
        else
        {
            agent.SetDestination(points[point].position);
            Vector3 distancia = points[point].position - this.transform.position;
            float magnitude = distancia.magnitude;
            if (distanceMin >= magnitude)
            {
                point = point + 1;
                if (point >= points.Length)
                {
                    point = 0;
                }
            }
        }
    }
    public void CheckForAttack()
    {
        if (Vector3.Distance(player.position, transform.position) <= DisPlayer)
        {
            anim.SetTrigger("Atack");
            agent.speed = 0;
            this.transform.LookAt(player.position);
        }
        else
        {
            attacking = false;
            persiguiendo = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            persiguiendo = true;
            if (agent.isActiveAndEnabled)
                agent.SetDestination(player.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            persiguiendo = false;
            if(agent.isActiveAndEnabled)
             agent.SetDestination(player.position);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            hpSystem.GetDamage();
        }
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
