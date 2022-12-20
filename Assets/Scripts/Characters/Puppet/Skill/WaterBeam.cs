using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBeam : Skill
{
    [SerializeField]
    private float _lifeTime = 0.5f;

    private Character _character = null;
    private Direction _direction = Direction.RIGHT;

    private HitBoxData _hitBoxData = null;

    private Animator _animator = null;

    public void SetWaterBeam(Character character, HitBoxData hitBoxData, Vector3 position, Direction direction)
    {
        transform.position = position;
        _character = character;
        _direction = direction;
        _hitBoxData = hitBoxData;

        GetComponent<Collider>().enabled = false;

        if (_direction == Direction.LEFT)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_direction == Direction.RIGHT)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_character != null && other.gameObject == _character.gameObject)
        {
            return;
        }

        if (!other.gameObject.CompareTag(_character.tag) && !other.gameObject.CompareTag("Invincibility") && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2")))
        {
            CharacterAttack characterAttack = _character.GetCharacterComponent<CharacterAttack>(ComponentType.Attack);
            characterAttack.TargetCharacterDamage = other?.gameObject?.GetComponent<Character>()?.GetCharacterComponent<CharacterDamage>(ComponentType.Damage);
            characterAttack.TargetCharacterDamage?.OnAttacked(null, _hitBoxData, other.ClosestPoint(transform.position), characterAttack.IsRight);

            //Exp
            int expCount = (_hitBoxData.addExp / 5) + 1;

            for (int i = 0; i < expCount; ++i)
            {
                StarEffect starEffect = Pool.PoolManager.GetItem("StarEff").GetComponent<StarEffect>();
                starEffect.SetEffect(transform.position, _character.GetCharacterComponent<CharacterLevel>(ComponentType.Level), _hitBoxData.addExp / expCount);
            }
        }
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
        {
            _animator.SetTrigger("End");
        }
    }

    private void OnDisable()
    {
        Pool.PoolManager.AddObjToPool("Assets/Prefabs/WaterBeam.prefab", gameObject);
    }
}
