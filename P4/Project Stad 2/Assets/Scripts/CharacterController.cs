using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private GameManager gm;
    public Transform player;
    public Rigidbody rb;
    public Animator anim;

    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;
    public float gravity = 1f;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        player = transform;
        rb = player.GetComponent<Rigidbody>();
        anim = player.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float horizontalMovement = (Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
        float verticalMovement = (Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        //oude manier lopen waarbij je door stoepen enz gaat
        //transform.position = new Vector3(transform.position.x + horizontalMovement, 0, transform.position.z + verticalMovement);

        //nieuwe rigidbody movement
        rb.velocity = new Vector3(horizontalMovement, -gravity, verticalMovement);

        Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement);

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        }


        //Animations
        float speed = horizontalMovement + verticalMovement;
        anim.SetFloat("MoveSpeed", speed);

        if (speed != 0)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if (gm.onMission)
        {
            anim.SetBool("HoldingPizza", true);
        }
        else if (!gm.onMission)
        {
            anim.SetBool("HoldingPizza", false);
        }
    }
}
