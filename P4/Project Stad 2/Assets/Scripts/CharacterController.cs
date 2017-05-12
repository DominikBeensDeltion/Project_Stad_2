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

        if (Input.GetButtonDown("a"))
        {
            transform.Rotate(0, -90, 0);
        }

        if (Input.GetButton("s"))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("d"))
        {
            transform.Rotate(0, +90, 0);
        }
    }
}
