using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabKeyCardActivation : MonoBehaviour
{
     [SerializeField]
     private GameObject sleepingGuardScene;
     private void OnTriggerEnter(Collider other)
     {
          if(other.tag == "Player")
          {
               sleepingGuardScene.SetActive(true);
               GameManager.Instance.HasCard = true;
          }
     }
}
