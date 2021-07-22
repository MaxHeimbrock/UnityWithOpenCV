using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntrinsicSetup : MonoBehaviour {
 
    Camera mainCamera;
 
    public float f = 35.0f; // f can be arbitrary, as long as sensor_size is resized to to make ax,ay consistient
 
    // Use this for initialization
    void Start () {
        mainCamera = gameObject.GetComponent<Camera>();
        changeCameraParam();
    }
 
    public void changeCameraParam()
    {
        float ax, ay, sizeX, sizeY;
        float x0, y0, shiftX, shiftY;
        int width, height;
 
        ax = 980.27331648970778f;
        ay = 651.87996496916037f;
        x0 = 973.92308134349241f;
        y0 = 337.92989084461959f;
 
        width = 1280;
        height = 720;
 
        sizeX = f * width / ax;
        sizeY = f * height / ay;
 
        shiftX = -(x0 - width / 2.0f) / width;
        shiftY = (y0 - height / 2.0f) / height;
 
        mainCamera.sensorSize = new Vector2(sizeX, sizeY);     // in mm, mx = 1000/x, my = 1000/y
        mainCamera.focalLength = f;                            // in mm, ax = f * mx, ay = f * my
        mainCamera.lensShift = new Vector2(shiftX, shiftY);    // W/2,H/w for (0,0), 1.0 shift in full W/H in image plane
 
    }
}
