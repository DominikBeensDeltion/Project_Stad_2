using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionDirection : MonoBehaviour
{

    public enum State
    {
        LeftRightForward,
        LeftRight,
        Left,
        Right,
        ForwardRight,
        ForwardLeft
    }
    public State intersectionDirection;

    public GameObject hobo;

    public void OnTriggerExit(Collider other)
    {
        if (intersectionDirection == State.LeftRightForward)
        {
            if (other.tag == "RidingHobo")
            {
                int direction = Random.Range(0, 3);

                if (direction == 0)
                {
                    //do nothing, hobo keeps going forward
                }
                else if (direction == 1)
                {
                    other.transform.Rotate(0, 90, 0);
                }
                else if (direction == 2)
                {
                    hobo = other.gameObject;
                    StartCoroutine(GoLeft());
                }
            }
        }
        else if (intersectionDirection == State.LeftRight)
        {
            if (other.tag == "RidingHobo")
            {
                int direction = Random.Range(0, 2);

                if (direction == 0)
                {
                    other.transform.Rotate(0, 90, 0);
                }
                else if (direction == 1)
                {
                    hobo = other.gameObject;
                    StartCoroutine(GoLeft());
                }
            }
        }
        else if (intersectionDirection == State.Left)
        {
            if (other.tag == "RidingHobo")
            {
                hobo = other.gameObject;
                StartCoroutine(GoLeft());
            }
        }
        else if (intersectionDirection == State.Right)
        {
            if (other.tag == "RidingHobo")
            {
                other.transform.Rotate(0, 90, 0);
            }
        }
        else if (intersectionDirection == State.ForwardRight)
        {
            if (other.tag == "RidingHobo")
            {
                int direction = Random.Range(0, 2);

                if (direction == 0)
                {
                    //do nothing, hobo keeps going forward
                }
                else if (direction == 1)
                {
                    other.transform.Rotate(0, 90, 0);
                }
            }
        }
        else if (intersectionDirection == State.ForwardLeft)
        {
            if (other.tag == "RidingHobo")
            {
                int direction = Random.Range(0, 2);

                if (direction == 0)
                {
                    //do nothing, hobo keeps going forward
                }
                else if (direction == 1)
                {
                    hobo = other.gameObject;
                    StartCoroutine(GoLeft());
                }
            }
        }
    }

    public IEnumerator GoLeft()
    {
        yield return new WaitForSeconds(0.4f);
        hobo.transform.Rotate(0, -90, 0);
    }
}
