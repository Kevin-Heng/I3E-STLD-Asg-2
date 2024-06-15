using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public GameObject item;
    public Transform itemParent;
    public bool isEquipped;

    public virtual void EquipItem()
    {
        GameManager.Instance.weaponsList.Add(item);
        item.transform.position = itemParent.transform.position;
        item.transform.rotation = itemParent.transform.rotation;

        item.transform.SetParent(itemParent);
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
