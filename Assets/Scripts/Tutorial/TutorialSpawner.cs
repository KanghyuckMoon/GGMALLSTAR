using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Addressable;
using Utill;
using Cinemachine;

namespace Tutorial
{
	public class TutorialSpawner : CharacterSpawner
	{

		protected override void Awake()
		{
			player1 = Instantiate(AddressablesManager.Instance.GetResource<GameObject>("Jaeby"), spawnPositionP1.position, Quaternion.identity);
			player1.GetComponent<Character>().InputDataBaseSO = inputDataBaseSOP1;

			player2 = Instantiate(AddressablesManager.Instance.GetResource<GameObject>("Frog_AI_Tutorial"), spawnPositionP2.position, Quaternion.identity);
			player2.tag = "Player2";

			cinemachineTargetGroup.AddMember(player1.transform, 1, 1);
			cinemachineTargetGroup.AddMember(player2.transform, 1, 1);
		}
	}

}
