using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class DisplayWebcam2 : MonoBehaviour
{
    [DllImport("OpenCV_Unity_Lib")]
    private static extern void InitCap(int[] resolution);
    
    [DllImport("OpenCV_Unity_Lib")]
    private static extern byte[] GetFrameUnity(float[] result);
    
    [DllImport("OpenCV_Unity_Lib")]
    private static extern void GetPlaneUnity(float[] plane);
    
    private Texture2D tex;
    private int width;
    private int height;

    private float[] results;

    public GameObject plane;
    void Start()
    {
        int[] resolution = new int[2];
        results = new float[11];

        InitCap(resolution);
        width = resolution[0];
        height = resolution[1];
        Debug.Log("Resolution: " + width + " x " + height);
        Debug.Log("done");
        tex = new Texture2D(width, height, TextureFormat.BGRA32, false);
    }

    void Update() 
    {
        // bottlecap(2) + forwardRot(3) + upRot(3) + pos(3)
        byte[] imgData = GetFrameUnity(results);
        //GetFrameUnity(bottlecap);
        
        //Debug.Log("Bottlecap: " + results[0] + "/" + results[1]);
        //Debug.Log("translate: (" + results[8] + ", " + results[9] + ", " + results[10] + ")");

        Vector3 f = new Vector3(results[2], results[3], results[4]);
        f /= 1000;
        Vector3 u = new Vector3(results[5], results[6], results[7]);
        u /= 1000;
        Vector3 pos = new Vector3(results[8], results[9], results[10]);
        pos /= 1000;
        
        // On StackOverflow, the y axis in pos and rot input is inversed to go from left to right handed coordinates
        Quaternion rot = Quaternion.LookRotation(new Vector3(f.x, f.y, f.z), new Vector3(u.x, u.y, u.z));
        pos = new Vector3(pos.x, pos.y, pos.z);

        plane.transform.position = pos;
        plane.transform.rotation = rot * Quaternion.Euler(90,0,0);
        
        tex.LoadRawTextureData(imgData);
        tex.Apply();
        this.GetComponent<RawImage>().texture = tex;
    }
}
