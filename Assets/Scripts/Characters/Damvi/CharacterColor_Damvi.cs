using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColor_Damvi : CharacterColor
{
	public CharacterColor_Damvi(Character character) : base(character)
	{
		//originMaterial = Character.GetComponentInChildren<SpriteRenderer>().material;
	}


	public override void SetP2Color()
	{
		//originMaterial = Addressable.AddressablesManager.Instance.GetResource<Material>("SpriteShadowP2");
		//Character.GetComponentInChildren<SpriteRenderer>().material = originMaterial;
	}

	public override void SetOriginMaterial()
	{
		//Character.GetComponentInChildren<SpriteRenderer>().material = originMaterial;
	}

	public override void SetWhiteMaterial()
	{
		//Character.GetComponentInChildren<SpriteRenderer>().material = Addressable.AddressablesManager.Instance.GetResource<Material>("SpriteWhite");
	}
}
