using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     private static MainMenu _instance;
     private static MainMenu Instance
     {
          get
          {
               if(_instance == null)
               {
                    Debug.LogError("Main Menu is null");
               }
               return _instance;
          }
     }

     private void Awake()
     {
          _instance = this;
     }

     public void StartGame()
     {
          SceneManager.LoadScene("Loading_Screen");
     }
     public void Quit()
     {
          Application.Quit();
     }
}
