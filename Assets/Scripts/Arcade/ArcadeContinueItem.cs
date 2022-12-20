using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Arcade
{
	public class ArcadeContinueItem : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("speed")]
		private float _speed = 1.0f;
		[SerializeField, FormerlySerializedAs("randomItems")]
		private GameObject[] _randomItems;

		private Vector3 _rotation = Vector3.zero;


		private void Start()
		{
			SetItem();
		}

		private void SetItem()
		{
			_randomItems[(int)SelectDataSO.characterSelectP1 - 1].SetActive(true);
		}

		private void Update()
		{
			_rotation.y += Time.deltaTime * _speed;
			_rotation.x += Time.deltaTime * _speed;
			_rotation.z += Time.deltaTime * _speed;
			transform.eulerAngles = _rotation;
		}
	}

}
