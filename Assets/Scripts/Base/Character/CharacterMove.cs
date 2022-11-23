using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterMove : CharacterComponent
{
    private const string _walkSound = "se_common_step_carpet";
    private float _effDelay = 1f;
    private bool _isEffInput = false;
    private CharacterAnimation characterAnimation = null;

    public CharacterMove(Character character) : base(character)
    {
        Utill.StaticCoroutine.Instance.StartCoroutine(PlayWalkEFF());


        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _inputDirection.x = -1;
            _effDelay = 0f;
            _isEffInput = true;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _inputDirection.x = 1;
            _effDelay = 0f;
            _isEffInput = true;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _inputDirection.x = -1;
            _isEffInput = true;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _inputDirection.x = 1;
            _isEffInput = true;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _inputDirection.x = 0;
            _isEffInput = false;
        }, EventType.KEY_UP);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _inputDirection.x = 0;
            _isEffInput = false;
        }, EventType.KEY_UP);
    }

    protected override void Awake()
    {
        _rigidbody = Character.Rigidbody;
        _transform = Character.transform;
        characterAnimation = Character.GetCharacterComponent<CharacterAnimation>();
    }

    private Transform _transform = null;
    private Rigidbody _rigidbody = null;
    private Vector2 _inputDirection = Vector2.zero;
    private Vector2 _moveDirection = Vector2.zero;

    public override void FixedUpdate()
    {
        if (_rigidbody.velocity.y == 0f)
		{
            _moveDirection = _inputDirection;
            if(_inputDirection.x != 0)
			{
                characterAnimation.SetAnimationBool(AnimationType.Run, true);
			}
            else
            {
                characterAnimation.SetAnimationBool(AnimationType.Run, false);
            }
        }
        else
        {
            characterAnimation.SetAnimationBool(AnimationType.Run, false);
        }

        Vector3 pos = Character.transform.position + Character.Collider.center;
        pos.x += (Character.Collider.size.x * _moveDirection.x);
        if (Physics.Raycast(pos, _moveDirection, 0.1f, LayerMask.GetMask("Wall", "Player")))
        {
            _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
        }
		else
        {
            _rigidbody.velocity = new Vector3(_moveDirection.x * Character.CharacterSO.MoveSpeed, _rigidbody.velocity.y, 0);
        }

        _inputDirection = Vector2.zero;
    }

    private IEnumerator PlayWalkEFF()
	{
        while (true)
		{
            _effDelay -= Time.deltaTime;

            if (_rigidbody?.velocity.y == 0f && _effDelay <= 0f && _isEffInput)
			{
                _effDelay = 0.3f;
                Sound.SoundManager.Instance.PlayEFF(_walkSound);
                Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Dirty_01, _character.transform.position);
            }
            yield return null;
		}
	}
}
