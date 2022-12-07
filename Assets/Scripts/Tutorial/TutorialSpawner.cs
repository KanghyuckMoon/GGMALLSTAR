using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Addressable;
using Utill;
using Cinemachine;

public class TutorialSpawner : CharacterSpawner
{

	protected override void Awake()
	{
		if (SelectDataSO.isAICharacterP1)
		{
			player1 = Instantiate(AddressablesManager.Instance.GetResource<GameObject>(SelectDataSO.characterSelectP1.ToString() + "_AI"), spawnPositionP1.position, Quaternion.identity);
		}
		else
		{
			player1 = Instantiate(AddressablesManager.Instance.GetResource<GameObject>(SelectDataSO.characterSelectP1.ToString()), spawnPositionP1.position, Quaternion.identity);
			player1.GetComponent<Character>().InputDataBaseSO = inputDataBaseSOP1;
		}


		if (SelectDataSO.isAICharacterP2)
		{
			player2 = Instantiate(AddressablesManager.Instance.GetResource<GameObject>(SelectDataSO.characterSelectP2.ToString() + "_AI"), spawnPositionP2.position, Quaternion.identity);
		}
		else
		{
			player2 = Instantiate(AddressablesManager.Instance.GetResource<GameObject>(SelectDataSO.characterSelectP2.ToString()), spawnPositionP2.position, Quaternion.identity);
			player2.GetComponent<Character>().InputDataBaseSO = inputDataBaseSOP2;
		}
		player2.tag = "Player2";

		cinemachineTargetGroup.AddMember(player1.transform, 1, 1);
		cinemachineTargetGroup.AddMember(player2.transform, 1, 1);
	}
}
