using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGraph : MonoBehaviour
{
	[SerializeField] private AnimationCurve animationCurve;
	[SerializeField] private float period = 2f;
	[SerializeField] private float speed = 1f;
	private float curTime;

	private void Update()
	{
		curTime += Time.deltaTime * speed;
		if (curTime >= period)
		{
			curTime -= curTime;
		}
		float yValue = animationCurve.Evaluate(curTime);
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, yValue, transform.eulerAngles.z);
	}

}
