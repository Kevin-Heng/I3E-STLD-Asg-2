using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    //Raycast 
    [SerializeField] public Transform fpsCam; //player camera

    //Gun firerate
    public float fireRate = 10f; //each shot occurs at an interval, affects nextTimeToShoot var
    public static float nextTimeToShoot = 0f; //can shoot immediately at the start, controls time taken to shoot the next bullet

    //Gun effects
    public GameObject bulletHit; //particle effect when bullet hits an object
    public ParticleSystem muzzleFlash;
    public float destroyTime;

    //Gun reload
    public float reloadTime = 1.5f;
    public bool isReloading = false;

    //Gun audio
    public AudioClip gunShot;
    public AudioClip gunReload;
    public AudioClip emptyMag;
    public float audioSound;

    public int damage;

    public int totalAmmo; //total ammo that is stored at the start of the game
    public int currentAmmo; //set to 0 at the start
    public int magazineAmmo; //total of 30 bullets for one magazine, max amount for currentAmmo

    public bool isEquipped;

    public void Shoot()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo)) //reduce ammo count if player hits item
        {
            if (currentAmmo > 0) //enough ammo to shoot
            {
                GameManager.Instance.ReduceAmmo(ref currentAmmo);
                muzzleFlash.Play();
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, audioSound);
                GameObject bulletImpact = Instantiate(bulletHit, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)); //particle effect only appears when it hits an object
                Destroy(bulletImpact, destroyTime); //remove the variable from hierarchy 2s after the particle effect finished
                if (currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                    GameManager.Instance.NoAmmo(ref currentAmmo, ref totalAmmo, emptyMag, fpsCam);
                }
                
                if (hitInfo.transform.CompareTag("Enemy"))
                {
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    Debug.Log(enemy.enemyHp);
                    enemy.enemyHp -= damage;
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
                AudioSource.PlayClipAtPoint(gunShot, fpsCam.position, audioSound);
                if (currentAmmo == 0) //magazine is empty
                {
                    StartCoroutine(Reload()); //reload function runs
                    GameManager.Instance.NoAmmo(ref currentAmmo, ref totalAmmo, emptyMag, fpsCam);
                }
                
            }

        }
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
                GameManager.Instance.ReloadGun(ref currentAmmo, ref magazineAmmo, ref totalAmmo);
                isReloading = false;
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
    public void OutOfAmmo()
    {
        if(Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))
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
