using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {

    public int healthAmt = 10;
    public PlayerDamage playerHealth;
	// Use this for initialization
	void Start () {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>();
	}
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            Debug.Log("old health: " + playerHealth.health);
            playerHealth.HealPlayer(healthAmt);
            Debug.Log("new health: " + playerHealth.health);
            Destroy(this.gameObject);
        }
    }
}
