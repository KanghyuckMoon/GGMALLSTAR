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

	private GameObject player1;
	private GameObject player2;

	private void Awake()
	{
		player1 = Instantiate(AddressablesManager.Instance.GetResource<GameObject>(SelectDataSO.characterSelectP1.ToString()), spawnPositionP1.position, Quaternion.identity);
		player2 = Instantiate(AddressablesManager.Instance.GetResource<GameObject>(SelectDataSO.characterSelectP2.ToString()), spawnPositionP2.position, Quaternion.identity);
		cinemachineTargetGroup.AddMember(player1.transform, 1, 1);
		cinemachineTargetGroup.AddMember(player2.transform, 1, 1);
	}
}
