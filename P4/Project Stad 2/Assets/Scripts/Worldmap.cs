using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Worldmap : MonoBehaviour
{
    public Camera worldmapCam;
    public RectTransform worldmapTransform;

    public float scrollSpeed = 2;
    public float minZoom;
    public float maxZoom;

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (worldmapTransform.localScale.x < maxZoom && worldmapTransform.localScale.y < maxZoom)
            {
                worldmapTransform.localScale += new Vector3(scrollSpeed, scrollSpeed, 0);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (worldmapTransform.localScale.x > minZoom && worldmapTransform.localScale.y > minZoom)
            {
                worldmapTransform.localScale -= new Vector3(scrollSpeed, scrollSpeed, 0);
            }
        }
    }
}
