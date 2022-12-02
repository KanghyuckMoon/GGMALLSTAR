using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HPHUD : MonoBehaviour
{
	[SerializeField] private Image hpBarIamgeP1;
	[SerializeField] private Image hpBarIamgeP2;
	[SerializeField] private RectTransform characterImageP1;
	[SerializeField] private RectTransform characterImageP2;

	private CharacterStat characterStatP1;
	private CharacterStat characterStatP2;

	private Vector3 originPosP1;
	private Vector3 originPosP2;

	private float previousHPP1;
	private float previousHPP2;


	private void Start()
	{
		CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
		var characterP1 = characterSpawner.Player1.GetComponent<Character>();
		var characterP2 = characterSpawner.Player2.GetComponent<Character>();

		characterStatP1 = characterP1.GetCharacterComponent<CharacterStat>();
		characterStatP2 = characterP2.GetCharacterComponent<CharacterStat>();


		characterStatP1.AddHPEvent(ChangeHPP1);
		characterStatP2.AddHPEvent(ChangeHPP2);

		previousHPP1 = characterStatP1.HP;
		previousHPP2 = characterStatP2.HP;

		originPosP1 = characterImageP1.localPosition;
		originPosP2 = characterImageP2.localPosition;

		ChangeHPP1();
		ChangeHPP2();
	}

	private void ChangeHPP1()
	{
		//hpTextP1.text = $"{characterStatP1.HP}";
		hpBarIamgeP1.fillAmount = characterStatP1.HP / characterStatP1.MaxHP;

		if(previousHPP1 > characterStatP1.HP)
		{
			characterImageP1.DOKill();
			characterImageP1.DOShakePosition(0.2f, 5).OnComplete(() =>
			{
				characterImageP1.localPosition = originPosP1;
			});
		}

		previousHPP1 = characterStatP1.HP;
	}
	private void ChangeHPP2()
	{
		//hpTextP2.text = $"{characterStatP2.HP}";
		hpBarIamgeP2.fillAmount = characterStatP2.HP / characterStatP2.MaxHP;

		if (previousHPP2 > characterStatP2.HP)
		{
			characterImageP2.DOKill();
			characterImageP2.DOShakePosition(0.2f, 5).OnComplete(() =>
			{
				characterImageP2.localPosition = originPosP2;
			});
		}

		previousHPP2 = characterStatP2.HP;
	}

}
