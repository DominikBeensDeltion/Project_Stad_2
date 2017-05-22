using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool isTarget;
    public UIManager uim;
    public GameManager gm;
    public GameObject markOne;
    public GameObject markTwo;
    public GameObject cloneOne;
    public GameObject cloneTwo;
    public string naam;

	void Start ()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        uim = GameObject.FindWithTag("UIM").GetComponent<UIManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(gm.timeToCountDown > 0)
            {
                if (isTarget)
                {
                    Debug.Log("Pizza Delivered");
                    uim.ResetHouseText();
                    gm.timerOn = false;
                    gm.timeToCountDown = 180F;
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if (isTarget)
            {
                isTarget = false;
                DeleteMarker();
            }
        }
    }

    public void CreateMarker()
    {
        Vector3 vec =  new Vector3(transform.position.x, transform.position.y, transform.position.z);
        vec.y += 3;
        cloneOne = Instantiate(markOne, vec, Quaternion.identity);
        cloneTwo = Instantiate(markTwo, vec, Quaternion.identity);
    }

    public void DeleteMarker()
    {
        Destroy(cloneOne);
        Destroy(cloneTwo);
        cloneOne = null;
        cloneTwo = null;
    }
}
