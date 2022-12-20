using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColor_Frog : CharacterColor
{
    public CharacterColor_Frog(Character character) : base(character)
    {
        _originMaterial = Character.GetComponentInChildren<SpriteRenderer>().material;
    }


    public override void SetP2Color()
    {
        _originMaterial = Addressable.AddressablesManager.Instance.GetResource<Material>("SpriteShadowP2");
        Character.GetComponentInChildren<SpriteRenderer>().material = _originMaterial;
    }

    public override void SetOriginMaterial()
    {
        Character.GetComponentInChildren<SpriteRenderer>().material = _originMaterial;
    }

    public override void SetWhiteMaterial()
    {
        Character.GetComponentInChildren<SpriteRenderer>().material = Addressable.AddressablesManager.Instance.GetResource<Material>("SpriteWhite");
    }
}
