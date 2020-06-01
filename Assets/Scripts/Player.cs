using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
     private NavMeshAgent navMeshAgent;
     private Animator animator;
     private Vector3 target;
     [SerializeField]
     private GameObject coinPrefab;
     public AudioClip coinSoundEffect;
     private bool coinTossed;
     // Start is called before the first frame update
     void Start()
    {
          navMeshAgent = GetComponent<NavMeshAgent>();
          animator = GetComponentInChildren<Animator>();
     }

    // Update is called once per frame
    void Update()
    {
          
          if (Input.GetMouseButtonDown(0))
          {
               Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
               RaycastHit hitInfo;
               if (Physics.Raycast(rayOrigin, out hitInfo))
               {
                    navMeshAgent.SetDestination(hitInfo.point);
                    animator.SetBool("walk", true);
                    target = hitInfo.point;
               }

          }
          float distance = Vector3.Distance(transform.position, target);
          if(distance < 1.0f)
          {
               animator.SetBool("walk", false);
          }

          if (Input.GetMouseButtonDown(1) && coinTossed == false)
          {
               Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
               RaycastHit hitInfo;

               if (Physics.Raycast(rayOrigin, out hitInfo)) {
                    Instantiate(coinPrefab, hitInfo.point, Quaternion.identity);
                    animator.SetTrigger("Throw");
                    AudioSource.PlayClipAtPoint(coinSoundEffect, Camera.main.transform.position);
                    coinTossed = true;
                    SendAIToCoinSpot(hitInfo.point);
               }
               

          }
          
     }

     void SendAIToCoinSpot(Vector3 coinPos)
     {
          GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard1");
          foreach(var guard in guards)
          {
               NavMeshAgent currentAgent = guard.GetComponent<NavMeshAgent>();
               GuardAI currentGuard = guard.GetComponent<GuardAI>();
               Animator currentAnim = guard.GetComponent<Animator>();

               currentGuard.coinTossed = true;
               currentAgent.SetDestination(coinPos);
               currentAnim.SetBool("walk", true);
               currentGuard.coinPos = coinPos;
               
          }
     }
}
