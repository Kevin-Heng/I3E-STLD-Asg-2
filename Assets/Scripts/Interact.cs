/*
 * Author: Kevin Heng
 * Date: 19/06/2024
 * Description: The Interact class is used to handle player interaction with interactable
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    public virtual void InteractObject()
    {
        Destroy(gameObject);
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
