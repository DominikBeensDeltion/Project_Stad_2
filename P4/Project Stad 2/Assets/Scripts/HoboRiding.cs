using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoboRiding : MonoBehaviour
{
    public float startSpeed = 5f;
    public float currentSpeed = 5f;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * currentSpeed));
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("player diededtd");
        }
    }
}
