using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterP2Color_Jaeby : CharacterP2Color
{
	public CharacterP2Color_Jaeby(Character character) : base(character)
	{

	}

	public override void SetP2Color()
	{
		Character.GetComponentInChildren<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
	}
}
