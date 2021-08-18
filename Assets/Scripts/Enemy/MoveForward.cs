using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5.00f;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Snowball");
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
