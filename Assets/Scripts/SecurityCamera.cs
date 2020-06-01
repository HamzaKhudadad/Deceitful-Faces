using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{

     [SerializeField]
     private Animator anim;
     [SerializeField]
     private GameObject gameOverScene;
     private void OnTriggerEnter(Collider other)
     {
          if(other.tag == "Player")
          {
               
               anim.enabled = false;
               MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
               Color color = new Color(0.6f, 0.1f, 0.1f, 0.3f);
               meshRenderer.material.SetColor("_TintColor", color);
               StartCoroutine(AlertRoutine());
          }
          
     }

     IEnumerator AlertRoutine()
     {
          yield return new WaitForSeconds(0.5f);
          gameOverScene.SetActive(true);
     }
}
