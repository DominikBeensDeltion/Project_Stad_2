using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    public float moveSpeed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetButton("w"))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetButton("a"))
        {
            transform.Rotate(transform.up *- 300 * Time.deltaTime);
        }

        if (Input.GetButton("s"))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Input.GetButton("d"))
        {
            transform.Rotate(transform.up * 300 * Time.deltaTime);
        }
    }
}
