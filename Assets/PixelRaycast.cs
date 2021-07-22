using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelRaycast : MonoBehaviour
{
    public Transform marker;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            marker.position = hit.point;
        }
    }
}
