using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitBoard : Interact
{
    bool circuitBoardPickedUp;


    [SerializeField] Transform fpsCam;

    public override void InteractObject()
    {
        circuitBoardPickedUp = true;
        base.InteractObject();
        GameManager.Instance.player.UpdateCircuitBoard(circuitBoardPickedUp);


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
