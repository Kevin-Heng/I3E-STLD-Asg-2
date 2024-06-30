/*
 * Author: Kevin Heng
 * Date: 26/06/2024
 * Description: The Screw class is used to update if screws have been picked up by player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : Interact
{
    /// <summary>
    /// boolean to check if screw is picked up
    /// </summary>
    bool screwPickedUp;

    /// <summary>
    /// player camera
    /// </summary>
    [SerializeField] Transform fpsCam;

    /// <summary>
    /// Function to pick up screws
    /// </summary>
    public override void InteractObject()
    {
        screwPickedUp = true; //screw picked up
        base.InteractObject();
        GameManager.Instance.player.UpdateScrew(screwPickedUp); //update boolean in player script

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
