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

	[SerializeField] protected CinemachineTargetGroup cinemachineTargetGroup;
	[SerializeField] protected Transform spawnPositionP1;
	[SerializeField] protected Transform spawnPositionP2;

	[SerializeField] protected InputDataBaseSO inputDataBaseSOP1;
	[SerializeField] protected InputDataBaseSO inputDataBaseSOP2;

	protected GameObject player1;
	protected GameObject player2;

	protected virtual void Awake()
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

	protected virtual IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		if(SelectDataSO.characterSelectP1 == SelectDataSO.characterSelectP2)
		{
			player2.GetComponent<Character>().GetCharacterComponent<CharacterColor>(ComponentType.Color).SetP2Color();
		}
		player1.GetComponent<Character>().GetCharacterComponent<CharacterStat>(ComponentType.Stat).IsPlayerP1 = true;
	}


}
