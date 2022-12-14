using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterSelect
{
	None = 0,
	Jaeby,
	Frog,
	Dice,
	Count
}

public enum StageSelect
{
	None = 0,
	Training,
	FoolCity,
	Well,
	QuietTown,
	Tower,
	LostKingdom,
	AgentStage,
	Bridge,
	Count
}

public static class SelectDataSO
{
	static public CharacterSelect characterSelectP1;
	static public CharacterSelect characterSelectP2;
	static public bool isAICharacterP1;
	static public bool isAICharacterP2;
	static public StageSelect stageSelect;
	static public int aiLevelP1 = 1;
	static public int aiLevelP2 = 1;


	//Arcade
	static public int priviousWinCount = 0;
	static public int winCount = 0;
	static public bool isArcade = false;

	//Tutorial
	static public bool isTutorial = false;
}
