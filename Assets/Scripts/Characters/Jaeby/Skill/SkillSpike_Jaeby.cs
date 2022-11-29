using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpike_Jaeby : MonoBehaviour
{
    public void SetSkillSpike(Vector3 position)
    {
        transform.position = position;
        GetComponent<Collider>().enabled = false;
        StartCoroutine(ColliderTriggerCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/SkillSpike_Jaeby.prefab", gameObject);
    }

    private void OnEnable()
    {
        StartCoroutine(ColliderTriggerCoroutine());
    }

    private void OnDisable()
    {
        GetComponent<Collider>().enabled = false;
    }

    private IEnumerator ColliderTriggerCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Collider>().enabled = true;
    }
}