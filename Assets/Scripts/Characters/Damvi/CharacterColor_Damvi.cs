using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterColor_Damvi : CharacterColor
{
	private Material origin_body;
	private Material origin_hair;
	private Material origin_trans;
	private Material origin_wear;
	private Material origin_eye;


	private Material origin_bodyP2;
	private Material origin_hairP2;
	private Material origin_transP2;
	private Material origin_wearP2;
	private Material origin_eyeP2;

	private List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

	public CharacterColor_Damvi(Character character) : base(character)
	{
		origin_body = Addressable.AddressablesManager.Instance.GetResource<Material>("Damvi_BodyP1");
		origin_hair = Addressable.AddressablesManager.Instance.GetResource<Material>("Damvi_HairP1");
		origin_trans = Addressable.AddressablesManager.Instance.GetResource<Material>("Damvi_TransP1");
		origin_wear = Addressable.AddressablesManager.Instance.GetResource<Material>("Damvi_WearP1");
		origin_eye = Addressable.AddressablesManager.Instance.GetResource<Material>("Damvi_EyeP1");
		skinnedMeshRenderers = Character.GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
		//originMaterial = Character.GetComponentInChildren<SpriteRenderer>().material;
	}


	public override void SetP2Color()
	{
		origin_bodyP2 = Addressable.AddressablesManager.Instance.GetResource<Material>("Damvi_BodyP2");
		origin_hairP2 = Addressable.AddressablesManager.Instance.GetResource<Material>("Damvi_HairP2");
		origin_transP2 = Addressable.AddressablesManager.Instance.GetResource<Material>("Damvi_TransP2");
		origin_wearP2 = Addressable.AddressablesManager.Instance.GetResource<Material>("Damvi_WearP2");
		origin_eyeP2 = Addressable.AddressablesManager.Instance.GetResource<Material>("Damvi_EyeP2");
		
		for(int i = 0; i < skinnedMeshRenderers.Count; ++i)
		{
			var materials = skinnedMeshRenderers[i].materials;
			for (int j = 0; j < materials.Length; ++j)
			{
				if(materials[j].name == $"{origin_body.name} (Instance)")
				{
					materials[j] = origin_bodyP2;
				}
				else if (materials[j].name == $"{origin_hair.name} (Instance)")
				{
					materials[j] = origin_hairP2;
				}
				else if (materials[j].name == $"{origin_trans.name} (Instance)")
				{
					materials[j] = origin_transP2;
				}
				else if (materials[j].name == $"{origin_wear.name} (Instance)")
				{
					materials[j] = origin_wearP2;
				}
				else if (materials[j].name == $"{origin_eye.name} (Instance)")
				{
					materials[j] = origin_eyeP2;
				}
			}
			skinnedMeshRenderers[i].materials = materials;
		}

		//Character.GetComponentInChildren<SpriteRenderer>().material = originMaterial;
	}

	public override void SetOriginMaterial()
	{

	}

	public override void SetWhiteMaterial()
	{

	}

}
