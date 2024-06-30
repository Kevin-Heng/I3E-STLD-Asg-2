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
    bool totem;
    public override void InteractObject()
    {
        base.InteractObject();
        GameManager.Instance.player.UpdateTotem(totem);
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
