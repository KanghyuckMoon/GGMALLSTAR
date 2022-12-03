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

    [CreateAssetMenu]
    public static class InventoryStaticSO
    {
        public static List<ItemDataSO> itemDatas = new List<ItemDataSO>();
    }



}