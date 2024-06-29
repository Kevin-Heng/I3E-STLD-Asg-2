using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalBoard : Interact
{
    bool metalBoardPickedUp;

    [SerializeField] Transform fpsCam;

    public override void InteractObject()
    {
        metalBoardPickedUp = true;
        base.InteractObject();
        GameManager.Instance.player.UpdateMetalBoard(metalBoardPickedUp);


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
