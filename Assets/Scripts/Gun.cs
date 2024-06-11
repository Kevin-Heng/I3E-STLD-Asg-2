using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    //Raycast 
    [SerializeField] Transform fpsCam; //player camera

    //Ammo texts
    public TextMeshProUGUI currentAmmoText; //text to show current ammo in gun
    public TextMeshProUGUI totalAmmoText; //text to show total ammo available

    //Gun ammo
    public static int totalAmmo = 0; //total ammo that is stored at the start of the game
    public static int currentAmmo; //set to 0 at the start
    public static int magazineAmmo = 30; //total of 30 bullets for one magazine, max amount for currentAmmo

    //Gun firerate
    public static float fireRate = 5f; //each shot occurs at an interval, affects nextTimeToShoot var
    static float nextTimeToShoot = 0f; //can shoot immediately at the start, controls time taken to shoot the next bullet

    //Gun effects
    public GameObject bulletHit; //particle effect when bullet hits an object

    //Gun reload
    float reloadTime = 1.5f;
    bool isReloading = false;

    public AudioClip gunShot;
    public AudioClip gunReload;
    public AudioClip emptyMag;


    void Shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo)) //reduce ammo count if player hits item
        {
            if(currentAmmo > 0) //enough ammo to shoot
            {
                currentAmmo--; //ammo reduce by 1 when 1 bullet is shot
                currentAmmoText.text = currentAmmo.ToString(); //update current ammo count on screen
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, 0.15f);
                GameObject bulletImpact = Instantiate(bulletHit, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)); //particle effect only appears when it hits an object
                Destroy(bulletImpact,2f); //remove the variable from hierarchy 2s after the particle effect finished
                if (currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                    if (totalAmmo == 0 && currentAmmo == 0)  //no ammo at all
                    {
                        AudioSource.PlayClipAtPoint(emptyMag, fpsCam.position, 1f); //emptyMag sound plays
                    }
                }
                
            } 
        }
        else //reduce ammo if player shoots the air
        {
            if (currentAmmo > 0)//enough ammo to shoot
            {
                currentAmmo--; //ammo reduce by 1 when 1 bullet is shot
                currentAmmoText.text = currentAmmo.ToString(); //update current ammo count on screen
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, 0.15f);
                if (currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                    if (totalAmmo == 0 && currentAmmo == 0)  //no ammo at all
                    {
                        AudioSource.PlayClipAtPoint(emptyMag, fpsCam.position, 1f); //emptyMag sound plays
                    }
                }
            }

        }
    }

   IEnumerator Reload() //reload function such that there is a reload time of 2s
    {
        if(currentAmmo < magazineAmmo) //can only reload when current ammo is less than the total ammo for one magazine
        {
            if(totalAmmo > 0) //reload gun if there is enough ammo
            {
                AudioSource.PlayClipAtPoint(gunReload, fpsCam.position, 1f);
                isReloading = true; //player is reloading
                yield return new WaitForSeconds(reloadTime); //pauses the function for 2s which acts reload time, then the code below this statement will run

                int difference = magazineAmmo - currentAmmo; //calculate how much ammo is needed to get current ammo back to 30
                if(difference > totalAmmo) //check if there is enough ammo to increase current ammo back to 30, e.g. current ammo = 20, difference = 10 and total ammo = 5
                {
                    currentAmmo += totalAmmo; //increase current ammo by the remainder ammo
                    totalAmmo -= totalAmmo; //total ammo is now 0
                }
                else
                {
                    totalAmmo -= difference; //reduce total ammo by the required amount to increase current ammo back to 30
                    currentAmmo += difference; //current ammo increase back to 30
                }
                currentAmmoText.text = currentAmmo.ToString(); //update on screen
                totalAmmoText.text = totalAmmo.ToString(); //update on screen
                isReloading = false;
            }

            
        }


    }

    void NoAmmo()
    {
        if (totalAmmo == 0 && currentAmmo == 0)
        {
            AudioSource.PlayClipAtPoint(emptyMag, fpsCam.position, 1f);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        currentAmmo += magazineAmmo; //start with 30 ammo
        currentAmmoText.text = currentAmmo.ToString(); //show starting amt on screen
        totalAmmoText.text = totalAmmo.ToString(); //show starting amt on screen
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToShoot && !isReloading) //can hold left click to shoot, shoot function activates in intervals
        {
            nextTimeToShoot = Time.time + 1 / fireRate; //this var increases as player continues to shoot and, shots fired are constant
            Shoot();

        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading) //press r to reload gun
        {
            StartCoroutine(Reload());
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R))
        {
            NoAmmo();
        }
    }
}
