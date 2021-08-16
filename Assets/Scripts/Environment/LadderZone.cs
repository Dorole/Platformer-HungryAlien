using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=KBSHz-ee8Sk&t=954s&ab_channel=gamesplusjames
public class LadderZone : MonoBehaviour
{
    private PlayerMovementController player;
    private float nextTimeToSearch = 2.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
    }

    private void LateUpdate()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            player.IsOnLadder = true;
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
           player.IsOnLadder = false;          
    }

    private void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                player = searchResult.GetComponent<PlayerMovementController>();
            nextTimeToSearch = Time.time + 0.5f;
        }

    }
}
