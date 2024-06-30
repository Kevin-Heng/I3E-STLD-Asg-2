/*
 * Author: Kevin Heng
 * Date: 30/06/2024
 * Description: The Totem class is used for player to pick up the totem
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : Interact
{
    /// <summary>
    /// boolean to check if totem is picked up
    /// </summary>
    bool totem;
    /// <summary>
    /// Function to pick up totem
    /// </summary>
    public override void InteractObject()
    {
        totem = true;
        base.InteractObject();
        GameManager.Instance.player.UpdateTotem(totem); //update boolean in player script
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
