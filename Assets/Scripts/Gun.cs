using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : Equip
{
    //Raycast 
    [SerializeField] Transform fpsCam; //player camera

    //Ammo texts
    public TextMeshProUGUI currentAmmoText; //text to show current ammo in gun
    public TextMeshProUGUI totalAmmoText; //text to show total ammo available

    //Gun firerate
    public static float fireRate = 10f; //each shot occurs at an interval, affects nextTimeToShoot var
    static float nextTimeToShoot = 0f; //can shoot immediately at the start, controls time taken to shoot the next bullet

    //Gun effects
    public GameObject bulletHit; //particle effect when bullet hits an object
    public ParticleSystem muzzleFlash;

    //Gun reload
    float reloadTime = 1.5f;
    bool isReloading = false;

    //Gun audio
    public AudioClip gunShot;
    public AudioClip gunReload;
    public AudioClip emptyMag;


    public bool isEquipped = false;
    int damage = 10;


    void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo)) //reduce ammo count if player hits item
        {
            if (GameManager.currentAmmo > 0) //enough ammo to shoot
            {
                GameManager.Instance.ReduceAmmo();
                currentAmmoText.text = GameManager.currentAmmo.ToString();
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, 0.07f);
                GameObject bulletImpact = Instantiate(bulletHit, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)); //particle effect only appears when it hits an object
                Destroy(bulletImpact, 1.5f); //remove the variable from hierarchy 2s after the particle effect finished
                if (GameManager.currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                    currentAmmoText.text = GameManager.currentAmmo.ToString(); //update on screen
                    totalAmmoText.text = GameManager.totalAmmo.ToString(); //update on screen
                    if (GameManager.totalAmmo == 0 && GameManager.currentAmmo == 0)  //no ammo at all
                    {
                        GameManager.Instance.NoAmmo(emptyMag, fpsCam);
                    }
                }
                muzzleFlash.Play();
                if (hitInfo.transform.CompareTag("Enemy"))
                {
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    Debug.Log(enemy.enemyHp);
                    enemy.enemyHp -= damage;
                    if (enemy.enemyHp == 0)
                    {
                        Destroy(enemy.gameObject);

                    }
                    
                }
            }
            
        }
        else //reduce ammo if player shoots the air
        {
            if (GameManager.currentAmmo > 0)//enough ammo to shoot
            {
                GameManager.Instance.ReduceAmmo();
                currentAmmoText.text = GameManager.currentAmmo.ToString(); //update on screen
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, 0.15f);
                if (GameManager.currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                    currentAmmoText.text = GameManager.currentAmmo.ToString(); //update on screen
                    totalAmmoText.text = GameManager.totalAmmo.ToString(); //update on screen
                    if (GameManager.totalAmmo == 0 && GameManager.currentAmmo == 0)  //no ammo at all
                    {
                        GameManager.Instance.NoAmmo(emptyMag, fpsCam);
                    }
                }
                muzzleFlash.Play();
            }

        }
    }

    IEnumerator Reload() //reload function such that there is a reload time of 2s
    {
        if (GameManager.currentAmmo < GameManager.magazineAmmo) //can only reload when current ammo is less than the total ammo for one magazine
        {
            if (GameManager.totalAmmo > 0) //reload gun if there is enough ammo
            {
                AudioSource.PlayClipAtPoint(gunReload, fpsCam.position, 1f);
                isReloading = true; //player is reloading
                yield return new WaitForSeconds(reloadTime); //pauses the function for 2s which acts reload time, then the code below this statement will run
                GameManager.Instance.ReloadGun();
                currentAmmoText.text = GameManager.currentAmmo.ToString(); //update on screen
                totalAmmoText.text = GameManager.totalAmmo.ToString(); //update on screen
                isReloading = false;
            }
        }
    }

    public override void EquipItem()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo))
        {
            if(hitInfo.transform.name == "gunRifle")
            {
                isEquipped = true;
                currentAmmoText.enabled = true;
                totalAmmoText.enabled = true;
                currentAmmoText.text = GameManager.currentAmmo.ToString(); //update on screen
                totalAmmoText.text = GameManager.totalAmmo.ToString(); //update on screen
                base.EquipItem();
            }
            
        }
    }

    





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToShoot && !isReloading && isEquipped) //can hold left click to shoot, shoot function activates in intervals
        {
            nextTimeToShoot = Time.time + 1 / fireRate; //this var increases as player continues to shoot and, shots fired are constant
            Shoot(); //shoot gun

        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && isEquipped ) //press r to reload gun
        {
            StartCoroutine(Reload()); //reload gun
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R) && isEquipped) //press left click or R key once
        {
            GameManager.Instance.NoAmmo(emptyMag, fpsCam); //play no ammo sound
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            EquipItem();
        }
        
    }
}
