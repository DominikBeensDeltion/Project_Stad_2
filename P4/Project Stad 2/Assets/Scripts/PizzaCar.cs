using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PizzaCar : MonoBehaviour
{
    public bool repaired;
    public bool inCar;
    public bool canGetOut;

    public GameObject player;
    public GameObject carObject;
    public MeshRenderer playerMapMark;

    public float currentMoveSpeed;
    public float rotateSpeed;

    public float forwardSpeed;
    public float backwardSpeed;

    public float gravity = 1;

    public AudioClip engine;
    public AudioSource sound;

    public Rigidbody rb;

    private FollowPlayer mainCamFollowScript;
    public GameObject mainCam;
    public GameObject minimapCam;

    public ParticleSystem brokenParticle;
    public ParticleSystem carEngineParticle;

    public float durability = 100F;
    public float houseDam = 5F;
    public float otherDam = 2.5F;
    public bool collided;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        mainCamFollowScript = mainCam.GetComponent<FollowPlayer>();
        //carEngineParticle.Stop();
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
                    mainCamFollowScript.offset.y += 10;
                    mainCamFollowScript.offset.z -= 4;
                    minimapCam.GetComponent<Camera>().orthographicSize += 5;
                    player.SetActive(false);
                    playerMapMark.enabled = false;
                    //carEngineParticle.Play();
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
        if (Physics.Raycast(new Vector3(carObject.transform.position.x, carObject.transform.position.y + 0.5f, carObject.transform.position.z), transform.TransformDirection(Vector3.left), out hit, 1.5F))
        {
            //U suck ray
        }
        else
        {
            player.SetActive(true);
            playerMapMark.enabled = true;
            mainCamFollowScript.offset = mainCamFollowScript.startOffset;
            minimapCam.GetComponent<Camera>().orthographicSize -= 5;
            player.transform.position = carObject.transform.position - (transform.right * 2);
            player.transform.SetParent(null);
            inCar = false;
            canGetOut = false;
            //carEngineParticle.Stop();
            sound.Stop();
        }
    }

    IEnumerator GetOutCoolDown()
    {
        yield return new WaitForSeconds(2);
        canGetOut = true;
    }

    public void UnlockCar()
    {
        repaired = true;
        brokenParticle.Stop();
    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag != "Sidewalk" && col.gameObject.tag != "Ground" && col.gameObject.tag != "Road" && col.gameObject.tag != "Player")
        {
            if (!collided && inCar)
            {
                collided = true;
                if (col.gameObject.tag == "House")
                {
                    Damage(houseDam);
                }
                else
                {
                    Damage(otherDam);
                }

                if (durability > 30)
                {
                    StartCoroutine(CarCollision());
                }
                UpdateMoveSpeed();
                StartCoroutine(WaitForCollide());
            }
        }
    }

    void Damage(float i)
    {
        if(durability > 0)
        {
            durability -= i;
            //Debug.Log("POTVIS");
        }
    }

    void UpdateMoveSpeed()
    {
        if(durability < 70)
        {
            currentMoveSpeed = 13F;
        }
        if (durability < 50)
        {
            currentMoveSpeed = 10F;
        }
        if (durability <= 30)
        {
            currentMoveSpeed = 7;
            brokenParticle.Play();
        }
        if (durability < 10)
        {
            currentMoveSpeed = 4F;
        }
    }

    //werkt niet vanaf de pickup
    //public void Repair()
    //{
    //    durability = 100F;
    //    currentMoveSpeed = 16F;
    //    brokenParticle.Stop();
    //}

    IEnumerator WaitForCollide()
    {
        yield return new WaitForSeconds(1);
        collided = false;
    }

    IEnumerator CarCollision()
    {
        brokenParticle.Play();
        yield return new WaitForSeconds(0.5f);
        brokenParticle.Stop();
    }
}