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
    /// Function to change player speed depending on the gun equipped
    /// </summary>
    /// <param name="player"> Reference Player class </param>
    public void ChangeSpeed(Player player)
    {      
        if (isEquipped) //rocket launcher is equipped
        {
            
            float currentSpeed = player.GetComponent<FirstPersonController>().MoveSpeed; //get current player speed
            currentSpeed -= changeSpeed; //change player speed
            player.GetComponent<FirstPersonController>().MoveSpeed = currentSpeed; //set new player speed
        }
        else //rocket launcher is unequipped
        {
            float currentSpeed = player.GetComponent<FirstPersonController>().MoveSpeed; //get currrent player speed
            currentSpeed += changeSpeed; //change player speed
            player.GetComponent<FirstPersonController>().MoveSpeed = currentSpeed; //set new player speed
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        isEquipped = false; //rocket launcher is unequipped at the start
        this.gameObject.SetActive(false); //hide rocket launcher
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        Reloading();
        OutOfAmmo();
    }
}
