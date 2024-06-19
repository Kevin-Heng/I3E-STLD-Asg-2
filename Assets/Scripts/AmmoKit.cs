using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoKit : Interact
{
    int ammoAmt;
    public AudioClip pickUp;
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
        if (rifle.isEquipped)
        {
            ammoAmt = 30;
            rifle.totalAmmo += ammoAmt;
            GameManager.Instance.totalRifleAmmoText.text = rifle.totalAmmo.ToString();
        }
        else
        {
            ammoAmt = 4;
            rL.totalAmmo += ammoAmt;
            GameManager.Instance.totalRLAmmoText.text = rL.totalAmmo.ToString();
        }
        AudioSource.PlayClipAtPoint(pickUp, fpsCam.position, 0.7f);

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
