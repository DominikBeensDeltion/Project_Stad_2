using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCar : MonoBehaviour
{
    public bool repaired;
    public bool inCar;
    public bool canGetOut;

    public GameObject player;
    public GameObject carObject;

    public float currentMoveSpeed;
    public float rotateSpeed;

    public float forwardSpeed;
    public float backwardSpeed;

    public float gravity = 1;

    public AudioClip engine;
    public AudioSource sound;

    public Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        //ray for checking if the player can get out of the car
        //Debug.DrawRay(new Vector3(carObject.transform.position.x, carObject.transform.position.y + 0.5f, carObject.transform.position.z), transform.TransformDirection(Vector3.left), Color.red);

        if (inCar)
        {
            if (canGetOut)
            {
                if (Input.GetButtonDown("e"))
                {
                    GetOut();
                }
            }
        }
    }

    void FixedUpdate()
    {
        CarMovement();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("e"))
            {
                if (repaired)
                {
                    inCar = true;
                    sound.PlayOneShot(engine);
                    player.transform.SetParent(carObject.transform);
                    player.transform.position = carObject.transform.position;
                    player.SetActive(false);
                    StartCoroutine("GetOutCoolDown");
                }
            }
        }
    }

    void CarMovement()
    {
        if (inCar)
        {
            //collider ignoring movement
            //transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * currentMoveSpeed * Time.deltaTime);
            //transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime);

            //if (Input.GetAxis("Vertical") * currentMoveSpeed * Time.deltaTime < 0)
            //{
            //    currentMoveSpeed = backwardSpeed;
            //}
            //else if (Input.GetAxis("Vertical") * currentMoveSpeed * Time.deltaTime > 0)
            //{
            //    currentMoveSpeed = forwardSpeed;
            //}

            if (rb.IsSleeping())
            {
                sound.Play();
            }

            //working player-like movement
            float horizontalMovement = (Input.GetAxis("Horizontal") * Time.deltaTime * currentMoveSpeed);
            float verticalMovement = (Input.GetAxis("Vertical") * Time.deltaTime * currentMoveSpeed);
            carObject.transform.position = new Vector3(transform.position.x + horizontalMovement, 0, transform.position.z + verticalMovement);

            rb.velocity = new Vector3(horizontalMovement, -gravity, verticalMovement);

            Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement);

            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            }
        }
    }

    void GetOut()
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(carObject.transform.position.x, carObject.transform.position.y + 0.5f, carObject.transform.position.z), transform.TransformDirection(Vector3.left), out hit, 2))
        {
            //U suck ray
        }
        else
        {
            player.SetActive(true);
            player.transform.position = carObject.transform.position - (transform.right * 2);
            player.transform.SetParent(null);
            inCar = false;
            canGetOut = false;
            sound.Stop();
        }
    }

    IEnumerator GetOutCoolDown()
    {
        yield return new WaitForSeconds(2);
        canGetOut = true;
    }
}