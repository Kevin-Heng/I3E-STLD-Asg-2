/*
 * Author: Kevin Heng
 * Date: 19/06/2024
 * Description: The AmmoKit class is used to handle ammo increase when player interacts with an ammo kit
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoKit : Interact
{
    /// <summary>
    /// amoount of ammo in one rifle magazine
    /// </summary>
    public int rifleAmmoAmt;
    /// <summary>
    /// amount of ammo in one rocket launcher magazine
    /// </summary>
    public int rLAmmoAmt;
    /// <summary>
    /// Player camera
    /// </summary>
    [SerializeField] Transform fpsCam; 
    /// <summary>
    /// rifle script
    /// </summary>
    public Rifle rifle;
    /// <summary>
    /// rocket launcher script
    /// </summary>
    public RocketLauncher rL;

    /// <summary>
    /// Function to pick up ammo kit
    /// </summary>
    public override void InteractObject()
    {
        IncreaseGunAmmo();
        base.InteractObject();      
    }

    /// <summary>
    /// Funcion to increase gun ammo
    /// </summary>
    public void IncreaseGunAmmo()
    {
        int rifleAmmoDiff = rifleAmmoAmt - rifle.currentAmmo; //difference in rifle ammo in magazine currently and ammo inside one magazine
        rifle.currentAmmo += rifleAmmoDiff; //increase  current rifle ammo by that amount
        if(rifleAmmoDiff > 0)
        {
            GameManager.Instance.currentRifleAmmoText.text = rifle.currentAmmo.ToString(); //update current rifle ammo text 
            GameManager.Instance.currentRifleAmmoText.color = Color.white; //change text colour back to white
        }
        rifle.totalAmmo += rifleAmmoAmt; //increase total rifle ammo by the total ammo in a magazine
        if(rifle.totalAmmo > 0)
        {
            GameManager.Instance.totalRifleAmmoText.color = Color.white; //change text colour back to white
        }
        GameManager.Instance.totalRifleAmmoText.text = rifle.totalAmmo.ToString(); //update total rifle ammo text


        int rLAmmoDiff = rLAmmoAmt - rL.currentAmmo;//difference in rocket launcher ammo in magazine currently and ammo inside one magazine
        rL.currentAmmo += rLAmmoDiff; //increase current rocket launcher ammo by that amount
        if (rLAmmoDiff > 0)
        {
            GameManager.Instance.currentRLAmmoText.text = rL.currentAmmo.ToString(); //update current rocket launcher ammo text
            GameManager.Instance.currentRLAmmoText.color = Color.white; //change text colour back to white
        }
        rL.totalAmmo += rLAmmoAmt; //increase total rocket launcher ammo by the total ammo in a magazine
        if (rL.totalAmmo > 0)
        {
            GameManager.Instance.currentRLAmmoText.color = Color.white; //change text colour back to white
        }
        GameManager.Instance.totalRLAmmoText.text = rL.totalAmmo.ToString(); //update total rocket launcher ammo text
    }

    // Start is called before the first frame update
    void Start()
    {
        if (fpsCam == null) //since projectile game object is not in scene, this is needed to set the fpsCam
        {
            fpsCam = Camera.main.transform; //set player camera
        }
        if(rifle == null && rL == null) //if scripts cannot be referenced into inspector menu
        {
            //set rifle and rL as the ones already in GameManager
            rifle = GameManager.Instance.rifle;
            rL = GameManager.Instance.rL;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
