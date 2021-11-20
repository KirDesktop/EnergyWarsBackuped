using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkingController : HealthScript
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyDetector _detector;

    private void Update()
    {
        if (health <= 0)
        {
            _destroyThis();
        }

        if (_detector.isPlayer)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_detector.player.transform.position);
        }
        else
        {
            _agent.isStopped = true;
        }
    } 
}
