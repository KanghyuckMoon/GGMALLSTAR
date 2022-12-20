using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Loading;
using Addressable;
using Sound;
using DG.Tweening;

namespace Arcade
{
	public class ArcadeManager : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("characterImage")] 
		private Image _characterImage;
		[SerializeField, FormerlySerializedAs("arrowImage")] 
		private RectTransform _arrowImage;
		[SerializeField, FormerlySerializedAs("checkImages")] 
		private RectTransform[] _checkImages;
		[SerializeField, FormerlySerializedAs("clearPanel")] 
		private GameObject _clearPanel;
		[SerializeField, FormerlySerializedAs("endingButton")] 
		private GameObject _endingButton;

		private int _clearRewardCount = 3;

		/// <summary>
		/// 게임에서 승리시 아이템 박스를 3개 지급하고 3개 다 누르면 다음 게임으로 이동
		/// </summary>
		public void OpenItemBox()
		{
			_clearRewardCount--;
			Sound.SoundManager.Instance.PlayEFF("se_item_genesis_get");
			Inventory.InventoryManager.Instance.RandomGetItem();


			if (_clearRewardCount == 0)
			{
				//Next Battle
				if (SelectDataSO.winCount == 5)
				{
					_endingButton.SetActive(true);

				}
				else
				{
					_clearPanel.gameObject.SetActive(false);
					GotoNextBattle();
				}
			}
		}
		private void Start()
		{
			SelectDataSO.isAICharacterP1 = false;
			SelectDataSO.isAICharacterP2 = true;

			//LoadItemPopUpScene
			SceneManager.LoadScene("ItemPopUp", LoadSceneMode.Additive);

			//Set Character Image and arrow image
			var vec = _arrowImage.anchoredPosition;
			vec.x = _checkImages[SelectDataSO.winCount].anchoredPosition.x;
			_arrowImage.anchoredPosition = vec;
			_characterImage.sprite = Addressable.AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_CImage");

			//Victory
			if (SelectDataSO.winCount > SelectDataSO.priviousWinCount)
			{
				OpenClearPanel();
			}
			else //Continue
			{
				SelectDataSO.aiLevelP2 = SelectDataSO.winCount + 1;
				GotoNextBattle();
			}

		}

		private void OpenClearPanel()
		{
			SelectDataSO.priviousWinCount = SelectDataSO.winCount;
			_clearRewardCount = 3;
			_clearPanel.gameObject.SetActive(true);
			Sound.SoundManager.Instance.PlayEFF("vc_narration_result_victory");
		}

		private void GotoNextBattle()
		{
			SelectDataSO.aiLevelP2 = SelectDataSO.winCount + 1;
			_arrowImage.DOAnchorPosX(_checkImages[SelectDataSO.winCount + 1].anchoredPosition.x, 1f).OnComplete(() =>
			{
				int characterRandom = Random.Range(1, 7);
				SelectDataSO.characterSelectP2 = (CharacterSelect)characterRandom;
				switch (Random.Range(0, 7))
				{
					default:
					case 0:
						Loading.LoadingScene.Instance.LoadScene("Training");
						break;
					case 1:
						Loading.LoadingScene.Instance.LoadScene("Well");
						break;
					case 2:
						Loading.LoadingScene.Instance.LoadScene("Tower");
						break;
					case 3:
						Loading.LoadingScene.Instance.LoadScene("QuietTown");
						break;
					case 4:
						Loading.LoadingScene.Instance.LoadScene("LostKingdom");
						break;
					case 5:
						Loading.LoadingScene.Instance.LoadScene("AgentStage");
						break;
					case 6:
						Loading.LoadingScene.Instance.LoadScene("Bridge");
						break;
				}

			}).SetDelay(1f);
		}

	}

}
