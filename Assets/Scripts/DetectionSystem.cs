using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectionSystem : MonoBehaviour
{
    public Transform target;
    public float range;
    NavMeshAgent agent;
    void Start()
    {
         agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= range){
            agent.destination = target.position;
        }
    }
}
