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
    private float _sturnTime = 0f;

    public void SetSturnTime(float time)
	{
        _sturnTime = time;
	}

    public override void FixedUpdate()
    {
        if (_sturnTime > 0f)
        {
            _sturnTime -= Time.fixedDeltaTime;
            return;
		}

        float speed = 0f;
        if (_rigidbody.velocity.y == 0f)
		{
            _moveDirection = _inputDirection;
            speed = Character.CharacterSO.MoveSpeed;
            if (_inputDirection.x != 0)
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
            speed = Character.CharacterSO.AirMoveSpeed;
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
            Vector3 vel = _rigidbody.velocity;
            vel.x = Mathf.Lerp(vel.x, _moveDirection.x * speed, Time.fixedDeltaTime * 10);
            _rigidbody.velocity = vel;
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

	public override void OnCollisionStay(Collision other)
	{
		base.OnCollisionStay(other);

        //if(other.collider.gameObject.CompareTag("Player") || other.collider.gameObject.CompareTag("Player2"))
        //{
        //    if (other.transform.position.y > Character.transform.position.y)
        //    {
        //        if (other.transform.position.x < Character.transform.position.x)
        //        {
        //            _rigidbody.AddForce(Vector3.left * 10, ForceMode.Impulse);
        //        }
        //        else
        //        {
        //            _rigidbody.AddForce(Vector3.right * 10, ForceMode.Impulse);
        //        }
        //    }
        //
        //}
    }
}
