using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Jaeby : MonoBehaviour
{
    private Character _character = null;
    private Direction _direction = Direction.NONE;

    public void SetSkillProjectile(Character character, Direction direction)
    {
        _character = character;
        _direction = direction;
        transform.position = character.transform.position;
        StartCoroutine(Move());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (_character.gameObject.CompareTag(other.gameObject.tag))
        {
            return;
        }
        gameObject.SetActive(false);
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/SkillProjectile_Jaeby.prefab", gameObject);
    }

    private IEnumerator Move()
    {
        while (true)
        {
            switch (_direction)
            {
                case Direction.LEFT:
                    transform.Translate(Vector3.left * 0.1f);
                    break;
                case Direction.RIGHT:
                    transform.Translate(Vector3.right * 0.1f);
                    break;
            }
            yield return null;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SetActiveCoroutine());
    }

    private void OnDisable()
    {
        _character = null;
        _direction = Direction.NONE;
    }

    private IEnumerator SetActiveCoroutine(bool active = false)
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(active);
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/SkillProjectile_Jaeby.prefab", gameObject);
    }
}
