using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camera : MonoBehaviour
{
    static WebCamTexture backCam;
    private Texture background;

    public RawImage image;
    private string _SavePath = "E:/snap/";
    int _CaptureCounter = 0;

    void Start()
    {

        background = image.texture;
        if (backCam == null)
            backCam = new WebCamTexture();
            

        

        if (!backCam.isPlaying)
            backCam.Play();
            image.texture=backCam;

            

    }



    void TakeSnapshot()
    {
        Debug.Log("snap method");
        Texture2D snap = new Texture2D(backCam.width, backCam.height);
        snap.SetPixels(backCam.GetPixels());
        snap.Apply();

        System.IO.File.WriteAllBytes(_SavePath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
        ++_CaptureCounter;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {

            TakeSnapshot();
        }
    }
}
