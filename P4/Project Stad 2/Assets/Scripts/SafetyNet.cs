using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyNet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "PlayerCar")
        {
            col.transform.position = new Vector3(-2.42F, 0.8F, 6.43F);
        }
        else
        {
            Destroy(col.gameObject);
        }
    }
}
