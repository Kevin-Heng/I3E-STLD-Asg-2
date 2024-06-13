using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    int damage = 10;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit");
            Destroy(projectile);
            GameManager.Instance.ReducePlayerHp(damage);
            Debug.Log(GameManager.Instance.playerHp);

        }
        else
        {
            Destroy(projectile, 1.25f);
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
