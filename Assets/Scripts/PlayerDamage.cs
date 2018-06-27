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
        print(maxHealth);
        health = maxHealth;
        healthBar.fillAmount = health;
    }

    void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.tag == "enemy")
        {
            TakeDamage(damage);
            print("Enemy just touched player!" + maxHealth);
        }    
    }

    void Update()
    {

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthBar();
    }
    public void HealPlayer(int amount)
    {
        TakeDamage(-amount);
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)health / maxHealth;
    }
}
