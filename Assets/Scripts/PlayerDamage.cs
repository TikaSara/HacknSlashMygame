using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour {

    [SerializeField]
    private int maxHealth = 30;
    int damage = 10;
    public int health;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = health;

        // Check if the player should be alive to begin with
        if (health <= 0)
        {
            StartCoroutine(GetComponent<CharacterController>().KillPlayer());
        }
    }

    // If an enemy hits the player, deal damage to the player
    void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.tag == "enemy")
        {
            TakeDamage(damage);
        }    
    }

    public void TakeDamage(int damage)
    {
        // Handle everything that happens when the player gets hit
        health -= damage;
        GetComponent<AudioSource>().Play();
        UpdateHealthBar();

        if (health <= 0)
        {
            StartCoroutine(GetComponent<CharacterController>().KillPlayer());
        }
    }
    public void HealPlayer(int amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)health / maxHealth;
    }

    public bool HasMaxHealth()
    {
        return health == maxHealth;
    }
}
