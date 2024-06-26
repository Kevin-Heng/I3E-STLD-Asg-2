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

    public float burnInterval;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!GameManager.Instance.burning && GameManager.Instance.playerHp > 0)
            {
                BurnDamage();
                Debug.Log("burn");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!GameManager.Instance.burning && GameManager.Instance.playerHp > 0)
            {
                StartCoroutine(BurnDOT());
            }
        }
    }

    public void BurnDamage()
    {
        if(Time.time >= nextTimeToBurn && GameManager.Instance.playerHp > 0)
        {
            GameManager.Instance.burning = true;
            GameManager.Instance.burningFrame.SetActive(true);
            nextTimeToBurn = Time.time + 1/burnTime;
            GameManager.Instance.ReducePlayerHp(burnDamage, playerHit, playerDie, GameManager.Instance.fpsCam);
            GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString();
            GameManager.Instance.burning = false;
        }
        
        
    }

    IEnumerator BurnDOT()
    {
        float endTime = Time.time + burnDuration;
        GameManager.Instance.burning = true;
        while (Time.time < endTime && GameManager.Instance.playerHp > 0)
        {
            GameManager.Instance.burningFrame.SetActive(true);
            GameManager.Instance.ReducePlayerHp(burnDamage, playerHit, playerDie, GameManager.Instance.fpsCam);
            GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString();
            yield return new WaitForSeconds(burnInterval);
        }
        GameManager.Instance.burning = false;
        GameManager.Instance.burningFrame.SetActive(false);
    }
    


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.burningFrame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
