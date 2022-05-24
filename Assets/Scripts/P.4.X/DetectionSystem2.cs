using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//O axente persegue se o target está dentro dunha distancia determinada en 360 grados e ten liña de visión P.4.1.2
public class DetectionSystem2 : MonoBehaviour
{
    public Transform target;
    public float range;
    public Transform home;
    public float radiusDetection;
    NavMeshAgent agent;
    
    void Start()
    {
         agent = GetComponent<NavMeshAgent>();
         home = GameObject.FindGameObjectWithTag("Enemy Home").transform;
         target = GameObject.FindGameObjectWithTag("Player").transform;
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 targetDirection = (target.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(Physics.Raycast(transform.position + Vector3.up, targetDirection, out hit, radiusDetection, -1)){
            //enemigo quieto por perdida de visibilidad del player.
            if(hit.transform.tag == "Obstacle"){
                agent.destination = transform.position;
            } else{
                CanFollowPlayer();
            }
        }
    }
    public bool CanFollowPlayer(){
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(distanceToTarget < radiusDetection){
            agent.destination = target.position;
            return true;
        } else{
            agent.destination = home.position;
            return false;
        }
    }
}
