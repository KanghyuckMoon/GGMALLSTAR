using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Inventory;

namespace UI.Credit
{
	public class Credit : MonoBehaviour
	{
		[SerializeField, FormerlySerializedAs("creditText")] 
		private TextMeshProUGUI _creditText;
		[SerializeField, FormerlySerializedAs("creditTransform")] 
		private RectTransform _creditTransform;
		[SerializeField, FormerlySerializedAs("posY")] 
		private float _posY = 4000f;
		[SerializeField, FormerlySerializedAs("duration")] 
		private float _duration = 10f;
		[SerializeField, FormerlySerializedAs("creditPrefebs")] 
		private List<GameObject> _creditPrefebs = new List<GameObject>();
		[SerializeField, FormerlySerializedAs("itemDataSOs")] 
		private List<ItemDataSO> _itemDataSOs = new List<ItemDataSO>();

		private IEnumerator Start()
		{
			_creditTransform.DOAnchorPosY(_posY, _duration).SetDelay(3f).OnComplete(() => MoveTitle());

			yield return new WaitForSeconds(1f);

			for (int i = 0; i < _itemDataSOs.Count; ++i)
			{
				if (!InventoryStaticSO.itemDatas.Find(x => x.itemName == _itemDataSOs[i].itemName))
				{
					//아이템 안 가지고 있음
					_creditPrefebs.Find(x => x.name == _itemDataSOs[i].gameName)?.gameObject?.SetActive(false);
					_creditText.text = _creditText.text.Replace(_itemDataSOs[i].gameName, "???");
				}
			}
		}

		private void MoveTitle()
		{
			SceneManager.LoadScene("Title");
		}

	}
}