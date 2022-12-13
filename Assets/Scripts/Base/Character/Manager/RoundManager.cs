using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Loading;
using Sound;

public class RoundManager : MonoBehaviour
{
	public static CharacterDebugData characterDebugDataP1;
	public static CharacterDebugData characterDebugDataP2;

	[SerializeField] private CharacterSpawner characterSpawner;
	[SerializeField] private Transform spawnPosP1;
	[SerializeField] private Transform spawnPosP2;

	private static RoundManager instance;
	private Character characterP1;
	private Character characterP2;
	private int roundNumber = 1;
	private int winCountP1 = 0;
	private int winCountP2 = 0;
	private bool isSetting = false;

	private System.Action gameSetEvent;
	private System.Action roundSetEvent;
	private System.Action roundReadyEvent;
	private System.Action roundStartEvent;
	private System.Action roundEndEvent;
	private System.Action gameEndEvent;
	private System.Action timeChangeEvent;

	public bool IsSetting
	{
		get
		{
			return isSetting;
		}
	}
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
	public System.Action GameSetEvent
	{
		get
		{
			return gameSetEvent;
		}
		set
		{
			gameSetEvent = value;
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
		instance = this;
		characterP1 = characterSpawner.Player1.GetComponent<Character>();
		characterP2 = characterSpawner.Player2.GetComponent<Character>();

		yield return new WaitForEndOfFrame();

		HPFullSetting();
		PostionSetting();
		SetInputSturnTime(5f);
		yield return new WaitForSeconds(1f);

		gameSetEvent?.Invoke();
		CameraManager.SetGameStart(characterP1.transform, characterP2.transform);

		yield return new WaitForSeconds(3f);
		RoundSetting();

	}

	public static int GetRoundNumber()
	{
		return instance.roundNumber;
	}

	public static void RoundEnd(Character loser)
	{
		instance.RoundEndSetting(loser);
	}

	public static bool ReturnIsSetting()
	{
		return instance.isSetting;
	}

	public static void StaticSetInputSturnTime(float time)
	{
		instance.SetInputSturnTime(time);
	}
	public static void StaticStopMove(float time)
	{
		instance.StopMove(time);
	}
	private void Update()
	{
		if (SelectDataSO.isTutorial)
		{
			return;
		}

		if (isSetting)
		{
			if (time > 0f)
			{
				time -= UnityEngine.Time.deltaTime;
				timeChangeEvent?.Invoke();
			}
			else
			{
				CharacterStat characterStatP1 = characterP1.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
				CharacterStat characterStatP2 = characterP2.GetCharacterComponent<CharacterStat>(ComponentType.Stat);

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

	private void RoundEndSetting(Character loser)
	{
		if (isSetting is false)
		{
			return;
		}
		isSetting = false;

		CameraManager.SetKO(loser.transform, 3f);
		SetInputSturnTime(6f);
		StopMove(6f);


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
			if (winCountP1 >= 3)
			{
				Debug.Log("Game End P1 Win");
			}
			else if (winCountP2 >= 3)
			{
				Debug.Log("Game End P2 Win");
			}
			SoundManager.Instance.PlayEFF("AkBankBattle#500 (1038384928)");
			StartCoroutine(GameEnd(6f));
		}
		else
		{
			SoundManager.Instance.PlayEFF("AkBankBattle#500 (1038384928)");
			StartCoroutine(NextRound(6f));
		}
	}

	private void HPFullSetting()
	{
		//HP Setting
		CharacterStat characterStatP1 = characterP1.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
		characterStatP1.SetHP((int)characterStatP1.MaxHP);

		CharacterStat characterStatP2 = characterP2.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
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
				SoundManager.Instance.PlayEFF("AkBankBattle#29 (57556810)");
				break;
			case 2:
				SoundManager.Instance.PlayEFF("AkBankBattle#320 (663251579)");
				break;
			case 3:
				SoundManager.Instance.PlayEFF("AkBankBattle#92 (185443775)");
				break;
			case 4:
				SoundManager.Instance.PlayEFF("AkBankBattle#420 (871787823)");
				break;
			case 5:
				SoundManager.Instance.PlayEFF("AkBankBattle#122 (241347237)");
				break;
		}
		SetInputSturnTime(3f);
		StopMove(0f);
		StartCoroutine(Fight(1f, 1f));

		roundNumber++;
	}


	private IEnumerator NextRound(float time)
	{
		roundEndEvent?.Invoke();
		yield return new WaitForSeconds(time);
		SoundManager.Instance.SetBGMSpeed(SoundManager.Instance.Pitch + 0.1f);
		HPFullSetting();
		PostionSetting();
		RoundSetting();
	}
	private IEnumerator GameEnd(float time)
	{
		gameEndEvent?.Invoke();
		yield return new WaitForSeconds(time);
		SoundManager.Instance.SetBGMSpeed(1.0f);
		if (SelectDataSO.isArcade && winCountP1 == 3)
		{
			SelectDataSO.winCount++;
			LoadingScene.Instance.LoadScene("Arcade", LoadingScene.LoadingSceneType.Normal);
		}
		else if(SelectDataSO.isArcade)
		{
			LoadingScene.Instance.LoadScene("ArcadeContinueScene", LoadingScene.LoadingSceneType.Normal);
		}
		else
		{
			characterDebugDataP1 = characterP1.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).CharacterDebugData;
			characterDebugDataP2 = characterP2.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).CharacterDebugData;

			characterDebugDataP1.damage = characterDebugDataP2.damaged;
			characterDebugDataP2.damage = characterDebugDataP1.damaged;
			characterDebugDataP1.winRoundCount = winCountP1;
			characterDebugDataP1.loseRoundCount = winCountP2;
			characterDebugDataP2.winRoundCount = winCountP2;
			characterDebugDataP2.loseRoundCount = winCountP1;

			LoadingScene.Instance.LoadScene("ResultScene", LoadingScene.LoadingSceneType.Result);
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
		SoundManager.Instance.PlayEFF("AkBankBattle#128 (253827322)");
		isSetting = true;
		roundStartEvent?.Invoke();
	}

	public void SetInputSturnTime(float stopTime)
	{
		StopCharacterInputSturn(characterP1, stopTime);
		StopCharacterInputSturn(characterP2, stopTime);
	}

	public void StopMove(float stopTime)
	{
		StopCharacter(characterP1, stopTime);
		StopCharacter(characterP2, stopTime);
	}

	private void StopCharacterInputSturn(Character character, float stopTime)
	{
		CharacterInput characterInput = character.GetCharacterComponent<CharacterInput>(ComponentType.Input);
		if (characterInput != null)
		{
			characterInput.SetStunTime(stopTime);
		}
		else
		{
			var aITestInput = character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
			aITestInput.SetStunTime(stopTime);
		}
	}

	private void StopCharacter(Character character, float stopTime)
	{
		CharacterGravity characterGravity = character.GetCharacterComponent<CharacterGravity>(ComponentType.Gravity);
		CharacterMove characterMove = character.GetCharacterComponent<CharacterMove>(ComponentType.Move);
		CharacterAnimation characterAnimation = character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
		CharacterDodge characterDodge = character.GetCharacterComponent<CharacterDodge>(ComponentType.Dodge);

		characterGravity.SetHitTime(stopTime);
		characterMove.SetSturnTime(stopTime);
		characterDodge.SetSturnTime(stopTime);
		characterAnimation.SetHitTime(stopTime);
		character.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

}
