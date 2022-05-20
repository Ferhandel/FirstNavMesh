using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectionSystem2 : MonoBehaviour
{
    public Transform target;
    public float range;
    public float angleDetection;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //distancia entre 2 puntos
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        //calculamos ángulos
        Vector3 targetDir = target.position - transform.position;
        //no se que leches es esto:
        angleDetection = Vector3.Angle(targetDir, transform.forward);
        if (distanceToTarget <= range){
            agent.destination = target.position;
        }

    }
}
