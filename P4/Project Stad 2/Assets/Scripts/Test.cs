using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public GameObject worldmapCam;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(WorldMapRefresh());
        }
    }

    public IEnumerator WorldMapRefresh()
    {
        worldmapCam.SetActive(true);
        yield return new WaitForEndOfFrame();
        worldmapCam.SetActive(false);
    }
}
