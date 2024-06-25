using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    /// <summary>
    /// Projectile damage if it hits player
    /// </summary>
    public int burnDamage;
    /// <summary>
    /// Audio played when enemy is hit by projectile
    /// </summary>
    public AudioClip playerHit;
    /// <summary>
    /// Audio played when player dies
    /// </summary>
    public AudioClip playerDie;


    public float burnTime;
    public static float nextTimeToBurn = 0;

    public float burnDuration;
    public bool burning;
    public float burnInterval;

    public GameObject burningFrame;


    private void OnTriggerStay(Collider other)
    {
        BurnDamage();
    }

    private void OnTriggerExit(Collider other)
    {
        burning = false;
        if (other.CompareTag("Player"))
        {
            if (!burning)
            {
                StartCoroutine(BurnDOT());
            }

        }
    }

    public void BurnDamage()
    {
        if(Time.time >= nextTimeToBurn && GameManager.Instance.playerHp > 0)
        {
            burning = true;
            burningFrame.SetActive(true);
            nextTimeToBurn = Time.time + 1/burnTime;
            GameManager.Instance.ReducePlayerHp(burnDamage, playerHit, playerDie, GameManager.Instance.fpsCam);
            GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString();
            burning = false;
        }
        
        
    }

    IEnumerator BurnDOT()
    {
        burning = true;
        float endTime = Time.time + burnDuration;
        while (Time.time < endTime && GameManager.Instance.playerHp > 0)
        {
            burningFrame.SetActive(true);
            GameManager.Instance.ReducePlayerHp(burnDamage, playerHit, playerDie, GameManager.Instance.fpsCam);
            GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString();
            yield return new WaitForSeconds(burnInterval);
        }
        burning = false;
        burningFrame.SetActive(false);
    }
    


    // Start is called before the first frame update
    void Start()
    {
        burningFrame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
