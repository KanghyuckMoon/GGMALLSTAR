using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterP2Color_Frog : CharacterP2Color
{
	public CharacterP2Color_Frog(Character character) : base(character)
	{

	}

	public override void SetP2Color()
	{
		Character.GetComponentInChildren<SpriteRenderer>().material = Addressable.AddressablesManager.Instance.GetResource<Material>("SpriteShadowP2");
		//Character.GetComponentInChildren<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
	}
}
