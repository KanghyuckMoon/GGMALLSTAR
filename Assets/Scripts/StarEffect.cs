using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Pool;

public class StarEffect : MonoBehaviour
{
	private LevelHUD levelHUD;
	[SerializeField] private AnimationCurve moveCurve;

	public void SetEffect(bool isPlayer1, Vector3 startPos, CharacterLevel characterLevel, int addExp)
	{
		levelHUD ??= FindObjectOfType<LevelHUD>();
		transform.position = startPos;

		StartCoroutine(MoveToTarget(isPlayer1, startPos, characterLevel, addExp));
	}

	private IEnumerator MoveToTarget(bool isPlayer1, Vector3 startPos, CharacterLevel characterLevel, int addExp)
	{
		float time = 0f;
		float speed = 1f;
		while(time < 1f)
		{
			yield return null;
			Vector3 pos = isPlayer1 ? levelHUD.LevelTextP1.position : levelHUD.LevelTextP2.position;
			pos.z = startPos.z;
			//Vector3 viewPos = Camera.main.WorldToViewportPoint(pos);
			//Vector3 targetPos = Camera.main.ViewportToWorldPoint(viewPos);
			transform.position = LinearBezierPoint(moveCurve.Evaluate(time), startPos, pos);
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
