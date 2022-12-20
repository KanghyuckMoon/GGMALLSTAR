using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterMove : CharacterComponent
{
    // �̵� ���� �̸� ����
    private const string _walkSound = "se_common_step_carpet";
    // �̵� ȿ�� ������
    private float _effDelay = 1f;
    // ����Ʈ �Է� bool
    private bool _isEffInput = false;
    // ���� ��?
    private bool _isGround = false;
    // ���� ��? Getter
    public bool IsGround => _isGround;
    // CharacterAnimation ĳ�� ����
    private CharacterAnimation _characterAnimation = null;

    public CharacterMove(Character character) : base(character)
    {
        // Ű �Է� �̺�Ʈ �Ҵ�
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
        // ���� �Ҵ�
        _rigidbody = Character.Rigidbody;
        _transform = Character.transform;
        _characterAnimation = Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
        _characterStat = Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
    }

    public override void Start()
    {
        // �̵��� ����Ʈ �������ִ� �ڸ�ƾ ����
        Character.StartCoroutine(PlayWalkEFF());
    }

    protected override void SetEvent()
    {
        // �Է� �̺�Ʈ ����
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

    // ĳ���� �ɷ�ġ ĳ�� ����
    private CharacterStat _characterStat;
    // ĳ���� Transform ĳ�� ����
    private Transform _transform = null;
    // ĳ���� Rigidbody ĳ�� ����
    private Rigidbody _rigidbody = null;

    // Ű �Է� ����
    private Vector2 _inputDirection = Vector2.zero;
    // Ű �Է� ���� Getter
    public Vector2 InputDirection => _inputDirection;

    // �̵� ����
    private Vector2 _moveDirection = Vector2.zero;
    // ���� �ð�
    private float _sturnTime = 0f;

    // ������ ������?
    private bool _isRight = false;
    // ������ ������? Getter
    public bool IsRight
    {
        get
        {
            return _isRight;
        }
    }

    /// <summary>
    /// ���� �ð� ����
    /// </summary>
    /// <param name="time"></param>
    public void SetStunTime(float time)
    {
        _sturnTime = time;
    }

    /// <summary>
    /// �̵� ���� ����
    /// </summary>
    /// <param name="vector2"></param>
    public void SetMoveDirection(Vector2 vector2)
    {
        _moveDirection = vector2;

    }

    public override void FixedUpdate()
    {
        // ���� üũ
        if (!Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat).IsAlive)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }

        // ���������� üũ
        if (_sturnTime > 0f)
        {
            _sturnTime -= Time.fixedDeltaTime;
            return;
        }

        // �̵��ӵ� ĳ�� ����
        float speed = 0f;

        // y���� ��ȭ�� ���ٸ�
        if (_rigidbody.velocity.y == 0f)
        {
            // �̵� ���� �Է¹����� ����
            SetMoveDirection(_inputDirection);
            // �̵��ӵ� ����
            speed = Character.CharacterSO.MoveSpeed;
            // �Է� �� �ִϸ��̼� ����
            if (_inputDirection.x != 0)
            {
                _characterAnimation.SetAnimationBool(AnimationType.Run, true);
            }
            else
            {
                // �Է��� ���ٸ� �ִϸ��̼� ����
                _characterAnimation.SetAnimationBool(AnimationType.Run, false);
            }
        }
        else
        {
            // �����̸� ���� �ӵ� ����
            speed = Character.CharacterSO.AirMoveSpeed;
            // �����̴ϱ� �ٴ� �ִϸ��̼� ����
            _characterAnimation.SetAnimationBool(AnimationType.Run, false);
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

    /// <summary>
    /// ������ ����Ʈ �����ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// �ݸ��� ���� �ε�������
    /// </summary>
    /// <param name="other"></param>
    public override void OnCollisionEnter(Collision other)
    {
        //Wall Check
        // �¾Ƽ� �ε���?
        if (_sturnTime > 0f)
        {
            // �ε����� ��?
            if (other.gameObject.layer == 8)
            {
                // ĳ���� ������?
                if (_characterStat.IsAlive)
                {
                    //right
                    // �� �ε��� ȿ�� ����
                    if (other.transform.position.x > Character.transform.position.x)
                    {
                        Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Hit_5, other.collider.ClosestPoint(Character.transform.position), Effect.EffectDirectionType.ReverseDirection, new Vector3(0, 0, 180));
                        Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Shockwave, other.collider.ClosestPoint(Character.transform.position), Effect.EffectDirectionType.SetParticleOffsetIs3DRotation, new Vector3(0, 45, 0));
                    }
                    else
                    {
                        Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Hit_5, other.collider.ClosestPoint(Character.transform.position), Effect.EffectDirectionType.ReverseDirection, new Vector3(0, 0, 0));
                        Effect.EffectManager.Instance.SetEffect(Effect.EffectType.Shockwave, other.collider.ClosestPoint(Character.transform.position), Effect.EffectDirectionType.SetParticleOffsetIs3DRotation, new Vector3(0, -45, 0));
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

            if (_characterStat.IsAlive && _rigidbody.velocity.y < 0f)
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
            if (_characterStat.IsAlive)
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
