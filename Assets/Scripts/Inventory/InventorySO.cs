using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inventory
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        public List<ItemDataSO> itemDatas;
    }

}