/*
 * Author: Kevin Heng
 * Date: 13/06/2024
 * Description: The Rifle class, child class of Gun class, is used to handle rifle interactions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{

    // Start is called before the first frame update
    void Start()
    {
        isEquipped = true; //rifle is equipped at the start
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        Reloading();
        OutOfAmmo();
    }
}
