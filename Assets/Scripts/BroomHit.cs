using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomHit : MonoBehaviour
{
    // If the broom hits an enemy, damage the enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<EnemyController>().GetHit();
            print("u been hit by broom");
        }
    }
}
