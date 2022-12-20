using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;

public class CharacterMove : CharacterComponent
{
    // 이동 사운드 이름 저장
    private const string _walkSound = "se_common_step_carpet";
    // 이동 효과 딜레이
    private float _effDelay = 1f;
    // 이펙트 입력 bool
    private bool _isEffInput = false;
    // 지금 땅?
    private bool _isGround = false;
    // 지금 땅? Getter
    public bool IsGround => _isGround;
    // CharacterAnimation 캐싱 변수
    private CharacterAnimation _characterAnimation = null;

    public CharacterMove(Character character) : base(character)
    {
        // 키 입력 이벤트 할당
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
        // 변수 할당
        _rigidbody = Character.Rigidbody;
        _transform = Character.transform;
        _characterAnimation = Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
        _characterStat = Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
    }

    public override void Start()
    {
        // 이동시 이펙트 생성해주는 코르틴 실행
        Character.StartCoroutine(PlayWalkEFF());
    }

    protected override void SetEvent()
    {
        // 입력 이벤트 설정
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

    // 캐릭터 능력치 캐싱 변수
    private CharacterStat _characterStat;
    // 캐릭터 Transform 캐싱 변수
    private Transform _transform = null;
    // 캐릭터 Rigidbody 캐싱 변수
    private Rigidbody _rigidbody = null;

    // 키 입력 방향
    private Vector2 _inputDirection = Vector2.zero;
    // 키 입력 방향 Getter
    public Vector2 InputDirection => _inputDirection;

    // 이동 방향
    private Vector2 _moveDirection = Vector2.zero;
    // 기절 시간
    private float _sturnTime = 0f;

    // 오른쪽 보는중?
    private bool _isRight = false;
    // 오른쪽 보는중? Getter
    public bool IsRight
    {
        get
        {
            return _isRight;
        }
    }

    /// <summary>
    /// 기절 시간 설정
    /// </summary>
    /// <param name="time"></param>
    public void SetStunTime(float time)
    {
        _sturnTime = time;
    }

    /// <summary>
    /// 이동 방향 설정
    /// </summary>
    /// <param name="vector2"></param>
    public void SetMoveDirection(Vector2 vector2)
    {
        _moveDirection = vector2;

    }

    public override void FixedUpdate()
    {
        // 생존 체크
        if (!Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat).IsAlive)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }

        // 스턴중인지 체크
        if (_sturnTime > 0f)
        {
            _sturnTime -= Time.fixedDeltaTime;
            return;
        }

        // 이동속도 캐싱 변수
        float speed = 0f;

        // y값의 변화가 없다면
        if (_rigidbody.velocity.y == 0f)
        {
            // 이동 방향 입력방향대로 설정
            SetMoveDirection(_inputDirection);
            // 이동속도 설정
            speed = Character.CharacterSO.MoveSpeed;
            // 입력 시 애니메이션 실행
            if (_inputDirection.x != 0)
            {
                _characterAnimation.SetAnimationBool(AnimationType.Run, true);
            }
            else
            {
                // 입력이 없다면 애니메이션 정지
                _characterAnimation.SetAnimationBool(AnimationType.Run, false);
            }
        }
        else
        {
            // 공중이면 공중 속도 적용
            speed = Character.CharacterSO.AirMoveSpeed;
            // 공중이니까 뛰는 애니메이션 정지
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
    /// 걸을때 이펙트 설정하는 코루틴
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
    /// 콜리전 끼리 부딪혔을때
    /// </summary>
    /// <param name="other"></param>
    public override void OnCollisionEnter(Collision other)
    {
        //Wall Check
        // 맞아서 부딪힘?
        if (_sturnTime > 0f)
        {
            // 부딪힌게 벽?
            if (other.gameObject.layer == 8)
            {
                // 캐릭터 안죽음?
                if (_characterStat.IsAlive)
                {
                    //right
                    // 벽 부딪힘 효과 적용
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
