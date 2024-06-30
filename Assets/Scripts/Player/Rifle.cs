/*
 * Author: Kevin Heng
 * Date: 13/06/2024
 * Description: The Rifle class, child class of Gun class, is used to handle rifle interactions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    /// <summary>
    /// To reference player capsule in inspector
    /// </summary>
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        isEquipped = true; //rifle is equipped at the start
        currentAmmo = magazineAmmo; //set current ammo to amount of ammo in a magazine
        totalAmmo = originalTotalAmmo; //set total ammo to original total ammo
        GameManager.Instance.currentRifleAmmoText.text = currentAmmo.ToString(); //update rifle ammo 
        GameManager.Instance.totalRifleAmmoText.text = totalAmmo.ToString(); //update rifle total ammo
    }

    // Update is called once per frame
    void Update()
    {
        if (isEquipped) //rifle equipped
        {
            player.GetComponent<Player>().UpdateGun(this);
            AudioManager.Instance.gunShot = AudioManager.Instance.rifleShot; //set audio when rifle shoots
            AudioManager.Instance.gunReload = AudioManager.Instance.rifleReload; //set audio when rifle reloads
        }

    }
}
