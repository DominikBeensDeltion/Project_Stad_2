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

    public float moveSpeed;
    public float rotateSpeed;

    public AudioClip engine;
    public AudioSource sound;

    public Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime);
            if (rb.IsSleeping())
            {
                sound.Play();
            }
            //float horizontalMovement = (Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
            //float verticalMovement = (Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
            //carObject.transform.position = new Vector3(transform.position.x + horizontalMovement, 0, transform.position.z + verticalMovement);

            //Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement);

            //if (direction != Vector3.zero)
            //{
            //    Quaternion rotation = Quaternion.LookRotation(direction);
            //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            //}
        }
    }

    void GetOut()
    {
        RaycastHit hit;
        if (Physics.Raycast(carObject.transform.position, Vector3.left, out hit, 2))
        {
            //U suck ray
        }

        else
        {
            player.SetActive(true);
            inCar = false;
            player.transform.SetParent(null);
            player.transform.position = new Vector3(carObject.transform.position.x - 3, carObject.transform.position.y, carObject.transform.position.z);
            canGetOut = false;
            sound.Stop();

        }
        Debug.DrawRay(carObject.transform.position, Vector3.left, Color.red);
    }

    IEnumerator GetOutCoolDown()
    {
        yield return new WaitForSeconds(2);
        canGetOut = true;
    }
}