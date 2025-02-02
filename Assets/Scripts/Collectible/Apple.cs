﻿using UnityEngine;

public class Apple : MonoBehaviour
{
   
    public int appleValue = 1;
    
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        GameManager.instance.UpdateAppleBar(appleValue);

        if(gameObject.tag != "SpecialApple")
            AudioManager.instance.Play("Apple");

        if (gameObject.tag == "BossApple")
            GameManager.instance.StartCoroutine("SpawnApple", transform.position);

        Destroy(gameObject);
    }
}
