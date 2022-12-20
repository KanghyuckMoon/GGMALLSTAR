using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace UI.InGame
{
	public class HPHUD : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("hpBarIamgeP1")] 
		private Image _hpBarIamgeP1;
		[SerializeField, FormerlySerializedAs("hpBarIamgeP2")] 
		private Image _hpBarIamgeP2;
		[SerializeField, FormerlySerializedAs("characterImageP1")] 
		private RectTransform _characterImageP1;
		[SerializeField, FormerlySerializedAs("characterImageP2")] 
		private RectTransform _characterImageP2;

		private CharacterStat _characterStatP1;
		private CharacterStat _characterStatP2;

		private Vector3 _originPosP1;
		private Vector3 _originPosP2;

		private float _previousHPP1;
		private float _previousHPP2;


		private void Start()
		{
			CharacterSpawner characterSpawner = FindObjectOfType<CharacterSpawner>();
			var characterP1 = characterSpawner.Player1.GetComponent<Character>();
			var characterP2 = characterSpawner.Player2.GetComponent<Character>();

			_characterStatP1 = characterP1.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
			_characterStatP2 = characterP2.GetCharacterComponent<CharacterStat>(ComponentType.Stat);


			_characterStatP1.AddHPEvent(ChangeHPP1);
			_characterStatP2.AddHPEvent(ChangeHPP2);

			_previousHPP1 = _characterStatP1.HP;
			_previousHPP2 = _characterStatP2.HP;

			_originPosP1 = _characterImageP1.localPosition;
			_originPosP2 = _characterImageP2.localPosition;

			ChangeHPP1();
			ChangeHPP2();
		}

		private void ChangeHPP1()
		{
			//hpTextP1.text = $"{characterStatP1.HP}";
			_hpBarIamgeP1.fillAmount = _characterStatP1.HP / _characterStatP1.MaxHP;

			if (_previousHPP1 > _characterStatP1.HP)
			{
				_characterImageP1.DOKill();
				_characterImageP1.DOShakePosition(0.2f, 5).OnComplete(() =>
				{
					_characterImageP1.localPosition = _originPosP1;
				});
			}

			_previousHPP1 = _characterStatP1.HP;
		}
		private void ChangeHPP2()
		{
			//hpTextP2.text = $"{characterStatP2.HP}";
			_hpBarIamgeP2.fillAmount = _characterStatP2.HP / _characterStatP2.MaxHP;

			if (_previousHPP2 > _characterStatP2.HP)
			{
				_characterImageP2.DOKill();
				_characterImageP2.DOShakePosition(0.2f, 5).OnComplete(() =>
				{
					_characterImageP2.localPosition = _originPosP2;
				});
			}

			_previousHPP2 = _characterStatP2.HP;
		}

	}

}