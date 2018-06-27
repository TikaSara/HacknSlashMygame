using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{


    public int startEnemyHealth = 30;
    private int health;
    public int damage = 10;
    public GameObject PickUpHealth;


    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start()
    {
        health = startEnemyHealth;
        healthBar.fillAmount = health;
        
    }

    public void GetHit()
    {
        health -= damage;


        healthBar.fillAmount = (float)health /startEnemyHealth;

        Debug.Log("procent: " + health / startEnemyHealth);

    }

    void Update()
    {

        if (health <= 0)
        {
            Instantiate(PickUpHealth, transform.position, transform.rotation);
            Destroy(this.gameObject);
            Debug.Log("jeej");
        }
    }

}


