using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cam : MonoBehaviour
{
    static WebCamTexture backCam;
    private Texture background;

    private RawImage image;
    private string _SavePath = "D:/snap/";
    int _CaptureCounter = 0;

    public RawImage Image { get => image; set => image = value; }

    void Start()
    {

    
        if (backCam == null)
            backCam = new WebCamTexture();
            

        

        if (!backCam.isPlaying)
            backCam.Play();
            

            

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
            //capture
            TakeSnapshot();
        }
    }
}
