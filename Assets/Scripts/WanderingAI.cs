using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting.APIUpdating;

public class WanderingAI : AIBase
{
    [SerializeField] private GameObject[] wanderPoints = new GameObject[1];
    private GameObject lastWanderPoint;
    [SerializeField]  private float cooldown;
    [SerializeField]  private bool cooldownActive = false;

    [SerializeField] private float minCooldown, maxCooldown;

    protected override void Start()
    {
        base.Start();
        PickWanderPoint();
    }

    protected override void Update()
    {
        base.Update();

        if (cooldownActive)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                PickWanderPoint();
                cooldownActive = false;
            }
        }
        else if (!cooldownActive && ReachedDestination())
        {
            cooldownActive = true;
            cooldown = Random.Range(minCooldown, maxCooldown);
            Debug.Log("Cooldown before moving: " + cooldown);
        }
    }

    void PickWanderPoint()
    {
        GameObject wanderPoint = wanderPoints[Random.Range(0, wanderPoints.Length)];

        if (wanderPoint == lastWanderPoint)
        {
            PickWanderPoint();
            return;
        }

        base.Move(wanderPoint.transform.position);
        agent.isStopped = false;
        lastWanderPoint = wanderPoint;
        WanderDebugVisuals(wanderPoint);
    }

    void WanderDebugVisuals(GameObject targetedPoint)
    {
        for (int i = 0; i < wanderPoints.Length; i++)
        {
            if (wanderPoints[i] == targetedPoint)
            {
                wanderPoints[i].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            else
            {
                wanderPoints[i].gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }

}
