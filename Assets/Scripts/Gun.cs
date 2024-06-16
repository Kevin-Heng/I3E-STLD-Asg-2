using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    //Raycast 
    [SerializeField] public Transform fpsCam; //player camera

    //Ammo texts
    public TextMeshProUGUI currentAmmoText; //text to show current ammo in gun
    public TextMeshProUGUI totalAmmoText; //text to show total ammo available

    //Gun firerate
    public float fireRate = 10f; //each shot occurs at an interval, affects nextTimeToShoot var
    public static float nextTimeToShoot = 0f; //can shoot immediately at the start, controls time taken to shoot the next bullet

    //Gun effects
    public GameObject bulletHit; //particle effect when bullet hits an object
    public ParticleSystem muzzleFlash;

    //Gun reload
    public float reloadTime = 1.5f;
    public bool isReloading = false;

    //Gun audio
    public AudioClip gunShot;
    public AudioClip gunReload;
    public AudioClip emptyMag;

    public int damage;

    public int totalAmmo; //total ammo that is stored at the start of the game
    public int currentAmmo; //set to 0 at the start
    public int magazineAmmo; //total of 30 bullets for one magazine, max amount for currentAmmo

    public bool isEquipped;
    public Transform itemParent;

    private void Awake()
    {
        currentAmmo += magazineAmmo;
        currentAmmoText.text = currentAmmo.ToString();
        totalAmmoText.text = totalAmmo.ToString();
    }



    public void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo)) //reduce ammo count if player hits item
        {
            if (currentAmmo > 0) //enough ammo to shoot
            {
                currentAmmo--;
                currentAmmoText.text = currentAmmo.ToString();
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, 0.07f);
                GameObject bulletImpact = Instantiate(bulletHit, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)); //particle effect only appears when it hits an object
                Destroy(bulletImpact, 1.5f); //remove the variable from hierarchy 2s after the particle effect finished
                if (currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                    if (totalAmmo == 0 && currentAmmo == 0)  //no ammo at all
                    {
                        AudioSource.PlayClipAtPoint(emptyMag, fpsCam.position, 0.5f);
                    }
                }
                
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
            if (currentAmmo > 0)//enough ammo to shoot
            {
                currentAmmo--;
                currentAmmoText.text = currentAmmo.ToString(); //update on screen
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, 0.07f);
                if (currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                    if (totalAmmo == 0 && currentAmmo == 0)  //no ammo at all
                    {
                        AudioSource.PlayClipAtPoint(emptyMag, fpsCam.position, 0.5f);
                    }
                }
                
            }

        }
        muzzleFlash.Play();
    }

    public IEnumerator Reload() //reload function such that there is a reload time of 2s
    {
        
        if (currentAmmo < magazineAmmo) //can only reload when current ammo is less than the total ammo for one magazine
        {
            if (totalAmmo > 0) //reload gun if there is enough ammo
            {
                AudioSource.PlayClipAtPoint(gunReload, fpsCam.position, 1f);
                isReloading = true; //player is reloading
                yield return new WaitForSeconds(reloadTime); //pauses the function for 2s which acts reload time, then the code below this statement will run
                int difference = magazineAmmo - currentAmmo; //calculate how much ammo is needed to get current ammo back to 30
                if (difference > totalAmmo) //check if there is enough ammo to increase current ammo back to 30, e.g. current ammo = 20, difference = 10 and total ammo = 5
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

    public void EquipItem() //function to pick up weapon from floor
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo))
        {
            if (hitInfo.transform.CompareTag("Weapon"))
            {
                isEquipped = true;
                GameObject weapon = hitInfo.transform.gameObject;
                GameManager.Instance.weaponsList.Add(weapon);
                weapon.transform.position = itemParent.transform.position;
                weapon.transform.rotation = itemParent.transform.rotation;
                weapon.transform.SetParent(itemParent);
                currentAmmoText.enabled = true;
                totalAmmoText.enabled = true;
                currentAmmoText.text = GameManager.Instance.currentAmmo.ToString(); //update on screen
                totalAmmoText.text = GameManager.Instance.totalAmmo.ToString(); //update on screen
            }

        }
    }


    public void Shooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToShoot && !isReloading && isEquipped) //can hold left click to shoot, shoot function activates in intervals
            if (Input.GetMouseButton(0) && Time.time >= nextTimeToShoot && !isReloading) //can hold left click to shoot, shoot function activates in intervals
            {
                nextTimeToShoot = Time.time + 1 / fireRate; //this var increases as player continues to shoot and, shots fired are constant
                Shoot(); //shoot gun
            }
    }
    public void Reloading()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && isEquipped) //press r to reload gun
        {
            StartCoroutine(Reload()); //reload gun
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
