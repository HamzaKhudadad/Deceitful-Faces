using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class backgroundvideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
   

  
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo() {
        videoPlayer.Prepare();
        WaitForSeconds wait = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            yield return wait;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        
    }
}
