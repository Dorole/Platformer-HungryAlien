using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5.00f;

    void FixedUpdate()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
