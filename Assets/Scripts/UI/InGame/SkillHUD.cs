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

	[SerializeField] private GameObject allStarSkillEffectP1;
	[SerializeField] private GameObject allStarSkillEffectP2;

	[SerializeField] private GameObject[] skillCoolTimeParticleP1;
	[SerializeField] private GameObject[] skillCoolTimeParticleP2;

	[SerializeField] private TextMeshProUGUI[] skillCoolTimeTextP1;
	[SerializeField] private TextMeshProUGUI[] skillCoolTimeTextP2;

	[SerializeField] private Sprite lockImage;


	private CharacterLevel characterLevelP1;
	private CharacterLevel characterLevelP2;
	private CharacterSkill characterSkillP1;
	private CharacterSkill characterSkillP2;

	private bool isNowCanUseSkill1P1;
	private bool isNowCanUseSkill2P1;
	private bool isNowCanUseAllStarSkillP1;

	private bool isNowCanUseSkill1P2;
	private bool isNowCanUseSkill2P2;
	private bool isNowCanUseAllStarSkillP2;


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
		if(SetCoolTimeAndImage(characterLevelP1.Level, 1, characterSkillP1.Skill1RemainCoolTime, characterSkillP1.Skill1CoolTimeRatio, skillCoolTimeTextP1[0], skillHideImagesP1[0], skillImagesP1[0].gameObject))
		{
			CheckCanUseSkill(characterSkillP1.Skill1RemainCoolTime, ref isNowCanUseSkill1P1, skillCoolTimeParticleP1[0]);
		}
	}

	private void ChangeSkill2_P1()
	{
		if(SetCoolTimeAndImage(characterLevelP1.Level, 2, characterSkillP1.Skill2RemainCoolTime, characterSkillP1.Skill2CoolTimeRatio, skillCoolTimeTextP1[1], skillHideImagesP1[1], skillImagesP1[1].gameObject))
		{
			CheckCanUseSkill(characterSkillP1.Skill2RemainCoolTime, ref isNowCanUseSkill2P1, skillCoolTimeParticleP1[1]);
		}
	}
	private void ChangeSkill3_P1()
	{
		if (characterLevelP1.Level > 3)
		{
			if (characterLevelP1.IsAllStarSkillUse)
			{
				skillImagesP1[2].sprite = lockImage;
				allStarSkillEffectP1.gameObject.SetActive(true);
			}
			else
			{
				allStarSkillEffectP1.gameObject.SetActive(true);
			}
			skillImagesP1[2].gameObject.SetActive(true);
		}
		else
		{
			skillImagesP1[2].gameObject.SetActive(false);
		}
	}

	private void ChangeSkill1_P2()
	{
		if(SetCoolTimeAndImage(characterLevelP2.Level, 1, characterSkillP2.Skill1RemainCoolTime, characterSkillP2.Skill1CoolTimeRatio, skillCoolTimeTextP2[0], skillHideImagesP2[0], skillImagesP2[0].gameObject))
		{
			CheckCanUseSkill(characterSkillP2.Skill1RemainCoolTime, ref isNowCanUseSkill1P2, skillCoolTimeParticleP2[0]);
		}
	}
	private void ChangeSkill2_P2()
	{
		if(SetCoolTimeAndImage(characterLevelP2.Level, 2, characterSkillP2.Skill2RemainCoolTime, characterSkillP2.Skill2CoolTimeRatio, skillCoolTimeTextP2[1], skillHideImagesP2[1], skillImagesP2[1].gameObject))
		{
			CheckCanUseSkill(characterSkillP2.Skill2RemainCoolTime, ref isNowCanUseSkill2P2, skillCoolTimeParticleP2[1]);
		}
	}

	private void ChangeSkill3_P2()
	{
		if (characterLevelP2.Level > 3)
		{
			if(characterLevelP2.IsAllStarSkillUse)
			{
				skillImagesP2[2].sprite = lockImage;
				allStarSkillEffectP2.gameObject.SetActive(false);
			}
			else
			{
				allStarSkillEffectP2.gameObject.SetActive(true);
			}
			skillImagesP2[2].gameObject.SetActive(true);
		}
		else
		{
			skillImagesP2[2].gameObject.SetActive(false);
		}
	}

	private bool SetCoolTimeAndImage(int level, int needLevel, float remainTime, float ratioCoolTime, TextMeshProUGUI text, Image image, GameObject skill)
	{
		if (level > needLevel)
		{
			if (remainTime > 0f)
			{
				text.gameObject.SetActive(true);
				image.gameObject.SetActive(true);
				image.fillAmount = 1f - ratioCoolTime;
				text.text = $"{(int)remainTime}";
			}
			else
			{
				text.gameObject.SetActive(false);
			}
			skill.SetActive(true);
			return true;
		}
		else
		{
			skill.SetActive(false);
			return false;
		}
	}

	private void CheckCanUseSkill(float remianCoolTime, ref bool isCanUse, GameObject effect)
	{
		if (remianCoolTime > 0f)
		{
			isCanUse = false;
		}
		else
		{
			if (!isCanUse)
			{
				isCanUse = true;
				effect.SetActive(true);
				Sound.SoundManager.Instance.PlayEFF("se_common_offset");
			}
		}
	}

}
