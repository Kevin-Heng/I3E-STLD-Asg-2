using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : Interact
{
    bool screwPickedUp;

    [SerializeField] Transform fpsCam;

    public override void InteractObject()
    {
        screwPickedUp = true;
        base.InteractObject();
        GameManager.Instance.player.UpdateScrew(screwPickedUp);

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
