using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanisterHolder : Interact
{
    public GameObject canisterOnHolder;
    public override void Interaction()
    {
        base.Interaction();
        if (GameManager.Instance.player.canister)
        {
            canisterOnHolder.SetActive(true);
            AudioManager.Instance.placeObject.Play();
        }
        else
        {
            StartCoroutine(RemoveText());
        }

    }
    IEnumerator RemoveText()
    {
        GameManager.Instance.errorText.text = "Pick up canister first";
        yield return new WaitForSeconds(3f);
        GameManager.Instance.errorText.text = "";
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
