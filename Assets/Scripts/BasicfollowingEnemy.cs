using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BasicfollowingEnemy : MonoBehaviour {

    private Transform target;
    private Transform myTransform;
    private NavMeshAgent Agent;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update ()
    {
        if(target != null)
        {
            Agent.SetDestination(target.position);
        }
        
        //Vector3 targetPos = target.position;
        //targetPos = Vector3.Scale(targetPos, new Vector3(1, 0, 1));
        //transform.LookAt(targetPos);
        //transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        
	}
}
