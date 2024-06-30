/*
 * Author: Kevin Heng
 * Date: 17/06/2024
 * Description: The RocketLauncher class, a child class of Gun class, is used to handle rocket launcher interactions
 */

using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Gun
{
    /// <summary>
    /// Change player speed depenig on gun equipped
    /// </summary>
    public float changeSpeed;

    /// <summary>
    /// To reference player capsule in inspector
    /// </summary>
    [SerializeField] GameObject player;

    /// <summary>
    /// Function to change player speed depending on the gun equipped
    /// </summary>
    /// <param name="player"> Reference Player class </param>
    public void ChangeSpeed(Player player)
    {      
        if (isEquipped) //rocket launcher is equipped
        {            
            float currentSpeed = player.GetComponent<FirstPersonController>().MoveSpeed; //get current player speed
            float currentSprint = player.GetComponent<FirstPersonController>().SprintSpeed; //get currrent sprint speed

            currentSpeed -= changeSpeed; //change player speed
            currentSprint -= changeSpeed; //change sprint speed

            player.GetComponent<FirstPersonController>().MoveSpeed = currentSpeed; //set new player speed
            player.GetComponent<FirstPersonController>().SprintSpeed = currentSprint; //set new sprint speed
        }
        else //rocket launcher is unequipped
        {
            float currentSpeed = player.GetComponent<FirstPersonController>().MoveSpeed; //get currrent player speed
            float currentSprint = player.GetComponent<FirstPersonController>().SprintSpeed; //get currrent sprint speed

            currentSpeed += changeSpeed; //change player speed
            currentSprint += changeSpeed; //change sprint speed

            player.GetComponent<FirstPersonController>().MoveSpeed = currentSpeed; //set new player speed
            player.GetComponent<FirstPersonController>().SprintSpeed = currentSprint; //set new sprint speed
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = magazineAmmo; //set current ammo to magazine ammo
        totalAmmo = originalTotalAmmo; //set total ammo to original total ammo
        isEquipped = false; //rocket launcher is unequipped at the start
        this.gameObject.SetActive(false); //hide rocket launcher
    }

    // Update is called once per frame
    void Update()
    {
        if (isEquipped) //rocket launcher equipped
        {
            player.GetComponent<Player>().UpdateGun(this);
            AudioManager.Instance.gunShot = AudioManager.Instance.rLShot; //set audio when rocket launcher shoots
            AudioManager.Instance.gunReload = AudioManager.Instance.rLReload; //set audio when rocket launcher reloads
        }
    }
}
