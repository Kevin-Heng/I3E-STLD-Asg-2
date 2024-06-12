using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    //Raycast 
    [SerializeField] Transform fpsCam; //player camera

    //Gun firerate
    public static float fireRate = 5f; //each shot occurs at an interval, affects nextTimeToShoot var
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

    void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo)) //reduce ammo count if player hits item
        {
            if (GameManager.currentAmmo > 0) //enough ammo to shoot
            {
                GameManager.Instance.ReduceAmmo();
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, 0.15f);
                GameObject bulletImpact = Instantiate(bulletHit, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)); //particle effect only appears when it hits an object
                Destroy(bulletImpact, 2f); //remove the variable from hierarchy 2s after the particle effect finished
                if (GameManager.currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                    if (GameManager.totalAmmo == 0 && GameManager.currentAmmo == 0)  //no ammo at all
                    {
                        AudioSource.PlayClipAtPoint(emptyMag, fpsCam.position, 1f); //emptyMag sound plays
                    }
                }
                muzzleFlash.Play();

            }
        }
        else //reduce ammo if player shoots the air
        {
            if (GameManager.currentAmmo > 0)//enough ammo to shoot
            {
                GameManager.Instance.ReduceAmmo();
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, 0.15f);
                if (GameManager.currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                    if (GameManager.totalAmmo == 0 && GameManager.currentAmmo == 0)  //no ammo at all
                    {
                        AudioSource.PlayClipAtPoint(emptyMag, fpsCam.position, 1f); //emptyMag sound plays
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
                isReloading = false;
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

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToShoot && !isReloading) //can hold left click to shoot, shoot function activates in intervals
        {
            nextTimeToShoot = Time.time + 1 / fireRate; //this var increases as player continues to shoot and, shots fired are constant
            Shoot(); //shoot gun

        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading) //press r to reload gun
        {
            StartCoroutine(Reload()); //reload gun
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R)) //press left click or R key once
        {
            GameManager.Instance.NoAmmo(emptyMag, fpsCam); //play no ammo sound
        }
    }
}
