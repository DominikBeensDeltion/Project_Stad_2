using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{

    private UIManager uim;

    private void Start()
    {
        uim = GameObject.FindGameObjectWithTag("UIM").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            uim.orderText.text = "Here lies: s" + transform.name + "\n\nLost but not forgotten";
            uim.orderAnimator.SetBool("Order", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            uim.orderAnimator.SetBool("Order", false);
        }
    }
}
