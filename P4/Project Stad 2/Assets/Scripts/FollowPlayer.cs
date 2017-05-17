using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;

    public bool canFollow;
    public bool noLerp;
    public bool lerp;

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
            if (noLerp)
            {
                transform.position = player.transform.position + offset;
            }
            else if (lerp)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, (followSpeed * Time.deltaTime));
            }
        }
    }
}
