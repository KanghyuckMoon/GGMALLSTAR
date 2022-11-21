using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterMove : CharacterComponent
{
    private const string _walkSound = "se_common_step_carpet";
    private float _effDelay = 1f;
    private bool _isEffInput = false;

    public CharacterMove(Character character, float speed = 7.5f) : base(character)
    {
        _speed = speed;
        Utill.StaticCoroutine.Instance.StartCoroutine(PlayWalkEFF());


        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _moveDirection.x = -1;
            _effDelay = 0f;
            _isEffInput = true;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _moveDirection.x = 1;
            _effDelay = 0f;
            _isEffInput = true;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _moveDirection.x = -1;
            _isEffInput = true;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _moveDirection.x = 1;
            _isEffInput = true;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _moveDirection.x = 0;
            _isEffInput = false;
        }, EventType.KEY_UP);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _moveDirection.x = 0;
            _isEffInput = false;
        }, EventType.KEY_UP);
    }

    protected override void Awake()
    {
        _rigidbody = Character.Rigidbody;
        _transform = Character.transform;
    }

    private Transform _transform = null;
    private Rigidbody _rigidbody = null;
    private float _speed = 7.5f;
    private Vector2 _moveDirection = Vector2.zero;

    public override void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_moveDirection.x * _speed, 0, 0);
        _moveDirection = Vector2.zero;
    }

    private IEnumerator PlayWalkEFF()
	{
        while (true)
		{
            _effDelay -= Time.deltaTime;

            if (_rigidbody.velocity.y == 0f && _effDelay <= 0f && _isEffInput)
			{
                _effDelay = 0.3f;
                Sound.SoundManager.Instance.PlayEFF(_walkSound);
                Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Dirty_01, _character.transform.position);
            }
            yield return null;
		}
	}
}
