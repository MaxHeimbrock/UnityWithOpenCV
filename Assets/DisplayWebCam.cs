using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWebCam : MonoBehaviour
{
    [DllImport("OpenCV_UnityPlugin")]
    private static extern void ProcessImage(ref Color32[] rawImage, int width, int height);
    
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
        if (_webcam.isPlaying)
        {
            var rawImage = _webcam.GetPixels32();
            ProcessImage(ref rawImage, _webcam.width, _webcam.height);
            _cameraTexture.SetPixels32(rawImage);
            _cameraTexture.Apply();
        }
    }

}