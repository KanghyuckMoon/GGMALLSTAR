using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Loading;
using DG.Tweening;

public class ArcadeManager : MonoBehaviour
{
	[SerializeField] private Image characterImage;
	[SerializeField] private RectTransform arrowImage;
	[SerializeField] private RectTransform[] checkImages;
	[SerializeField] private GameObject clearPanel;
	[SerializeField] private GameObject mainButton;

	private int clearRewardCount = 3;

	private void Start()
	{
		SceneManager.LoadScene("ItemPopUp", LoadSceneMode.Additive);

		var vec = arrowImage.anchoredPosition;
		vec.x = checkImages[SelectDataSO.winCount].anchoredPosition.x;
		arrowImage.anchoredPosition = vec;
		characterImage.sprite = Addressable.AddressablesManager.Instance.GetResource<Sprite>($"{SelectDataSO.characterSelectP1.ToString()}_CImage");


		if (SelectDataSO.winCount == 5)
		{
			OpenClearPanel();
		}
		else
		{
			arrowImage.DOAnchorPosX(checkImages[SelectDataSO.winCount + 1].anchoredPosition.x , 1f).OnComplete(() => 
			{
				int characterRandom = Random.Range(1, 3);
				SelectDataSO.characterSelectP2 = (CharacterSelect)characterRandom;
				switch (Random.Range(0, 4))
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
				}

			}).SetDelay(1f);
		}


	}

	private void OpenClearPanel()
	{
		clearRewardCount = 3;
		clearPanel.gameObject.SetActive(true);
	}

	public void OpenItemBox()
	{
		clearRewardCount--;
		Inventory.InventoryManager.Instance.RandomGetItem();
		if(clearRewardCount == 0)
		{
			mainButton.SetActive(true);
		}
	}

}
