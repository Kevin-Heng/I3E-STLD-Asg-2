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
            GameManager.Instance.Shoot(fpsCam, gunShot, emptyMag, gunReload, bulletHit, muzzleFlash, reloadTime, isReloading);

        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading) //press r to reload gun
        {
            StartCoroutine(GameManager.Instance.Reload(gunReload, fpsCam, reloadTime, isReloading));
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.NoAmmo(emptyMag, fpsCam);
        }
    }
}
