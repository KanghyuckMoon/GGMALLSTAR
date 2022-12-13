using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterMove : CharacterComponent
{
    private const string _walkSound = "se_common_step_carpet";
    private float _effDelay = 1f;
    private bool _isEffInput = false;
    private bool _isGround = false;
    public bool IsGround => _isGround;
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
        characterStat = Character.GetCharacterComponent<CharacterStat>();
    }

    protected override void SetEvent()
    {
        base.SetEvent();
        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _isRight = false;
        }, EventType.KEY_DOWN);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _isRight = true;
        }, EventType.KEY_DOWN);
        CharacterEvent.AddEvent(EventKeyWord.LEFT, () =>
        {
            _isRight = false;
        }, EventType.KEY_HOLD);

        CharacterEvent.AddEvent(EventKeyWord.RIGHT, () =>
        {
            _isRight = true;
        }, EventType.KEY_HOLD);
    }

    private CharacterStat characterStat;
    private Transform _transform = null;
    private Rigidbody _rigidbody = null;

    private Vector2 _inputDirection = Vector2.zero;
    public Vector2 InputDirection => _inputDirection;

    private Vector2 _moveDirection = Vector2.zero;
    private float _sturnTime = 0f;

    private bool _isRight = false;
    public bool IsRight
    {
        get
        {
            return _isRight;
        }
    }

    public void SetSturnTime(float time)
    {
        _sturnTime = time;
    }

    public void SetMoveDirection(Vector2 vector2)
    {
        _moveDirection = vector2;

    }

    public override void FixedUpdate()
    {
        if (!Character.GetCharacterComponent<CharacterStat>().IsAlive)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }

        if (_sturnTime > 0f)
        {
            _sturnTime -= Time.fixedDeltaTime;
            return;
        }

        float speed = 0f;

        if (_rigidbody.velocity.y == 0f)
        {
            SetMoveDirection(_inputDirection);
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

        //Wall Check
        Vector3 pos = Character.transform.position + Character.Collider.center;
        pos.x += Character.Collider.size.x * 0.5f * _moveDirection.x;
        pos.y -= Character.Collider.size.y * 0.5f;
        pos.y += 0.1f;
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

            try
            {
                if (_rigidbody?.velocity.y == 0f && _effDelay <= 0f && _isEffInput)
                {
                    _effDelay = 0.3f;
                    Sound.SoundManager.Instance.PlayEFF(_walkSound);
                    Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Dirty_01, _character.transform.position);
                }
            }
            catch
            {
                yield break;
            }

            yield return null;
        }
    }

    public override void OnCollisionEnter(Collision other)
    {
        //Wall Check
        if (_sturnTime > 0f)
        {
            if (other.gameObject.layer == 8)
            {
                if (characterStat.IsAlive)
                {
                    //right
                    if (other.transform.position.x > Character.transform.position.x)
                    {
                        Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Hit_5, other.collider.ClosestPoint(Character.transform.position), Effect.EffectDirectionType.ReverseDirection, new Vector3(0, 0, 180));
                    }
                    else
                    {
                        Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Hit_5, other.collider.ClosestPoint(Character.transform.position), Effect.EffectDirectionType.ReverseDirection, new Vector3(0, 0, 0));
                    }
                }
            }
        }
    }

    public override void OnCollisionStay(Collision other)
    {
        //Ground Check
        if (other.gameObject.layer == 6)
        {
            _isGround = true;

            if (characterStat.IsAlive && _rigidbody.velocity.y < 0f)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, 0);
            }
        }
        else
        {
            _isGround = false;
        }

        //Wall Check
        if (other.gameObject.layer == 8)
        {
            if (characterStat.IsAlive)
            {
                if (other.transform.position.x > Character.transform.position.x)
                {
                    _rigidbody.velocity = new Vector3(-1f, _rigidbody.velocity.y, 0);
                }
                else
                {
                    _rigidbody.velocity = new Vector3(1f, _rigidbody.velocity.y, 0);
                }
            }
        }
    }
}
