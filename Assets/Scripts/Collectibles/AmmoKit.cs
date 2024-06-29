using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoKit : Interact
{
    public int rifleAmmoAmt;
    public int rLAmmoAmt;

    [SerializeField] Transform fpsCam;

    public Rifle rifle;
    public RocketLauncher rL;

    public override void InteractObject()
    {
        IncreaseGunAmmo();
        base.InteractObject();      
    }

    public void IncreaseGunAmmo()
    {
        int rifleAmmoDiff = rifleAmmoAmt - rifle.currentAmmo;
        rifle.currentAmmo += rifleAmmoDiff;
        if(rifleAmmoDiff > 0)
        {
            GameManager.Instance.currentRifleAmmoText.text = rifle.currentAmmo.ToString();
            GameManager.Instance.currentRifleAmmoText.color = Color.white;
        }
        rifle.totalAmmo += rifleAmmoAmt;
        if(rifle.totalAmmo > 0)
        {
            GameManager.Instance.totalRifleAmmoText.color = Color.white;
        }
        GameManager.Instance.totalRifleAmmoText.text = rifle.totalAmmo.ToString();


        int rLAmmoDiff = rLAmmoAmt - rL.currentAmmo;
        rL.currentAmmo += rLAmmoDiff;
        if(rLAmmoDiff > 0)
        {
            GameManager.Instance.currentRLAmmoText.text = rL.currentAmmo.ToString();
            GameManager.Instance.currentRLAmmoText.color = Color.white;
        }
        rL.totalAmmo += rLAmmoAmt;
        if (rL.totalAmmo > 0)
        {
            GameManager.Instance.currentRLAmmoText.color = Color.white;
        }
        GameManager.Instance.totalRLAmmoText.text = rL.totalAmmo.ToString();

        AudioManager.Instance.pickUp.Play();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (fpsCam == null) //since projectile game object is not in scene, this is needed to set the fpsCam
        {
            fpsCam = Camera.main.transform; //set player camera
        }
        if(rifle == null && rL == null)
        {
            rifle = GameManager.Instance.rifle;
            rL = GameManager.Instance.rL;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
