using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstruccionMask;
    public bool canSeePlayer;
    public Transform target;
    NavMeshAgent enemy;
    private void Start(){
        enemy = GetComponent<NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());        
    }

    void Update()
    {
        enemy.destination = target.position;
    }
    private IEnumerator FOVRoutine(){
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while(true){
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck(){
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if(rangeChecks.Length != 0){
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2){
                float distanceToTarge = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarge, obstruccionMask)){
                    canSeePlayer = true;
                }else{
                    canSeePlayer = false;
                }
            } else{
                canSeePlayer = false;
            } 
        } else if (canSeePlayer){
            canSeePlayer = false;
        }
    }
}
