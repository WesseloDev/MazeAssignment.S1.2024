using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBase : MonoBehaviour
{
    [SerializeField] protected float destinationThreshhold;
    protected NavMeshAgent agent;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        if (ReachedDestination())
            agent.isStopped = true;
    }

    protected virtual void Move(Vector3 target)
    {
        agent.destination = target;
    }

    protected virtual bool ReachedDestination()
    {
        return agent.remainingDistance <= destinationThreshhold && agent.velocity.magnitude <= 1f;
    }
}
