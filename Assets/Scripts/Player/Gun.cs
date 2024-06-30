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
    /// <summary>
    /// Player camera 
    /// </summary>
    [SerializeField] public Transform fpsCam;

    [Header("Gun firerate")]
    /// <summary>
    /// Gun fire rate
    /// </summary>
    public float fireRate; //each shot occurs at an interval, affects nextTimeToShoot var
    /// <summary>
    /// When the next bullet can be shot
    /// </summary>
    public static float nextTimeToShoot = 0f; //can shoot immediately at the start, controls time taken to shoot the next bullet

    [Header("Gun effects")]
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

    [Header("Gun reload")]
    /// <summary>
    /// Time taken to reload gun
    /// </summary>
    public float reloadTime;
    /// <summary>
    /// Boolean to check if gun is reloading
    /// </summary>
    public bool isReloading = false;

    [Header("Sound level")]
    /// <summary>
    /// Audio sound level for gunShot
    /// </summary>
    public float gunShotSoundLvl;

    [Header("Gun damage")]
    /// <summary>
    /// Gun damage
    /// </summary>
    public int damage;

    [Header("Gun ammo")]
    /// <summary>
    /// Total ammo in gun
    /// </summary>
    public int totalAmmo;
    /// <summary>
    /// total ammo in gun at the start of the game
    /// </summary>
    public int originalTotalAmmo;
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
    /// ammo UI
    /// </summary>
    public GameObject ammoIcon;

    [Header("Enemy")]
    /// <summary>
    /// Enemy script
    /// </summary>
    Enemy enemy;
    /// <summary>
    /// Value for RNG for enemy to drop ammo or med kit
    /// </summary>
    public int maxNumRange;
    /// <summary>
    /// med kit prefab
    /// </summary>
    public GameObject medKit;
    /// <summary>
    /// ammo kit prefab
    /// </summary>
    public GameObject ammoKit;

    /// <summary>
    /// Function to shoot a bullet
    /// </summary>
    public virtual void Shoot()
    {
        RaycastHit hitInfo; //to store info when raycast hits an object
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo)) //reduce ammo count if player hits item
        {
            if (currentAmmo > 0) //check if there is enough ammo to shoot
            {
                GameManager.Instance.ReduceAmmo(ref currentAmmo);
                muzzleFlash.Play();
                //AudioSource.PlayClipAtPoint(AudioManager.Instance.gunShot, fpsCam.position, gunShotSoundLvl);
                AudioManager.Instance.gunShot.Play();
                GameObject bulletImpact = Instantiate(bulletHit, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)); //particle effect only appears when it hits an object
                Destroy(bulletImpact, destroyTime); //remove the variable from hierarchy
                
                if (currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function 
                    if (totalAmmo == 0) //no ammo at all
                    {
                        AudioManager.Instance.emptyMag.Play(); //play audio source
                        if (isEquipped)
                        {
                            //current and total ammo text set to red to warn player
                            GameManager.Instance.currentRifleAmmoText.color = Color.red;
                            GameManager.Instance.totalRifleAmmoText.color = Color.red;
                        }
                        else
                        {
                            //current and total ammo text set to red to warn player
                            GameManager.Instance.currentRLAmmoText.color = Color.red;
                            GameManager.Instance.totalRLAmmoText.color = Color.red;
                        }
                    }
                }

                DamageEnemy(damage, hitInfo); //damage enemy
            }

        }
        else //reduce ammo if player shoots the air
        {
            if (currentAmmo > 0)//enough ammo to shoot
            {
                GameManager.Instance.ReduceAmmo(ref currentAmmo);
                muzzleFlash.Play();
                //AudioSource.PlayClipAtPoint(AudioManager.Instance.gunShot, fpsCam.position, gunShotSoundLvl);
                AudioManager.Instance.gunShot.Play();
                if (currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                    if (totalAmmo == 0) //no ammo at all
                    {
                        AudioManager.Instance.emptyMag.Play(); //play audio source
                        if (isEquipped)
                        {
                            //current and total ammo text set to red to warn player
                            GameManager.Instance.currentRifleAmmoText.color = Color.red;
                            GameManager.Instance.totalRifleAmmoText.color = Color.red;
                        }
                        else
                        {
                            //current and total ammo text set to red to warn player
                            GameManager.Instance.currentRLAmmoText.color = Color.red;
                            GameManager.Instance.totalRLAmmoText.color = Color.red;
                        }
                    }
                }
                

            }

        }
        if (hitInfo.transform.CompareTag("Projectile")) //player hits enemy projectile, destroy it
        {
            Destroy(hitInfo.transform.gameObject);
        }
    }

    /// <summary>
    /// Function to calculate damage done to an enemy
    /// </summary>
    /// <param name="damage"> damage done by gun </param>
    /// <param name="hitInfo"> raycast hitInfo </param>
    public void DamageEnemy(int damage, RaycastHit hitInfo)
    {
        if (hitInfo.transform.TryGetComponent<Enemy>(out enemy)) //raycast hits an enemy 
        {
            enemy.enemyHp -= damage; //reduce enemy hp by gun damage
            enemy.enemyHpText.text = enemy.enemyHp.ToString() + "/" + enemy.originalEnemyHp.ToString(); //update enemy hp text
            
            if (enemy.enemyHp <= 0) //enemy dies when hp is less than or equal to 0
            {
                Destroy(enemy.gameObject); //destroy enemy 
                RandomDrop(); //drop ammo or med kit
                GameManager.Instance.bleedingFrame.SetActive(false); //turn off player damaged, bleeding UI
            }
        }
    }

    /// <summary>
    /// Function for enemy to drop ammo or med kit based on RNG
    /// </summary>
    public void RandomDrop()
    {
        int randomNum = Random.Range(0, maxNumRange); //get random number between 0 (inclusive) and maxNumRange (exclusive)
        if(randomNum == 1 || randomNum == 2)
        {
            //spawn med kit if randomNum = 1 or = 2, destroy after 6s
            GameObject medKitInstance = Instantiate(medKit, enemy.transform.position, medKit.transform.rotation);
            Destroy(medKitInstance, 6f);
        }
        else if(randomNum == 4 || randomNum == 5)
        {
            //spawn ammo kit if randomNum = 4 or = 5, destroy after 6s
            GameObject ammoKitInstance = Instantiate(ammoKit, enemy.transform.position, ammoKit.transform.rotation);
            Destroy(ammoKitInstance, 6f);
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
                AudioManager.Instance.gunReload.Play(); //reload audio source
                isReloading = true; //player is reloading
                yield return new WaitForSeconds(reloadTime); //pauses the function for the specified reload time, then the code below this statement will run
                GameManager.Instance.ReloadGun(ref currentAmmo, ref magazineAmmo, ref totalAmmo);
                isReloading = false;
            }
        }
    }

    /// <summary>
    /// Function for player input to shoot gun
    /// </summary>
    public void Shooting()
    {
        if (Time.time >= nextTimeToShoot && !isReloading && isEquipped) //can hold left click to shoot, shoot function activates in intervals
        {
            nextTimeToShoot = Time.time + 1 / fireRate; //this var increases as player continues to shoot and, shots fired are constant
            Shoot(); 
            OutOfAmmo(); 
        }
    }
    /// <summary>
    /// Function for player input to reload gun
    /// </summary>
    public void Reloading()
    {
        if (!isReloading && isEquipped) //check if player is not reloading and gun is equipped
        {
            StartCoroutine(Reload()); //reload gun
            OutOfAmmo();
        }
    }
    /// <summary>
    /// Function to check if player has run out of ammo and chanage text to red
    /// </summary>
    public void OutOfAmmo()
    {
        if (totalAmmo == 0) //no ammo at all
        {
            AudioManager.Instance.emptyMag.Play(); //play audio source
            if (isEquipped)
            {
                //current and total ammo text set to red to warn player
                GameManager.Instance.currentRifleAmmoText.color = Color.red;
                GameManager.Instance.totalRifleAmmoText.color = Color.red;
            }
            else
            {
                //current and total ammo text set to red to warn player
                GameManager.Instance.currentRLAmmoText.color = Color.red;
                GameManager.Instance.totalRLAmmoText.color = Color.red;
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

    }
}