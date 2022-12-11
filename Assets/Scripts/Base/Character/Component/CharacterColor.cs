using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterColor : CharacterComponent
{
	protected Material originMaterial;
	public CharacterColor(Character character) : base(character)
    {

    }

    public abstract void SetP2Color();

	public abstract void SetWhiteMaterial();

	public abstract void SetOriginMaterial();
}
