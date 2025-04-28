using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Nemici
{
    public int detectionRange=100; 
    public int attackRangeBoss;
    protected override void Update()
    {
       if (CanSeePlayer())
       {
        ChasePlayer();
       }
    }
    private bool CanSeePlayer()
    {
          float distanceToPlayer = Vector3.Distance(transform.position, player.position);
          if (distanceToPlayer <= detectionRange)
          {
                Vector3 directionToPlayer = player.position - transform.position;
                if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange))
                {
                      if (hit.transform.CompareTag("Player"))
                      {
                            return true; 
                      }
                }
          }
          return false; 
    }

     private void ChasePlayer()
    {
         if (Vector3.Distance(transform.position, player.position) <= attackRangeBoss)
         {
             AttackPlayer();
         }
         agent.SetDestination(player.position);
         agent.speed= 80f;
    }
}
