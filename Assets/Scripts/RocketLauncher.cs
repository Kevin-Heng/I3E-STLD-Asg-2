using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Gun
{

    // Start is called before the first frame update
    void Start()
    {
        isEquipped = false;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        Reloading();
        OutOfAmmo();

    }
}
