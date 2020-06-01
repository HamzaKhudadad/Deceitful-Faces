using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverTrigger : MonoBehaviour
{
     [SerializeField]
     private AudioClip clipToPlay;
 
   
     private void OnTriggerEnter(Collider other)
     {
          if(other.tag == "Player")
          {
               
               AudioManager.Instance.PlayVoiceOver(clipToPlay);
          }
     }
}
