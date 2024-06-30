/*
 * Author: Kevin Heng
 * Date: 26/06/2024
 * Description: The CircuitBoard class is used to update if circuit board have been picked up by player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBoard : Interact
{
    /// <summary>
    /// boolean to check if circuit board is picked up
    /// </summary>
    bool circuitBoardPickedUp;

    /// <summary>
    /// player camera
    /// </summary>
    [SerializeField] Transform fpsCam;

    /// <summary>
    /// Function to pick up circuit board
    /// </summary>
    public override void InteractObject()
    {
        circuitBoardPickedUp = true; //circuit board is picked up
        base.InteractObject();
        GameManager.Instance.player.UpdateCircuitBoard(circuitBoardPickedUp); //update boolean in player script


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
