using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;
    public Transform player;

    private void Start()
    {
        player = transform;
    }

    private void FixedUpdate()
    {
        float horizontalMovement = (Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
        float verticalMovement = (Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        transform.position = new Vector3(transform.position.x + horizontalMovement, 0, transform.position.z + verticalMovement);

        Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement);

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        }
    }
}
