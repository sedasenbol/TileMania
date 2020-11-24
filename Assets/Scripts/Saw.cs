using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Saw : MonoBehaviour
{
	private const float ROTATION_SPEED = -180;
	private Transform xform;
	private bool isRotating = false;
	private void Awake()
	{
		xform = GetComponent<Transform>();
		isRotating = false;
	}
	private void Start()
	{
		isRotating = true;
	}
	private void Update()
	{
		if (!isRotating)
		{
			return;
		}

		var euler = xform.rotation.eulerAngles;
		euler.z += ROTATION_SPEED * Time.deltaTime;
		xform.rotation = Quaternion.Euler(euler.x, euler.y, euler.z);
	}
}