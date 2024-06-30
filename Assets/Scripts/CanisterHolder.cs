/*
 * Author: Kevin Heng
 * Date: 26/06/2024
 * Description: The CanisterHolder class is used for player to place the canister on holder
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanisterHolder : Interact
{
    /// <summary>
    /// check if canister is placed on holder
    /// </summary>
    bool canisterPlaced;
    /// <summary>
    /// empty canister on holder game object
    /// </summary>
    public GameObject canisterOnHolder;
    /// <summary>
    /// full canister game object
    /// </summary>
    public GameObject fullCanister;
    public override void PlaceItem()
    {
        if (GameManager.Instance.player.canister)
        {
            base.PlaceItem();
            canisterPlaced = true;
            GameManager.Instance.player.UpdateCanisterPlaced(canisterPlaced);
            canisterOnHolder.SetActive(true);
            StartCoroutine(WaitForCharge());
        }
        else
        {
            GameManager.Instance.warningTextBox.SetActive(true);
            GameManager.Instance.warningText.text = "Place down an empty energy canister";
            StartCoroutine(HideText());
        }
    }

    IEnumerator WaitForCharge()
    {
        GameManager.Instance.warningTextBox.SetActive(true);
        GameManager.Instance.warningText.text = "Charging...";
        yield return new WaitForSeconds(15);
        GameManager.Instance.player.canister = false;
        fullCanister.SetActive(true);
        StartCoroutine(HideText());
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
