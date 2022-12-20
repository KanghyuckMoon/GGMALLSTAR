using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardItem : MonoBehaviour
{
	public enum RotateAxis
	{
		Normal,
		X,
		Y,
		Z,
	}

	public RotateAxis rotateAxis;
	private Vector3 originRot;

	private void Start()
	{
		originRot = transform.localEulerAngles;
	}

	private void LateUpdate()
	{
		transform.LookAt(Camera.main.transform);
		Vector3 eulerRot = transform.localEulerAngles;
		switch (rotateAxis)
		{
			case RotateAxis.Normal:
				transform.localEulerAngles += originRot;
				break;
			case RotateAxis.X:
				eulerRot.y = originRot.y;
				eulerRot.z = originRot.z;
				transform.localEulerAngles = eulerRot;
				break;
			case RotateAxis.Y:
				eulerRot.x = originRot.x;
				eulerRot.z = originRot.z;
				transform.localEulerAngles = eulerRot;
				break;
			case RotateAxis.Z:
				eulerRot.x = originRot.x;
				eulerRot.y = originRot.y;
				transform.localEulerAngles = eulerRot;
				break;
		}
	}
}
