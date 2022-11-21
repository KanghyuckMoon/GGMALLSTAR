using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Addressable;
using Utill;
using Cinemachine;

public class CharacterSpawner : MonoBehaviour
{
	[SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;
	[SerializeField] private Transform spawnPositionP1;
	[SerializeField] private Transform spawnPositionP2;
	private void Awake()
	{
		GameObject p1 = Instantiate(AddressablesManager.Instance.GetResource<GameObject>(SelectDataSO.characterSelectP1.ToString()), spawnPositionP1.position, Quaternion.identity);
		GameObject p2 = Instantiate(AddressablesManager.Instance.GetResource<GameObject>(SelectDataSO.characterSelectP2.ToString()), spawnPositionP2.position, Quaternion.identity);
		cinemachineTargetGroup.AddMember(p1.transform, 1, 1);
		cinemachineTargetGroup.AddMember(p2.transform, 1, 1);
	}
}
