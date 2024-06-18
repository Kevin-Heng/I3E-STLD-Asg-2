/*
 * Author: Kevin Heng
 * Date: 10/06/2024
 * Description: The Gun class is a parent class which is used to handle the functions of a gun
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    //Player camera 
    [SerializeField] public Transform fpsCam; 

    //Gun firerate
    /// <summary>
    /// Gun fire rate
    /// </summary>
    public float fireRate; //each shot occurs at an interval, affects nextTimeToShoot var
    /// <summary>
    /// When the next bullet can be shot
    /// </summary>
    public static float nextTimeToShoot = 0f; //can shoot immediately at the start, controls time taken to shoot the next bullet

    //Gun effects
    /// <summary>
    /// Particle effect to instantiate when bullet hits an object
    /// </summary>
    public GameObject bulletHit; 
    /// <summary>
    /// Particle effect at barrel of gun when bullet is shot
    /// </summary>
    public ParticleSystem muzzleFlash;
    /// <summary>
    /// Time taken to destory bulletHit game object from hierarchy and game
    /// </summary>
    public float destroyTime;

    //Gun reload
    /// <summary>
    /// Time taken to reload gun
    /// </summary>
    public float reloadTime;
    /// <summary>
    /// Boolean to check if gun is reloading
    /// </summary>
    public bool isReloading = false;

    //Gun audio
    /// <summary>
    /// Audio when bullet is shot
    /// </summary>
    public AudioClip gunShot;
    /// <summary>
    /// Audio when reloading gun
    /// </summary>
    public AudioClip gunReload;
    /// <summary>
    /// Audio when there is no ammo left in gun
    /// </summary>
    public AudioClip emptyMag;
    /// <summary>
    /// Audio sound level for gunShot
    /// </summary>
    public float gunShotSoundLvl;

    /// <summary>
    /// Gun damage
    /// </summary>
    public int damage;

    /// <summary>
    /// Total ammo in gun
    /// </summary>
    public int totalAmmo; 
    /// <summary>
    /// Current ammo in gun magazine
    /// </summary>
    public int currentAmmo;
    /// <summary>
    /// Amount of ammo in one gun magazine
    /// </summary>
    public int magazineAmmo; 
    /// <summary>
    /// Boolean to check if gun is equipped or not
    /// </summary>
    public bool isEquipped;

    /// <summary>
    /// Function to shoot a bullet
    /// </summary>
    public void Shoot()
    {
        RaycastHit hitInfo; //to store info when raycast hits an object
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo)) //reduce ammo count if player hits item
        {
            if (currentAmmo > 0) //check if there is enough ammo to shoot
            {
                GameManager.Instance.ReduceAmmo(ref currentAmmo); 
                muzzleFlash.Play(); 
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, gunShotSoundLvl);
                GameObject bulletImpact = Instantiate(bulletHit, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)); //particle effect only appears when it hits an object
                Destroy(bulletImpact, destroyTime); //remove the variable from hierarchy
                if (currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function 
                }
                GameManager.Instance.NoAmmo(ref currentAmmo, ref totalAmmo, emptyMag, fpsCam);
                
                if (hitInfo.transform.CompareTag("Enemy")) //raycast hits an enemy 
                {
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>(); //access Enemy class 
                    enemy.enemyHp -= damage; //reduce enemy hp by gun damage
                    //enemy dies when hp is less than or equal to 0
                    if (enemy.enemyHp <= 0) 
                    {
                        Destroy(enemy.gameObject);
                    }
                    
                }
            }
            
        }
        else //reduce ammo if player shoots the air
        {
            if (currentAmmo > 0)//enough ammo to shoot
            {
                GameManager.Instance.ReduceAmmo(ref currentAmmo);
                muzzleFlash.Play();
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, gunShotSoundLvl);
                if (currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                }
                GameManager.Instance.NoAmmo(ref currentAmmo, ref totalAmmo, emptyMag, fpsCam);
                
            }

        }
    }

    /// <summary>
    /// Reload function for gun
    /// </summary>
    /// <returns></returns>
    public IEnumerator Reload() //reload function such that there is a reload time 
    {       
        if (currentAmmo < magazineAmmo) //can only reload when current ammo is less than the total ammo for one magazine
        {
            if (totalAmmo > 0) //reload gun if there is enough ammo
            {
                AudioSource.PlayClipAtPoint(gunReload, fpsCam.position, 1f);
                isReloading = true; //player is reloading
                yield return new WaitForSeconds(reloadTime); //pauses the function for the specified reload time, then the code below this statement will run
                GameManager.Instance.ReloadGun(ref currentAmmo, ref magazineAmmo, ref totalAmmo);
                isReloading = false;
            }
        }
    }


    public void Shooting()
    {
        if (Time.time >= nextTimeToShoot && !isReloading && isEquipped) //can hold left click to shoot, shoot function activates in intervals
        {
            nextTimeToShoot = Time.time + 1 / fireRate; //this var increases as player continues to shoot and, shots fired are constant
            Shoot(); //shoot gun
        }
    }
    public void Reloading()
    {
        if (!isReloading && isEquipped) //press r to reload gun
        {
            StartCoroutine(Reload()); //reload gun
        }
    }
    public void OutOfAmmo()
    {
        GameManager.Instance.NoAmmo(ref currentAmmo, ref totalAmmo, emptyMag, fpsCam);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
