using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomHit : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            EnemyController enemyScript = other.gameObject.GetComponent<EnemyController>();
            print("u been hit by broom");
            enemyScript.GetHit();
        }
    }
}
