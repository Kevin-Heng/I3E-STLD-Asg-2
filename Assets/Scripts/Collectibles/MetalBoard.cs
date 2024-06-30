/*
 * Author: Kevin Heng
 * Date: 26/06/2024
 * Description: The MetalBoard class is used to update if metal board have been picked up by player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalBoard : Interact
{
    /// <summary>
    /// boolean to check if metal board has been picked up
    /// </summary>
    bool metalBoardPickedUp;

    /// <summary>
    /// player camera
    /// </summary>
    [SerializeField] Transform fpsCam;

    /// <summary>
    /// Function to pick up metal boards
    /// </summary>
    public override void InteractObject()
    {
        metalBoardPickedUp = true; //metal board picked up
        base.InteractObject();
        GameManager.Instance.player.UpdateMetalBoard(metalBoardPickedUp); //update boolean in player script


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
