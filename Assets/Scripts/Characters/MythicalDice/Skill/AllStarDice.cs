using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllStarDice : MonoBehaviour
{
    private float speed = 100.0f;
    private Vector3 rotation = Vector3.zero;

    public void SetDeleteTime(float time)
	{
        StartCoroutine(Delete(time));
	}

    private IEnumerator Delete(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        Pool.PoolManager.AddObjToPool("AllStarDice", gameObject);
	}

    private void Update()
    {
        rotation.y += Time.deltaTime * speed;
        rotation.z += Time.deltaTime * speed;
        rotation.x += Time.deltaTime * speed;
        transform.eulerAngles = rotation;
    }
}
