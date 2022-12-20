using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
	/// <summary>
	/// ����â ��� ȸ��
	/// </summary>
	public class RotateCamera : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("speed")]
		private float _speed = 1.0f;
		private Vector3 _rotation = Vector3.zero;

		public void Update()
		{
			_rotation.y += Time.deltaTime * _speed;
			transform.eulerAngles = _rotation;
		}
	}
}