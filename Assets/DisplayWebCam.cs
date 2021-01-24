using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWebCam : MonoBehaviour
{
    [DllImport("OpenCV_UnityPlugin")]
    private static extern void ProcessImage(ref Color32[] rawImage, int width, int height, ref Matrix4x4 camera_pos_result, ref Vector2 ufo_pos_result, ref Vector2 laser_pos_result);
    
    public Image CameraImage;
    
    private WebCamTexture _webcam;
    private Texture2D _cameraTexture;
    
    void Start()
    {
        _webcam = new WebCamTexture();
        _webcam.Play();
        _cameraTexture = new Texture2D(_webcam.width, _webcam.height);
        CameraImage.material.mainTexture = _cameraTexture;

        CameraImage.rectTransform.sizeDelta = new Vector2(_webcam.width, _webcam.height);
    }

    void Update()
    {
        Matrix4x4 camera_pos_result = new Matrix4x4();
        Vector2 ufo_pos_result = new Vector2(0,0);
        Vector2 laser_pos_result = new Vector2(0,0);
    
        if (_webcam.isPlaying)
        {
            var rawImage = _webcam.GetPixels32();
            ProcessImage(ref rawImage, _webcam.width, _webcam.height, ref camera_pos_result, ref ufo_pos_result, ref laser_pos_result);
            _cameraTexture.SetPixels32(rawImage);
            _cameraTexture.Apply();
        }
        
        print(camera_pos_result);
        print(ufo_pos_result);
        print(laser_pos_result);
    }

}