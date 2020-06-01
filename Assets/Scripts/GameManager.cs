using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
     public PlayableDirector introCutscene;
     private static GameManager _instance;
     public static GameManager Instance
     {
         
          get
          {
               if (_instance == null)
               {
                    Debug.LogError("GameManager is null!");
               }
               return _instance;
          }
     }
     public bool HasCard { get; set; }
     private void Awake()
     {
          _instance = this;
     }

     public void Update()
     {
          if (Input.GetKeyDown(KeyCode.S))
          {
               introCutscene.time = 61.14f;
               AudioManager.Instance.PlayMusic();
          }
     }
}
