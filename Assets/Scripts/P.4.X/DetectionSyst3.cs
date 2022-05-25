using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectionSyst3 : MonoBehaviour
{
    public Transform target;
    public Transform home;
    //public float range;
    public float angleDetection;
    public float angleVision;
    NavMeshAgent agent;
    AgentState state;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        home  = GameObject.FindGameObjectWithTag("Home").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        state = AgentState.Idle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 leftRay = transform.forward * -30;
        Vector3 rightRay = transform.forward * 30;
        Debug.DrawLine(transform.position, target.transform.position, Color.green, 1f);
        Debug.DrawLine(transform.position, transform.forward * 10, Color.red, 1f);
        Debug.DrawLine(transform.position, transform.forward * 10, Color.blue, 1f);
        Debug.DrawLine(transform.position, transform.forward * 10, Color.yellow, 1f);
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(Physics.Raycast(transform.position, target.transform.position - transform.position, out hit, angleDetection)){
            CanFollow();
        }
        switch (state)
        {
            case AgentState.Idle:
                Debug.Log(state);
                if(CanFollow() == true)
                    SetState(AgentState.Chasing);
                break;
            case AgentState.Chasing:
                Debug.Log(state);
                    if(CanFollow() == true)
                agent.destination = target.position;
                    else
                SetState(AgentState.Returning);
                break;
            case AgentState.Returning:
                Debug.Log(state);
                if(CanFollow() == true)
                    SetState(AgentState.Chasing);
                else if (agent.destination == home.position)
                    SetState(AgentState.Idle);
                break;     
        }
        
    }
    public void SetState(AgentState newState){
        if(newState != state){
            state = newState;
            switch(newState){
                case AgentState.Idle:
                break;
                case AgentState.Chasing:
                break;
                case AgentState.Returning:
                break;
            }
        }
    }
    public bool CanFollow(){
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(distanceToTarget < angleDetection && CanSee() == true){
            agent.destination = target.position;
            return true;
        } else{
            agent.destination = home.position;
            return false;
        }
    }
    public bool CanSee(){
        Vector3 targetDirection = target.transform.position - transform.position;
        angleVision = Vector3.Angle(transform.forward, targetDirection);
        if(angleVision > -40 && angleVision < 40){
            return true;
        } else if((hit.transform.tag == "Obstaculo")){
            Debug.Log("NO VISION");
            agent.destination = home.position;
        }
        return false;
    }
    public enum AgentState{
        Idle, //Parado
        Chasing, //Siguiendo al Target "Player"
        Returning, //volviendo a "home"
    }
}