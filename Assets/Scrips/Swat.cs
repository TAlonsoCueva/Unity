using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swat: Energizable
{
    float horizontal;
    float vertical;
    public float speed;

    [HideInInspector]
    public Animator animator;

    public float jumpEnergyCost;
    public float jumpForce;

    float Life;

    [HideInInspector]
    public Rigidbody rb;
    public float sprintEnergyCost;
    bool sprinting;
    bool canSprint;
    public float crouchSpeed;
    [HideInInspector]
    public bool isCrouched;
    public Vector3 crouchedCameraOffset;
    public float sprintCD;
    CameraController cameraController;
    [HideInInspector]
    public WeaponManager weaponManager;
    public List<AudioClip> pisadas = new List<AudioClip>();
    AudioSource audioSource;
    private void Awake()
    {
        canSprint = true;
        currentEnergy = maxEnergy;
        animator = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Life = 1;
        cameraController = FindObjectOfType<CameraController>();
        weaponManager = GetComponent<WeaponManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ResetSprint() 
    {
        canSprint = true;
    }
    public override void Update()
    {
        base.Update();
        
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        if (isCrouched)  
        {
            transform.Translate(new Vector3(horizontal, 0, vertical) * Time.deltaTime * crouchSpeed);
        }
        else if (sprinting && canSprint) 
        {
            transform.Translate(new Vector3(horizontal, 0, vertical) * Time.deltaTime * speed * 1.5f);
            if (CanUseEnergy(sprintEnergyCost * Time.deltaTime))
            {
                UseEnergy(sprintEnergyCost * Time.deltaTime);
            }
            else 
            {
                sprinting = false;
                canSprint = false;
                Invoke("ResetSprint", sprintCD);
            }

        }else
             transform.Translate(new Vector3(horizontal, 0, vertical) * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.C))
        {
            Crouch();
        }
        else
            UnCrouch();
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }
        if (sprinting && !Input.GetKey(KeyCode.LeftShift))
            Invoke("ResetSprint", 0.5f);

        sprinting = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.F))
        {
            weaponManager.SwapWeapon(1);
            animator.SetTrigger("cambioArma");
        }
        if (Input.GetMouseButton(0) && weaponManager.CurrentWeapon().isAutomatic)
        {
            Shoot();
        }else
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(weaponManager.CanReload())
            {
                weaponManager.CurrentWeapon().Reload();
                HudController.instance.SetWeaponData(weaponManager.CurrentWeapon());
                animator.SetTrigger("reloading");
                audioSource.Play();
            }
        }
    }
    public void Jump() 
    {
        if (!CanUseEnergy(jumpEnergyCost))
            return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        UseEnergy(jumpEnergyCost);
    }
    public bool CanUseEnergy(float e) 
    {
        return currentEnergy >= e;
    }
    public void UseEnergy(float e) 
    {
        currentEnergy -= e;
        energyRegeneration = energyRegenerationBase;
        HudController.instance.SetEnergyFillAmount(currentEnergy/maxEnergy);
    }
    void Shoot()
    {
        if(weaponManager.canShoot)
            weaponManager.CurrentWeapon().Shoot();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            Life = Life - 0.1f;

            HudController.instance.SetHpFillAmount(Life);

            if (Life <= 0)
            {
                HudController.instance.CanvasOver.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (collision.gameObject.tag == "Botiquin")
        {
            if (Life >= 1)
                return;

            Life = Life + 0.2f;

            HudController.instance.SetHpFillAmount(Life);

            Destroy(collision.gameObject);

            if (Life >= 1)
            {
                Life = 1;
            }
        }
    }
    public void Crouch() 
    {
        isCrouched = true;
        cameraController.offset = crouchedCameraOffset;
    }
    public void UnCrouch()
    {
        isCrouched = false;
        cameraController.offset = Vector3.zero;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Win")
        {
            HudController.instance.CanvasWin.SetActive(true);
            Time.timeScale = 0;
        }
        if (other.gameObject.tag == "Sword")
        {
            Life = Life - 0.3f;

            HudController.instance.SetHpFillAmount(Life);

            if (Life <= 0)
            {
                HudController.instance.CanvasOver.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    public override bool CanRegenerateEnergy()
    {
        return base.CanRegenerateEnergy() && !sprinting;
    }
    public override void OnUpdateEnergy()
    {
       HudController.instance.SetEnergyFillAmount(currentEnergy / maxEnergy);
    }
    public void PisadaSoundEffect() 
    {
        audioSource.PlayOneShot(pisadas[Random.Range(0,pisadas.Count)]);
    }
}

