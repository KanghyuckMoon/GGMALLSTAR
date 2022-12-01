using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Addressable;
using TMPro;

public class SkillHUD : MonoBehaviour
{
	[SerializeField] private Image[] skillImagesP1;
	[SerializeField] private Image[] skillImagesP2;

	[SerializeField] private Image[] skillHideImagesP1;
	[SerializeField] private Image[] skillHideImagesP2;

	[SerializeField] private TextMeshProUGUI[] skillCoolTimeTextP1;
	[SerializeField] private TextMeshProUGUI[] skillCoolTimeTextP2;


	private CharacterLevel characterLevelP1;
	private CharacterLevel characterLevelP2;
	private CharacterSkill characterSkillP1;
	private CharacterSkill characterSkillP2;


	private void Start()
	{
		CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
		var characterP1 = characterSpawner.Player1.GetComponent<Character>();
		var characterP2 = characterSpawner.Player2.GetComponent<Character>();

		characterLevelP1 = characterP1.GetCharacterComponent<CharacterLevel>();
		characterLevelP2 = characterP2.GetCharacterComponent<CharacterLevel>();

		characterSkillP1 = characterP1.GetCharacterComponent<CharacterSkill>();
		characterSkillP2 = characterP2.GetCharacterComponent<CharacterSkill>();




		if (characterSkillP1 != null)
		{
			characterSkillP1?.AddSkill1CoolTimeChange(ChangeSkill1_P1);
			characterSkillP1?.AddSkill2CoolTimeChange(ChangeSkill2_P1);
			characterLevelP1?.AddChangeLevelEvent(ChangeSkill1_P1);
			characterLevelP1?.AddChangeLevelEvent(ChangeSkill2_P1);
			characterLevelP1?.AddChangeLevelEvent(ChangeSkill3_P1);

			ChangeSkill1_P1();
			ChangeSkill2_P1();
			ChangeSkill3_P1();
		}

		if (characterSkillP2 != null)
		{
			characterSkillP2?.AddSkill1CoolTimeChange(ChangeSkill1_P2);
			characterSkillP2?.AddSkill2CoolTimeChange(ChangeSkill2_P2);
			characterLevelP2?.AddChangeLevelEvent(ChangeSkill1_P2);
			characterLevelP2?.AddChangeLevelEvent(ChangeSkill2_P2);
			characterLevelP2?.AddChangeLevelEvent(ChangeSkill3_P2);

			ChangeSkill1_P2();
			ChangeSkill2_P2();
			ChangeSkill3_P2();
		}

		//SkillImage

		skillImagesP1[0].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_SkillImage1");
		skillImagesP1[1].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_SkillImage2");
		skillImagesP1[2].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_AllStarSkillImage");

		skillImagesP2[0].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_SkillImage1");
		skillImagesP2[1].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_SkillImage2");
		skillImagesP2[2].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_AllStarSkillImage");


	}

	private void ChangeSkill1_P1()
	{
		if(characterLevelP1.Level > 1)
		{
			if(characterSkillP1.Skill1RemainCoolTime > 0f)
			{
				skillCoolTimeTextP1[0].gameObject.SetActive(true);
				skillHideImagesP1[0].gameObject.SetActive(true);
				skillHideImagesP1[0].fillAmount = 1f - characterSkillP1.Skill1CoolTimeRatio;
				skillCoolTimeTextP1[0].text = $"{(int)characterSkillP1.Skill1RemainCoolTime}";
			}
			else
			{
				skillCoolTimeTextP1[0].gameObject.SetActive(false);
			}
			skillImagesP1[0].gameObject.SetActive(true);
		}
		else
		{
			skillImagesP1[0].gameObject.SetActive(false);
		}
	}
	private void ChangeSkill2_P1()
	{
		if (characterLevelP1.Level > 2)
		{
			if (characterSkillP1.Skill2RemainCoolTime > 0f)
			{
				skillCoolTimeTextP1[1].gameObject.SetActive(true);
				skillHideImagesP1[1].gameObject.SetActive(true);
				skillHideImagesP1[1].fillAmount = 1f - characterSkillP1.Skill2CoolTimeRatio;
				skillCoolTimeTextP1[1].text = $"{(int)characterSkillP1.Skill2RemainCoolTime}";
			}
			else
			{
				skillCoolTimeTextP1[1].gameObject.SetActive(false);
			}
			skillImagesP1[1].gameObject.SetActive(true);
		}
		else
		{
			skillImagesP1[1].gameObject.SetActive(false);
		}
	}
	private void ChangeSkill3_P1()
	{
		if (characterLevelP1.Level > 3)
		{
			skillImagesP1[2].gameObject.SetActive(true);
		}
		else
		{
			skillImagesP1[2].gameObject.SetActive(false);
		}
	}

	private void ChangeSkill1_P2()
	{
		if (characterLevelP2.Level > 1)
		{
			if (characterSkillP2.Skill1RemainCoolTime > 0f)
			{
				skillCoolTimeTextP2[0].gameObject.SetActive(true);
				skillHideImagesP2[0].gameObject.SetActive(true);
				skillHideImagesP2[0].fillAmount = 1f - characterSkillP2.Skill1CoolTimeRatio;
				skillCoolTimeTextP2[0].text = $"{(int)characterSkillP2.Skill1RemainCoolTime}";
			}
			else
			{
				skillCoolTimeTextP2[0].gameObject.SetActive(false);
			}
			skillImagesP2[0].gameObject.SetActive(true);
		}
		else
		{
			skillImagesP2[0].gameObject.SetActive(false);
		}
	}
	private void ChangeSkill2_P2()
	{
		if (characterLevelP2.Level > 2)
		{
			if (characterSkillP2.Skill2RemainCoolTime > 0f)
			{
				skillCoolTimeTextP2[1].gameObject.SetActive(true);
				skillHideImagesP2[1].gameObject.SetActive(true);
				skillHideImagesP2[1].fillAmount = 1f - characterSkillP2.Skill2CoolTimeRatio;
				skillCoolTimeTextP2[1].text = $"{(int)characterSkillP2.Skill2RemainCoolTime}";
			}
			else
			{
				skillCoolTimeTextP2[1].gameObject.SetActive(false);
			}
			skillImagesP2[1].gameObject.SetActive(true);
		}
		else
		{
			skillImagesP2[1].gameObject.SetActive(false);
		}
	}
	private void ChangeSkill3_P2()
	{
		if (characterLevelP2.Level > 3)
		{
			skillImagesP2[2].gameObject.SetActive(true);
		}
		else
		{
			skillImagesP2[2].gameObject.SetActive(false);
		}
	}
}
