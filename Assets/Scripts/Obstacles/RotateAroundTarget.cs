using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundTarget : MonoBehaviour
{
	public Transform Target;
	public float Speed;
	public Vector3 Axis;

    // Update is called once per frame
    void Update()
    {
		transform.RotateAround(Target.position, Axis, Speed * Time.deltaTime);
    }
}
