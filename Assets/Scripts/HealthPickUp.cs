using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {

    public int healthAmt = 10;
    public PlayerDamage playerHealth;

	void Start () {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerDamage>();
	}
	
    // If the player walks into a HealthPickUp, heal the player and destroy the HealthPickUp.
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player" && !other.GetComponent<PlayerDamage>().HasMaxHealth())
        {
            Debug.Log("old health: " + playerHealth.health);
            playerHealth.HealPlayer(healthAmt);
            Debug.Log("new health: " + playerHealth.health);
            Destroy(this.gameObject);
        }
    }
}
