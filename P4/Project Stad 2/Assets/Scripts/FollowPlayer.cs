using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameManager gm;
    private GameObject player;

    public bool lerpOrNot;

    public Vector3 offset;
    public float followSpeed = 30f;
    public float posY;

    private void Start()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
        posY = player.transform.position.y;
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        if (gm.gameState == GameManager.GameState.Playing)
        {
            if (!lerpOrNot)
            {
                //transform.position = player.transform.position + offset;
                Vector3 position = new Vector3(player.transform.position.x, posY, player.transform.position.z) + offset;
                transform.position = Vector3.MoveTowards(transform.position, position, (followSpeed * Time.deltaTime));
            }
            else if (lerpOrNot)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, (followSpeed * Time.deltaTime));
            }
        }
    }
}
