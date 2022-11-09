using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
	public class PoolTest : MonoBehaviour
	{
		[ContextMenu("GetObj")]
		public void GetObj()
		{
			PoolManager.GetItem<PoolObj>("PoolObj");
		}
	}

}