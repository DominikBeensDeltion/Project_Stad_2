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
        Debug.Log("Collided");
        Vector3 up = col.transform.position;
        up.y = col.transform.position.y + 39;
        col.transform.position = up;
    }
}
