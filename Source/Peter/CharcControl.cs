using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcControl : MonoBehaviour {
    public float moveSpeed;
    public float mousesensitivity = 5.0F;
    public float updownrange = 60.0F;
    float verticalRotation = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MouseLook();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
    }

    void MouseLook()
    {
        float rotLeftRight = Input.GetAxis("Mouse X") * mousesensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mousesensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -updownrange, updownrange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
