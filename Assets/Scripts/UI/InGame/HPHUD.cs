using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPHUD : MonoBehaviour
{
	[SerializeField] private Image hpBarIamgeP1;
	[SerializeField] private Image hpBarIamgeP2;

	private CharacterStat characterStatP1;
	private CharacterStat characterStatP2;

	private void Start()
	{
		CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
		var characterP1 = characterSpawner.Player1.GetComponent<Character>();
		var characterP2 = characterSpawner.Player2.GetComponent<Character>();

		characterStatP1 = characterP1.GetCharacterComponent<CharacterStat>();
		characterStatP2 = characterP2.GetCharacterComponent<CharacterStat>();


		characterStatP1.AddHPEvent(ChangeHPP1);
		characterStatP2.AddHPEvent(ChangeHPP2);

		ChangeHPP1();
		ChangeHPP2();
	}

	private void ChangeHPP1()
	{
		//hpTextP1.text = $"{characterStatP1.HP}";
		hpBarIamgeP1.fillAmount = characterStatP1.HP / characterStatP1.MaxHP;
	}
	private void ChangeHPP2()
	{
		//hpTextP2.text = $"{characterStatP2.HP}";
		hpBarIamgeP2.fillAmount = characterStatP2.HP / characterStatP2.MaxHP;
	}

}
