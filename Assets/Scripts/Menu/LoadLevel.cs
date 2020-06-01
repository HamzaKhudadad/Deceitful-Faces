using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
     public Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
          StartCoroutine(LoadLevelAsync());
    }

   IEnumerator LoadLevelAsync()
     {
          AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");
          while (!asyncLoad.isDone)
          {
               progressBar.fillAmount = asyncLoad.progress;
               yield return new WaitForEndOfFrame();
          }
         
     }
}
