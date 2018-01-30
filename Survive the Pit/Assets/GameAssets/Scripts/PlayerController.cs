﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	Vector2 mouselook;
	Vector2 smoothV;

	public float sensitivity = 5f;
	public float smoothing = 1f;

	public float viewClampUp = -90f;
	public float viewClampDown = 80f;

	private GameObject player;
	private GameObject mainCam;

	private void Start()
	{
		player = this.gameObject;
		mainCam = Camera.main.gameObject;
	}

	private void Update()
	{
		RotateCamera();
	}

	private void RotateCamera()
	{
		// TODO Split into controller and keyboard sections based on input
		Vector2 md = new Vector2(hInput.GetAxis("RotateCameraX"), hInput.GetAxis("RotateCameraY"));

		md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
		mouselook += smoothV;
		mouselook.y = Mathf.Clamp(mouselook.y, viewClampUp, viewClampDown);

		mainCam.transform.localRotation = Quaternion.AngleAxis(mouselook.y, Vector3.right);
		player.transform.localRotation = Quaternion.AngleAxis(mouselook.x, player.transform.up);
	}
}
