using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Pool;

public class StarEffect : MonoBehaviour
{
	private LevelHUD levelHUD;
	[SerializeField] private AnimationCurve moveCurve;

	public void SetEffect(Vector3 startPos, CharacterLevel characterLevel, int addExp)
	{
		transform.rotation = Quaternion.identity;
		levelHUD ??= FindObjectOfType<LevelHUD>();
		transform.position = startPos;
		StartCoroutine(MoveToTarget(startPos, characterLevel, addExp));
	}

	private IEnumerator MoveToTarget(Vector3 startPos, CharacterLevel characterLevel, int addExp)
	{

		float startAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
		transform.rotation = Quaternion.AngleAxis(startAngle, Vector3.forward);
		Vector3 spreadPos = startPos + new Vector3(Mathf.Cos(startAngle), Mathf.Sin(startAngle), 0);
		spreadPos.z = startPos.z;

		float delay = 1f + Random.Range(0.3f, 0.5f);
		float delayCurernt = 0f;
		while (delayCurernt < delay)
		{
			yield return null;
			transform.position = LinearBezierPoint(moveCurve.Evaluate(delayCurernt), startPos, spreadPos);
			delayCurernt += Time.deltaTime;
		}

		transform.DOKill();

		float time = 0f;
		float speed = 1f;

		float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
		Vector3 shotPos = transform.position;
		Vector3 prevPoint = startPos;
		Vector3 dir = Vector3.zero;

		while (time < 1f)
		{
			yield return null;
			
			//Position
			Vector3 targetPos = characterLevel.Character.transform.position;
			transform.position = LinearBezierPoint(moveCurve.Evaluate(time), shotPos, targetPos);

			//Rotate
			if (time > 0.01f)
			{
				dir = transform.position - prevPoint;
				angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

				prevPoint = transform.position;
			}

			time += Time.deltaTime * speed;


		}

		characterLevel.AddExp(addExp);
		PoolManager.AddObjToPool("StarEff", gameObject);
		gameObject.SetActive(false);
	}


	private Vector3 LinearBezierPoint(float t, Vector3 start, Vector3 end)
	{
		return start + (t * (end - start));
	}
}
