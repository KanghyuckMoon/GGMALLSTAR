using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Loading;
using Sound;

public class RoundManager : MonoBehaviour
{
	[SerializeField] private CharacterSpawner characterSpawner;
	[SerializeField] private Transform spawnPosP1;
	[SerializeField] private Transform spawnPosP2;

	private Character characterP1;
	private Character characterP2;
	private int roundNumber = 1;
	private int winCountP1 = 0;
	private int winCountP2 = 0;
	private bool isSetting = false;

	private System.Action roundSetEvent;
	private System.Action roundReadyEvent;
	private System.Action roundStartEvent;
	private System.Action roundEndEvent;
	private System.Action gameEndEvent;

	public int WinCountP1 => winCountP1;
	public int WinCountP2 => winCountP2;

	public System.Action RoundSetEvent
	{
		get
		{
			return roundSetEvent;
		}
		set
		{
			roundSetEvent = value;
		}
	}

	public System.Action RoundReadyEvent
	{
		get
		{
			return roundReadyEvent;
		}
		set
		{
			roundReadyEvent = value;
		}
	}

	public System.Action RoundStartEvent
	{
		get
		{
			return roundStartEvent;
		}
		set
		{
			roundStartEvent = value;
		}
	}

	public System.Action RoundEndEvent
	{
		get
		{
			return roundEndEvent;
		}
		set
		{
			roundEndEvent = value;
		}
	}
	public System.Action GameEndEvent
	{
		get
		{
			return gameEndEvent;
		}
		set
		{
			gameEndEvent = value;
		}
	}
	public int RoundNumber => roundNumber;

	private IEnumerator Start()
	{
		characterP1 = characterSpawner.Player1.GetComponent<Character>();
		characterP2 = characterSpawner.Player2.GetComponent<Character>();

		yield return new WaitForEndOfFrame();

		HPFullSetting();
		PostionSetting();
		RoundSetting();
	}

	public static void RoundEnd(Character loser)
	{
		RoundManager roundManager = FindObjectOfType<RoundManager>();
		roundManager.RoundEndSetting(loser);
	}

	private void RoundEndSetting(Character loser)
	{
		if (isSetting is false)
		{
			return;
		}

		isSetting = false;

		if (loser == characterP1)
		{
			winCountP2++;
			Debug.Log("P1 KO");
		}
		else if (loser == characterP2)
		{
			winCountP1++;
			Debug.Log("P2 KO");
		}

		if (winCountP1 >= 3 || winCountP2 >= 3)
		{
			CharacterInput characterInputP1 = characterP1.GetCharacterComponent<CharacterInput>();
			characterInputP1.SetStunTime(1f);

			CharacterInput characterInputP2 = characterP2.GetCharacterComponent<CharacterInput>();
			characterInputP2.SetStunTime(1f);

			if (winCountP1 >= 3)
			{
				Debug.Log("Game End P1 Win");
			}
			else if (winCountP2 >= 3)
			{
				Debug.Log("Game End P2 Win");
			}
			SoundManager.Instance.PlayEFF("vc_narration_gameset_CN-JP-KR");
			StartCoroutine(GameEnd(6f));
		}
		else
		{
			//CharacterInput characterInputP1 = characterP1.GetCharacterComponent<CharacterInput>();
			//characterInputP1.SetStunTime(1f);
			//
			//CharacterInput characterInputP2 = characterP2.GetCharacterComponent<CharacterInput>();
			//characterInputP2.SetStunTime(1f);

			SoundManager.Instance.PlayEFF("vc_narration_gameset");

			StartCoroutine(NextRound(6f));
		}
	}

	private void HPFullSetting()
	{
		//HP Setting
		CharacterStat characterStatP1 = characterP1.GetCharacterComponent<CharacterStat>();
		characterStatP1.SetHP((int)characterStatP1.MaxHP);

		CharacterStat characterStatP2 = characterP2.GetCharacterComponent<CharacterStat>();
		characterStatP2.SetHP((int)characterStatP2.MaxHP);
	}

	private void PostionSetting()
	{
		characterP1.transform.position = spawnPosP1.position;
		characterP2.transform.position = spawnPosP2.position;
	}

	private void RoundSetting()
	{
		if (roundNumber == 5)
		{
			Debug.Log("Round Final");
		}
		else
		{
			Debug.Log($"Round {roundNumber}");
		}
		roundSetEvent?.Invoke();

		switch (roundNumber)
		{
			default:
			case 1:
				SoundManager.Instance.PlayEFF("vc_narration_one");
				break;
			case 2:
				SoundManager.Instance.PlayEFF("vc_narration_two");
				break;
			case 3:
				SoundManager.Instance.PlayEFF("vc_narration_three");
				break;
			case 4:
				SoundManager.Instance.PlayEFF("vc_narration_four");
				break;
			case 5:
				SoundManager.Instance.PlayEFF("vc_narration_five");
				break;
		}

		CharacterInput characterInputP1 = characterP1.GetCharacterComponent<CharacterInput>();
		characterInputP1.SetStunTime(2f);

		CharacterInput characterInputP2 = characterP2.GetCharacterComponent<CharacterInput>();
		characterInputP2.SetStunTime(2f);

		StartCoroutine(Fight(1f, 1f));

		roundNumber++;
	}


	private IEnumerator NextRound(float time)
	{
		roundEndEvent?.Invoke();
		   yield return new WaitForSeconds(time);
		HPFullSetting();
		PostionSetting();
		RoundSetting();
	}
	private IEnumerator GameEnd(float time)
	{
		gameEndEvent?.Invoke();
		yield return new WaitForSeconds(time);
		LoadingScene.Instance.LoadScene("Main", LoadingScene.LoadingSceneType.Normal);
	}

	private IEnumerator Fight(float readyTime, float fightTime)
	{
		yield return new WaitForSeconds(readyTime);
		SoundManager.Instance.PlayEFF("vc_narration_ready");
		roundReadyEvent?.Invoke();

		yield return new WaitForSeconds(fightTime);
		Debug.Log("Fight");
		SoundManager.Instance.PlayEFF("vc_narration_go");
		isSetting = true;
		roundStartEvent?.Invoke();
	}

}
