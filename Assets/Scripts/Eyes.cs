using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
     public GameObject gameOverCutscene;

     private void OnTriggerEnter(Collider other)
     {
          if(other.tag == "Player") 
          {
               gameOverCutscene.SetActive(true);
          }
     }
}
