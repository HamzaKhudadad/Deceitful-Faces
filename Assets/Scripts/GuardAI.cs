using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
     public List<Transform> wayPoints;
     public NavMeshAgent agent;
     [SerializeField]
     private int currentTarget;
     private bool reverse;
     private bool targetReached;
     private Animator animator;
     public bool coinTossed;
     public Vector3 coinPos;
     // Start is called before the first frame update
     void Start()
     {
          agent = GetComponent<NavMeshAgent>();
          animator = GetComponent<Animator>();

     }

     // Update is called once per frame
     void Update()
     {

          if (wayPoints.Count > 0 && wayPoints[currentTarget] != null && coinTossed == false)
          {

               agent.SetDestination(wayPoints[currentTarget].position);
               
               float distance = Vector3.Distance(transform.position, wayPoints[currentTarget].position);

               if(distance < 1.0f && (currentTarget == 0 || currentTarget == wayPoints.Count - 1))
               {
                    if(animator != null) {
                         animator.SetBool("Walk", false);
                    }
                    
               }
               else
               {
                    if (animator != null)
                    {
                         animator.SetBool("Walk", true);
                    }
                   
               }

               if(distance < 1.0f && targetReached == false)
               {
                    if (wayPoints.Count < 2)
                    {
                         return;
                    }
                    if ((currentTarget == 0 || currentTarget == wayPoints.Count - 1) && wayPoints.Count > 1) {
                         targetReached = true;
                         StartCoroutine(WaitBeforeMoving());
                    }
                    else
                    {

                         if (reverse) {
                              currentTarget--;
                              if(currentTarget<= 0)
                              {
                                   reverse = false;
                                   currentTarget = 0;
                              }
                         }
                         else
                         {
                              currentTarget++;
                         }
                    }
                    
                    
               }
          }

          else
          {
               float distance = Vector3.Distance(transform.position, coinPos);
               if(distance < 4f)
               {
                    animator.SetBool("Walk", false);
               }
          }

         

     }

     IEnumerator WaitBeforeMoving()
     {
          if(currentTarget == 0 || currentTarget == wayPoints.Count - 1)
          {
               yield return new WaitForSeconds(2.0f);
          }

          if (reverse)
          {
               currentTarget--;
               if (currentTarget == 0)
               {
                    reverse = false;
                    currentTarget = 0;
               }
          }
          else
          {
               currentTarget++;
               if (currentTarget == wayPoints.Count)
               {
                    reverse = true;
                    currentTarget--;
               }
          }

       
          targetReached = false;
     }


}

