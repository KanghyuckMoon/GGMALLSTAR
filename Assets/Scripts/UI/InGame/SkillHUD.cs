using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Addressable;
using TMPro;
using DG.Tweening;

namespace UI.InGame
{
	public class SkillHUD : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("skillImagesP1")]
		private Image[] _skillImagesP1;
		[SerializeField, FormerlySerializedAs("skillImagesP2")]
		private Image[] _skillImagesP2;


		[SerializeField, FormerlySerializedAs("skillHideImagesP1")]
		private Image[] _skillHideImagesP1;
		[SerializeField, FormerlySerializedAs("skillHideImagesP2")]
		private Image[] _skillHideImagesP2;


		[SerializeField, FormerlySerializedAs("allStarSkillEffectP1")]
		private GameObject _allStarSkillEffectP1;
		[SerializeField, FormerlySerializedAs("allStarSkillEffectP2")]
		private GameObject _allStarSkillEffectP2;


		[SerializeField, FormerlySerializedAs("skillCoolTimeParticleP1")]
		private GameObject[] _skillCoolTimeParticleP1;
		[SerializeField, FormerlySerializedAs("skillCoolTimeParticleP2")]
		private GameObject[] _skillCoolTimeParticleP2;


		[SerializeField, FormerlySerializedAs("skillCoolTimeTextP1")]
		private TextMeshProUGUI[] _skillCoolTimeTextP1;
		[SerializeField, FormerlySerializedAs("skillCoolTimeTextP2")]
		private TextMeshProUGUI[] _skillCoolTimeTextP2;


		[SerializeField, FormerlySerializedAs("lockImage")]
		private Sprite _lockImage;


		[SerializeField, FormerlySerializedAs("allStarSkillCutObjP1")]
		private RectTransform _allStarSkillCutObjP1;
		[SerializeField, FormerlySerializedAs("allStarSkillCutObjP2")]
		private RectTransform _allStarSkillCutObjP2;
		[SerializeField, FormerlySerializedAs("allStarSkillCutP1")]
		private Image _allStarSkillCutP1;
		[SerializeField, FormerlySerializedAs("allStarSkillCutP2")]
		private Image _allStarSkillCutP2;
		private Vector3 _originAllStarSkillCutPosP1;
		private Vector3 _originAllStarSkillCutPosP2;


		private CharacterDodge _characterDodgeP1;
		private CharacterDodge _characterDodgeP2;
		private CharacterLevel _characterLevelP1;
		private CharacterLevel _characterLevelP2;
		private CharacterSkill _characterSkillP1;
		private CharacterSkill _characterSkillP2;

		private bool _isNowCanUseSkill1P1;
		private bool _isNowCanUseSkill2P1;
		private bool _isNowCanUseDodgeP1;

		private bool _isNowCanUseSkill1P2;
		private bool _isNowCanUseSkill2P2;
		private bool _isNowCanUseDodgeP2;


		private void Start()
		{
			CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
			var characterP1 = characterSpawner.Player1.GetComponent<Character>();
			var characterP2 = characterSpawner.Player2.GetComponent<Character>();

			_characterLevelP1 = characterP1.GetCharacterComponent<CharacterLevel>(ComponentType.Level);
			_characterLevelP2 = characterP2.GetCharacterComponent<CharacterLevel>(ComponentType.Level);

			_characterSkillP1 = characterP1.GetCharacterComponent<CharacterSkill>(ComponentType.Skill1);
			_characterSkillP2 = characterP2.GetCharacterComponent<CharacterSkill>(ComponentType.Skill1);

			_characterDodgeP1 = characterP1.GetCharacterComponent<CharacterDodge>(ComponentType.Dodge);
			_characterDodgeP2 = characterP2.GetCharacterComponent<CharacterDodge>(ComponentType.Dodge);



			if (_characterSkillP1 != null)
			{
				_characterSkillP1?.AddSkill1CoolTimeChange(ChangeSkill1_P1);
				_characterSkillP1?.AddSkill2CoolTimeChange(ChangeSkill2_P1);
				_characterSkillP1?.AddAllStarSkillUse(ChangeSkill3_P1);
				_characterSkillP1?.AddAllStarSkillUse(UseAllStarSkillP1);
				_characterLevelP1?.AddChangeLevelEvent(ChangeSkill1_P1);
				_characterLevelP1?.AddChangeLevelEvent(ChangeSkill2_P1);
				_characterLevelP1?.AddChangeLevelEvent(ChangeSkill3_P1);
				_characterDodgeP1?.AddChangeDodgeCoolTimeAction(ChangeDodge_P1);

				ChangeSkill1_P1();
				ChangeSkill2_P1();
				ChangeSkill3_P1();
				ChangeDodge_P1();
			}

			if (_characterSkillP2 != null)
			{
				_characterSkillP2?.AddSkill1CoolTimeChange(ChangeSkill1_P2);
				_characterSkillP2?.AddSkill2CoolTimeChange(ChangeSkill2_P2);
				_characterSkillP2?.AddAllStarSkillUse(ChangeSkill3_P2);
				_characterSkillP2?.AddAllStarSkillUse(UseAllStarSkillP2);
				_characterLevelP2?.AddChangeLevelEvent(ChangeSkill1_P2);
				_characterLevelP2?.AddChangeLevelEvent(ChangeSkill2_P2);
				_characterLevelP2?.AddChangeLevelEvent(ChangeSkill3_P2);
				_characterDodgeP2?.AddChangeDodgeCoolTimeAction(ChangeDodge_P2);

				ChangeSkill1_P2();
				ChangeSkill2_P2();
				ChangeSkill3_P2();
				ChangeDodge_P2();
			}

			//SkillImage

			_skillImagesP1[0].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_SkillImage1");
			_skillImagesP1[1].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_SkillImage2");
			_skillImagesP1[2].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_AllStarSkillImage");
			_skillImagesP1[3].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_AttackImage");
			_skillImagesP1[4].sprite = AddressablesManager.Instance.GetResource<Sprite>($"DodgeImage");
			_allStarSkillCutP1.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_AllStarSkillImage");

			_skillImagesP2[0].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_SkillImage1");
			_skillImagesP2[1].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_SkillImage2");
			_skillImagesP2[2].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_AllStarSkillImage");
			_skillImagesP2[3].sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_AttackImage");
			_skillImagesP2[4].sprite = AddressablesManager.Instance.GetResource<Sprite>($"DodgeImage");
			_allStarSkillCutP2.sprite = AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP2.ToString()}_AllStarSkillImage");

			_originAllStarSkillCutPosP1 = _allStarSkillCutObjP1.localPosition;
			_originAllStarSkillCutPosP2 = _allStarSkillCutObjP2.localPosition;

		}

		private void ChangeDodge_P1()
		{
			SetDodgeCoolTimeImage(_skillHideImagesP1[4], _characterDodgeP1.CoolTime, _skillCoolTimeTextP1[4], _characterDodgeP1.CoolTimeRatio);
			CheckCanUseSkill(_characterDodgeP1.CoolTime, ref _isNowCanUseDodgeP1, _skillCoolTimeParticleP1[4]);
		}
		private void ChangeDodge_P2()
		{
			SetDodgeCoolTimeImage(_skillHideImagesP2[4], _characterDodgeP2.CoolTime, _skillCoolTimeTextP2[4], _characterDodgeP2.CoolTimeRatio);
			CheckCanUseSkill(_characterDodgeP2.CoolTime, ref _isNowCanUseDodgeP2, _skillCoolTimeParticleP2[4]);
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
			if (SetCoolTimeAndImage(_characterLevelP1.Level, 1, _characterSkillP1.Skill1RemainCoolTime, _characterSkillP1.Skill1CoolTimeRatio, _skillCoolTimeTextP1[0], _skillHideImagesP1[0], _skillImagesP1[0].gameObject))
			{
				CheckCanUseSkill(_characterSkillP1.Skill1RemainCoolTime, ref _isNowCanUseSkill1P1, _skillCoolTimeParticleP1[0]);
			}
		}

		private void ChangeSkill2_P1()
		{
			if (SetCoolTimeAndImage(_characterLevelP1.Level, 2, _characterSkillP1.Skill2RemainCoolTime, _characterSkillP1.Skill2CoolTimeRatio, _skillCoolTimeTextP1[1], _skillHideImagesP1[1], _skillImagesP1[1].gameObject))
			{
				CheckCanUseSkill(_characterSkillP1.Skill2RemainCoolTime, ref _isNowCanUseSkill2P1, _skillCoolTimeParticleP1[1]);
			}
		}

		private void UseAllStarSkillP1()
		{
			_allStarSkillCutObjP1.gameObject.SetActive(true);
			_allStarSkillCutObjP1.DOShakePosition(1f, 6f).OnComplete(() =>
			{
				_allStarSkillCutObjP1.localPosition = _originAllStarSkillCutPosP1;
				_allStarSkillCutObjP1.gameObject.SetActive(false);
			});
		}
		private void UseAllStarSkillP2()
		{
			_allStarSkillCutObjP2.gameObject.SetActive(true);
			_allStarSkillCutObjP2.DOShakePosition(1f, 6f).OnComplete(() =>
			{
				_allStarSkillCutObjP2.localPosition = _originAllStarSkillCutPosP2;
				_allStarSkillCutObjP2.gameObject.SetActive(false);
			});
		}

		private void ChangeSkill3_P1()
		{
			if (_characterLevelP1.Level > 3)
			{
				if (_characterLevelP1.IsAllStarSkillUse)
				{
					_skillImagesP1[2].sprite = _lockImage;
					_allStarSkillEffectP1.gameObject.SetActive(false);
				}
				else
				{
					_allStarSkillEffectP1.gameObject.SetActive(true);
				}
				_skillImagesP1[2].gameObject.SetActive(true);
			}
			else
			{
				_skillImagesP1[2].gameObject.SetActive(false);
			}
		}

		private void ChangeSkill1_P2()
		{
			if (SetCoolTimeAndImage(_characterLevelP2.Level, 1, _characterSkillP2.Skill1RemainCoolTime, _characterSkillP2.Skill1CoolTimeRatio, _skillCoolTimeTextP2[0], _skillHideImagesP2[0], _skillImagesP2[0].gameObject))
			{
				CheckCanUseSkill(_characterSkillP2.Skill1RemainCoolTime, ref _isNowCanUseSkill1P2, _skillCoolTimeParticleP2[0]);
			}
		}
		private void ChangeSkill2_P2()
		{
			if (SetCoolTimeAndImage(_characterLevelP2.Level, 2, _characterSkillP2.Skill2RemainCoolTime, _characterSkillP2.Skill2CoolTimeRatio, _skillCoolTimeTextP2[1], _skillHideImagesP2[1], _skillImagesP2[1].gameObject))
			{
				CheckCanUseSkill(_characterSkillP2.Skill2RemainCoolTime, ref _isNowCanUseSkill2P2, _skillCoolTimeParticleP2[1]);
			}
		}

		private void ChangeSkill3_P2()
		{
			if (_characterLevelP2.Level > 3)
			{
				if (_characterLevelP2.IsAllStarSkillUse)
				{
					_skillImagesP2[2].sprite = _lockImage;
					_allStarSkillEffectP2.gameObject.SetActive(false);
				}
				else
				{
					_allStarSkillEffectP2.gameObject.SetActive(true);
				}
				_skillImagesP2[2].gameObject.SetActive(true);
			}
			else
			{
				_skillImagesP2[2].gameObject.SetActive(false);
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

}