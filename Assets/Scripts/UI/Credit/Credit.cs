using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Inventory;

public class Credit : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI creditText;
	[SerializeField] private RectTransform creditTransform;
	[SerializeField] private float posY = 4000f;
	[SerializeField] private float duration = 10f;
	[SerializeField] private List<GameObject> creditPrefebs = new List<GameObject>();
	[SerializeField] private List<ItemDataSO> itemDataSOs = new List<ItemDataSO>();

	private IEnumerator Start()
	{
		creditTransform.DOAnchorPosY(posY, duration).SetDelay(3f).OnComplete(() => MoveTitle());

		yield return new WaitForSeconds(1f);

		for(int i = 0; i < itemDataSOs.Count; ++i)
		{
			if(!InventoryStaticSO.itemDatas.Find(x => x.itemName == itemDataSOs[i].itemName))
			{
				//아이템 안 가지고 있음
				creditPrefebs.Find(x => x.name == itemDataSOs[i].gameName)?.gameObject?.SetActive(false);
				creditText.text = creditText.text.Replace(itemDataSOs[i].gameName, "???");
			}
		}
	}

	private void MoveTitle()
	{
		SceneManager.LoadScene("Title");
	}

}