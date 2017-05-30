using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worldmap : MonoBehaviour
{
    public Camera worldmapCam;
    public GameObject wm;

    public int scrollSpeed = 2;
    public int minZoom;
    public int maxZoom;

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && (Input.GetAxis("Mouse ScrollWheel") + GetComponent<Camera>().orthographicSize) > minZoom)
        {
            for (int sensitivityOfScrolling = 3; sensitivityOfScrolling > 0; sensitivityOfScrolling--)
            {
                GetComponent<Camera>().orthographicSize -= scrollSpeed;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && (Input.GetAxis("Mouse ScrollWheel") + GetComponent<Camera>().orthographicSize) < maxZoom)
        {
            for (int sensitivityOfScrolling = 3; sensitivityOfScrolling > 0; sensitivityOfScrolling--)
            {
                GetComponent<Camera>().orthographicSize += scrollSpeed;
            }
        }
    }
}
