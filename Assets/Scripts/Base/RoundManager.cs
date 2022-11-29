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
	private System.Action timeChangeEvent;

	public int WinCountP1 => winCountP1;
	public int WinCountP2 => winCountP2;

	private float time = 0f;
	public float Time => time;


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
	public System.Action TimeChangeEvent
	{
		get
		{
			return timeChangeEvent;
		}
		set
		{
			timeChangeEvent = value;
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
			SetInputSturnTime(2f);

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
		SetInputSturnTime(2f);
		StartCoroutine(Fight(1f, 1f));

		roundNumber++;
	}


	private IEnumerator NextRound(float time)
	{
		roundEndEvent?.Invoke();
		   yield return new WaitForSeconds(time);
		if (SelectDataSO.isArcade)
		{
			if(winCountP1 > 0)
			{
				SelectDataSO.winCount++;
				LoadingScene.Instance.LoadScene("Arcade", LoadingScene.LoadingSceneType.Normal);
			}
			else
			{
				LoadingScene.Instance.LoadScene("Main", LoadingScene.LoadingSceneType.Normal);
			}
		}
		else
		{
			HPFullSetting();
			PostionSetting();
			RoundSetting();
		}

	}
	private IEnumerator GameEnd(float time)
	{
		gameEndEvent?.Invoke();
		yield return new WaitForSeconds(time);
		if (SelectDataSO.isArcade && winCountP1 > 0)
		{
			SelectDataSO.winCount++;
			LoadingScene.Instance.LoadScene("Arcade", LoadingScene.LoadingSceneType.Normal);
		}
		else
		{
			LoadingScene.Instance.LoadScene("Main", LoadingScene.LoadingSceneType.Normal);
		}
	}

	private IEnumerator Fight(float readyTime, float fightTime)
	{
		yield return new WaitForSeconds(readyTime);
		SoundManager.Instance.PlayEFF("vc_narration_ready");
		roundReadyEvent?.Invoke();
		time = 99f;

		yield return new WaitForSeconds(fightTime);
		Debug.Log("Fight");
		SoundManager.Instance.PlayEFF("vc_narration_go");
		isSetting = true;
		roundStartEvent?.Invoke();
	}

	private void SetInputSturnTime(float time)
	{
		CharacterInput characterInputP1 = characterP1.GetCharacterComponent<CharacterInput>();
		if (characterInputP1 != null)
		{
			characterInputP1.SetStunTime(time);
		}
		else
		{
			var aITestInputP1 = characterP1.GetCharacterComponent<CharacterAIInput>();
			aITestInputP1.SetStunTime(time);
		}

		CharacterInput characterInputP2 = characterP2.GetCharacterComponent<CharacterInput>();
		if (characterInputP2 != null)
		{
			characterInputP2.SetStunTime(time);
		}
		else
		{
			var aITestInputP2 = characterP2.GetCharacterComponent<CharacterAIInput>();
			aITestInputP2.SetStunTime(time);
		}

	}

	private void Update()
	{
		if (isSetting)
		{
			if (time > 0f)
			{
				time -= UnityEngine.Time.deltaTime;
				timeChangeEvent?.Invoke();
			}
			else
			{
				CharacterStat characterStatP1 = characterP1.GetCharacterComponent<CharacterStat>();
				CharacterStat characterStatP2 = characterP2.GetCharacterComponent<CharacterStat>();

				if (characterStatP1.HP / characterStatP1.MaxHP > characterStatP2.HP / characterStatP2.MaxHP)
				{
					RoundEndSetting(characterP2);
				}
				else
				{
					RoundEndSetting(characterP1);
				}
			}
		}
	}

}
