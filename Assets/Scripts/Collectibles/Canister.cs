/*
 * Author: Kevin Heng
 * Date: 26/06/2024
 * Description: The Canister class is used to update if canister have been picked up by player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canister : Interact
{
    /// <summary>
    /// boolean to check if canister is picked up
    /// </summary>
    bool canisterPickedUp;

    /// <summary>
    /// player camera
    /// </summary>
    [SerializeField] Transform fpsCam;

    /// <summary>
    /// Function to pick up canister
    /// </summary>
    public override void InteractObject()
    {
        canisterPickedUp = true; //canister is picked up
        base.InteractObject();
        GameManager.Instance.player.UpdateCanister(canisterPickedUp); //update boolean in player script
    }
    // Start is called before the first frame update
    void Start()
    {
        if (fpsCam == null) //since projectile game object is not in scene, this is needed to set the fpsCam
        {
            fpsCam = Camera.main.transform; //set player camera
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
