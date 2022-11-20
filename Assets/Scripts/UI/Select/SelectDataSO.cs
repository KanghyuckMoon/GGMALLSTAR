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
	Count
}

public static class SelectDataSO
{
	static public CharacterSelect characterSelectP1;
	static public CharacterSelect characterSelectP2;
	static public StageSelect stageSelect;
}
