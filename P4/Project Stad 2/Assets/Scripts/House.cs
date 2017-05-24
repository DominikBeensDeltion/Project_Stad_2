using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    public UIManager uim;
    public GameManager gm;

    public bool isTarget;
    public GameObject markOne;
    public GameObject cloneOne;
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

                    DeleteMarker();
                    isTarget = false;
                }
            }
        }
    }

    public void CreateMarker()
    {
        Vector3 vec = new Vector3(transform.position.x, 20, transform.position.z);
        vec.y += 3;
        cloneOne = Instantiate(markOne, vec, Quaternion.identity);
    }

    public void DeleteMarker()
    {
        Destroy(cloneOne);
        cloneOne = null;
    }
}
