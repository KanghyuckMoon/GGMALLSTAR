using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Frog : MonoBehaviour
{
    private Character _character = null;
    private Direction _direction = Direction.NONE;
    private HitBoxData _hitBoxData;

    public void SetFireBall(Character character, Direction direction, Vector3 position, HitBoxData hitBoxData)
    {
        _character = character;
        _direction = direction;
        _hitBoxData = hitBoxData;
        transform.position = position;

        if (_hitBoxData.atkEffSoundName != "")
        {
            Sound.SoundManager.Instance.PlayEFF(_hitBoxData.atkEffSoundName);
        }

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            if (_direction == Direction.RIGHT)
            {
                transform.Translate(Vector3.right * 3f * Time.deltaTime);
            }
            else if (_direction == Direction.LEFT)
            {
                transform.Translate(Vector3.left * 3f * Time.deltaTime);
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
    }
}
