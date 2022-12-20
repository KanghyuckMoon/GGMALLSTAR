using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// �ִϸ��̼� �׷����� ���� ȸ��
/// </summary>
public class TimeGraph : MonoBehaviour
{
	[SerializeField, FormerlySerializedAs("animationCurve")] 
	private AnimationCurve _animationCurve;
	[SerializeField, FormerlySerializedAs("period")] 
	private float _period = 2f;
	[SerializeField, FormerlySerializedAs("speed")] 
	private float _speed = 1f;

	private float _curTime;

	private void Update()
	{
		_curTime += Time.deltaTime * _speed;
		if (_curTime >= _period)
		{
			_curTime -= _curTime;
		}
		float yValue = _animationCurve.Evaluate(_curTime);
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, yValue, transform.eulerAngles.z);
	}

}
