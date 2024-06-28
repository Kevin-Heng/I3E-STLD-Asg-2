using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canister : Interact
{
    bool canisterPickedUp;

    public AudioClip pickUp;
    [SerializeField] Transform fpsCam;

    public override void InteractObject()
    {
        canisterPickedUp = true;
        base.InteractObject();
        GameManager.Instance.player.UpdateCanister(canisterPickedUp);

        AudioSource.PlayClipAtPoint(pickUp, fpsCam.position, 0.7f);

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
