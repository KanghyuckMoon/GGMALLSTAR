using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Addressable;
using TMPro;
using DG.Tweening;

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

	[SerializeField] private RectTransform allStarSkillCutObjP1;
	[SerializeField] private RectTransform allStarSkillCutObjP2;
	[SerializeField] private Image allStarSkillCutP1;
	[SerializeField] private Image allStarSkillCutP2;
	private Vector3 originAllStarSkillCutPosP1;
	private Vector3 originAllStarSkillCutPosP2;


	private CharacterDodge characterDodgeP1;
	private CharacterDodge characterDodgeP2;
	private CharacterLevel characterLevelP1;
	private CharacterLevel characterLevelP2;
	private CharacterSkill characterSkillP1;
	private CharacterSkill characterSkillP2;

	private bool isNowCanUseSkill1P1;
	private bool isNowCanUseSkill2P1;
	private bool isNowCanUseAllStarSkillP1;
	private bool isNowCanUseDodgeP1;

	private bool isNowCanUseSkill1P2;
	private bool isNowCanUseSkill2P2;
	private bool isNowCanUseAllStarSkillP2;
	private bool isNowCanUseDodgeP2;


	private void Start()
	{
		CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
		var characterP1 = characterSpawner.Player1.GetComponent<Character>();
		var characterP2 = characterSpawner.Player2.GetComponent<Character>();

		characterLevelP1 = characterP1.GetCharacterComponent<CharacterLevel>(ComponentType.Level);
		characterLevelP2 = characterP2.GetCharacterComponent<CharacterLevel>(ComponentType.Level);

		characterSkillP1 = characterP1.GetCharacterComponent<CharacterSkill>(ComponentType.Skill1);
		characterSkillP2 = characterP2.GetCharacterComponent<CharacterSkill>(ComponentType.Skill1);

		characterDodgeP1 = characterP1.GetCharacterComponent<CharacterDodge>(ComponentType.Dodge);
		characterDodgeP2 = characterP2.GetCharacterComponent<CharacterDodge>(ComponentType.Dodge);



		if (characterSkillP1 != null)
		{
			characterSkillP1?.AddSkill1CoolTimeChange(ChangeSkill1_P1);
			characterSkillP1?.AddSkill2CoolTimeChange(ChangeSkill2_P1);
			characterSkillP1?.AddAllStarSkillUse(ChangeSkill3_P1);
			characterSkillP1?.AddAllStarSkillUse(UseAllStarSkillP1);
			characterLevelP1?.AddChangeLevelEvent(ChangeSkill1_P1);
			characterLevelP1?.AddChangeLevelEvent(ChangeSkill2_P1);
			characterLevelP1?.AddChangeLevelEvent(ChangeSkill3_P1);
			characterDodgeP1?.AddChangeDodgeCoolTimeAction(ChangeDodge_P1);

			ChangeSkill1_P1();
			ChangeSkill2_P1();
			ChangeSkill3_P1();
			ChangeDodge_P1();
		}

		if (characterSkillP2 != null)
		{
			characterSkillP2?.AddSkill1CoolTimeChange(ChangeSkill1_P2);
			characterSkillP2?.AddSkill2CoolTimeChange(ChangeSkill2_P2);
			characterSkillP2?.AddAllStarSkillUse(ChangeSkill3_P2);
			characterSkillP2?.AddAllStarSkillUse(UseAllStarSkillP2);
			characterLevelP2?.AddChangeLevelEvent(ChangeSkill1_P2);
			characterLevelP2?.AddChangeLevelEvent(ChangeSkill2_P2);
			characterLevelP2?.AddChangeLevelEvent(ChangeSkill3_P2);
			characterDodgeP2?.AddChangeDodgeCoolTimeAction(ChangeDodge_P2);

			ChangeSkill1_P2();
			ChangeSkill2_P2();
			ChangeSkill3_P2();
			ChangeDodge_P2();
		}

		//SkillImage

		skillImagesP1[0].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_SkillImage1");
		skillImagesP1[1].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_SkillImage2");
		skillImagesP1[2].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_AllStarSkillImage");
		skillImagesP1[3].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_AttackImage");
		skillImagesP1[4].sprite = AddressablesManager.Instance.GetResource<Sprite>($"DodgeImage");
		allStarSkillCutP1.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_AllStarSkillImage");

		skillImagesP2[0].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_SkillImage1");
		skillImagesP2[1].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_SkillImage2");
		skillImagesP2[2].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_AllStarSkillImage");
		skillImagesP2[3].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_AttackImage");
		skillImagesP2[4].sprite = AddressablesManager.Instance.GetResource<Sprite>($"DodgeImage");
		allStarSkillCutP2.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_AllStarSkillImage");

		originAllStarSkillCutPosP1 = allStarSkillCutObjP1.localPosition;
		originAllStarSkillCutPosP2 = allStarSkillCutObjP2.localPosition;

	}

	private void ChangeDodge_P1()
	{
		SetDodgeCoolTimeImage(skillHideImagesP1[4], characterDodgeP1.CoolTime, skillCoolTimeTextP1[4], characterDodgeP1.CoolTimeRatio);
		CheckCanUseSkill(characterDodgeP1.CoolTime, ref isNowCanUseDodgeP1, skillCoolTimeParticleP1[4]);
	}
	private void ChangeDodge_P2()
	{
		SetDodgeCoolTimeImage(skillHideImagesP2[4], characterDodgeP2.CoolTime, skillCoolTimeTextP2[4], characterDodgeP2.CoolTimeRatio);
		CheckCanUseSkill(characterDodgeP2.CoolTime, ref isNowCanUseDodgeP2, skillCoolTimeParticleP2[4]);
	}

	private bool SetDodgeCoolTimeImage(Image image, float remainTime, TextMeshProUGUI text, float ratioCoolTime)
	{
		if (remainTime > 0f)
		{
			text.gameObject.SetActive(true);
			image.gameObject.SetActive(true);
			image.fillAmount = ratioCoolTime;
			text.text = $"{(int)remainTime}";
			return false;
		}
		else
		{
			text.gameObject.SetActive(false);
			return true;
		}
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

	private void UseAllStarSkillP1()
	{
		allStarSkillCutObjP1.gameObject.SetActive(true);
		allStarSkillCutObjP1.DOShakePosition(1f, 6f).OnComplete(() =>
		{
			allStarSkillCutObjP1.localPosition = originAllStarSkillCutPosP1;
			allStarSkillCutObjP1.gameObject.SetActive(false);
		});
	}
	private void UseAllStarSkillP2()
	{
		allStarSkillCutObjP2.gameObject.SetActive(true);
		allStarSkillCutObjP2.DOShakePosition(1f, 6f).OnComplete(() =>
		{
			allStarSkillCutObjP2.localPosition = originAllStarSkillCutPosP2;
			allStarSkillCutObjP2.gameObject.SetActive(false);
		});
	}

	private void ChangeSkill3_P1()
	{
		if (characterLevelP1.Level > 3)
		{
			if (characterLevelP1.IsAllStarSkillUse)
			{
				skillImagesP1[2].sprite = lockImage;
				allStarSkillEffectP1.gameObject.SetActive(false);
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
