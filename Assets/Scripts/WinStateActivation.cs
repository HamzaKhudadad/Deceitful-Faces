using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateActivation : MonoBehaviour
{
     [SerializeField]
     private GameObject _winCutscene;
     private void OnTriggerEnter(Collider other)
     {
          if(other.tag == "Player")
          {
               if (GameManager.Instance.HasCard)
               {
                    _winCutscene.SetActive(true);
               }
               else
               {
                    Debug.Log("You must have a key");
               }
          }
     }
}
