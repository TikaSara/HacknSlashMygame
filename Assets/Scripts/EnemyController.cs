using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    public int startEnemyHealth = 10;
    private int health;
    public int damage = 10;
    public GameObject PickUpHealth;
    public AudioClip hitSound;


    private void Start()
    {
        health = startEnemyHealth;

        // Check if the enemy even HAS health
        if (health <= 0)
        {
            Instantiate(PickUpHealth, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void GetHit()
    {
        // Handle everything that happens when an enemy gets hit
        health -= damage;
        AudioSource.PlayClipAtPoint(hitSound, transform.position, 1f);
        
        if (health <= 0)
        {
            Instantiate(PickUpHealth, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

}


