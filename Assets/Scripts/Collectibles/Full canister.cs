/*
 * Author: Kevin Heng
 * Date: 30/06/2024
 * Description: The FullCanister class is used to update if full canister have been picked up by player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullcanister : Interact
{
    /// <summary>
    /// check if full canister is picked up
    /// </summary>
    bool fullCanister;
    /// <summary>
    /// function to pick up full canister
    /// </summary>
    public override void InteractObject()
    {
        base.InteractObject();
        fullCanister = true;
        GameManager.Instance.player.FullCanister(fullCanister);
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
