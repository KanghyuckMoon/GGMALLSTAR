using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
	[SerializeField] private RectTransform creditTransform;
	[SerializeField] private float posY = 4000f;
	[SerializeField] private float duration = 10f;


	private void Start()
	{
		creditTransform.DOAnchorPosY(posY, duration).SetDelay(3f).OnComplete(() => MoveTitle());
	}

	private void MoveTitle()
	{
		SceneManager.LoadScene("Title");
	}

}
