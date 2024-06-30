/*
 * Author: Kevin Heng
 * Date: 30/06/2024
 * Description: The TotemPlatform class is used for player to place the totem on platform
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemPlatform : Interact
{
    /// <summary>
    /// check if totem was placed
    /// </summary>
    bool totemPlaced;
    /// <summary>
    /// Totem game object
    /// </summary>
    public GameObject totem;

    /// <summary>
    /// function to place totem
    /// </summary>
    public override void PlaceItem()
    {
        if (GameManager.Instance.player.totem)
        {
            base.PlaceItem();
            totem.SetActive(true);
            totemPlaced = true;
            GameManager.Instance.player.UpdateTotemPlaced(totemPlaced);
            
        }
        else
        {
            GameManager.Instance.warningText.text = "You require a totem";
            GameManager.Instance.warningTextBox.SetActive(true);
            StartCoroutine(HideText());
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
