using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectionSyst2 : MonoBehaviour
{
    public Transform target;
    public Transform home;
    public float range;
    public float angleDetection;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        home  = GameObject.FindGameObjectWithTag("Home").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 targetDirection = (target.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(Physics.Raycast(transform.position + Vector3.up, targetDirection, out hit, angleDetection, -1)){
            if(hit.transform.tag == "Obstaculo"){
                agent.destination = transform.position;
            } else{
                CanFollow();
            }
        }
    }
    public bool CanFollow(){
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(distanceToTarget < angleDetection){
            agent.destination = target.position;
            return true;
        } else{
            agent.destination = home.position;
            return false;
            
        }
    }
}
