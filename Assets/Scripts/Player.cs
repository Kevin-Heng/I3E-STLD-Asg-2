/*
 * Author: Kevin Heng
 * Date: 10/06/2024
 * Description: The Player class is used to handle player interactions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Reference Gun parent class
    /// </summary>
    Gun currentGun;

    Interact currentInteractable;
    [SerializeField] Transform fpsCam;



    /// <summary>
    /// Function to update which gun is currently equipped
    /// </summary>
    /// <param name="gun"> To input weapons with Gun class </param>
    public void UpdateGun(Gun gun)
    {
       currentGun = gun;
    }

    /// <summary>
    /// Player input to reload gun (R)
    /// </summary>
    void OnReload()
    {
        Debug.Log("reload");
        currentGun.Reloading();
    }

    void OnInteract()
    {
        if(currentInteractable != null)
        {
            currentInteractable.InteractObject();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hitInfo))
        {
            if (hitInfo.transform.TryGetComponent<Interact>(out currentInteractable))
            {
                Debug.Log(hitInfo.transform.name);
            }
        }

    }
}
