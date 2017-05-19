using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;

    public bool canFollow;
    public bool lerpOrNot;

    public Vector3 offset;
    public float followSpeed = 3f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        if (canFollow)
        {
            if (!lerpOrNot)
            {
                transform.position = player.transform.position + offset;
            }
            else if (lerpOrNot)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, (followSpeed * Time.deltaTime));
            }
        }
    }
}
