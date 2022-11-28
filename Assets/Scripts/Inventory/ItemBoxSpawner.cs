using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemBoxObj;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnItem", Random.Range(20f, 50f));
    }

    private void SpawnItem()
	{
        itemBoxObj.gameObject.SetActive(true);
    }
}
