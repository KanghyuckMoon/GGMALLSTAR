using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Addressable;
using Utill;
using Cinemachine;

public class CharacterSpawner : MonoBehaviour
{
	public GameObject Player1
	{
		get
		{
			return player1;
		}
	}
	public GameObject Player2
	{
		get
		{
			return player2;
		}
	}

	[SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;
	[SerializeField] private Transform spawnPositionP1;
	[SerializeField] private Transform spawnPositionP2;

	[SerializeField] private InputDataBaseSO inputDataBaseSOP1;
	[SerializeField] private InputDataBaseSO inputDataBaseSOP2;

	private GameObject player1;
	private GameObject player2;

	private void Awake()
	{
		if(SelectDataSO.isAICharacterP1)
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

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		if(SelectDataSO.characterSelectP1 == SelectDataSO.characterSelectP2)
		{
			player2.GetComponent<Character>().GetCharacterComponent<CharacterP2Color>().SetP2Color();
		}
		player1.GetComponent<Character>().GetCharacterComponent<CharacterStat>().IsPlayerP1 = true;
	}


}
