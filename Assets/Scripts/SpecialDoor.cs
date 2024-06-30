/*
 * Author: Kevin Heng
 * Date: 30/06/2024
 * Description: The SpecialDoor class is used to lock the door to the volcano unless totem is placed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDoor : Interact
{
    /// <summary>
    /// function to check if player has totem or has placed down totem, then change scene
    /// </summary>
    public override void ChangeScene()
    {
        if (!GameManager.Instance.player.totem || !GameManager.Instance.player.totemPlaced)
        {
            GameManager.Instance.warningTextBox.SetActive(true);
            GameManager.Instance.warningText.text = "Place a totem on the blue platform to unlock the door";
            StartCoroutine(HideText());
        }
        else
        {
            base.ChangeScene();
        }
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
