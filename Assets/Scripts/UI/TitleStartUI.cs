using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sound;

public class TitleStartUI : MonoBehaviour
{
	public Image screenImage;
	public string effSound;
	public PressSpace pressSpace;
	public float speed = 1.0f;

	private void Start()
	{
		pressSpace.coroutineEvent += StartAnimation;
	}

	private IEnumerator StartAnimation()
	{
		SoundManager.Instance.PlayEFF(effSound);
		for (float i = 0; i < 1.0f;)
		{
			i += Time.deltaTime * speed;
			screenImage.color = new Color(1, 1, 1, i);
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}
}
