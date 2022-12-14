using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBeam : Skill
{
    [SerializeField]
    private float _lifeTime = 0.5f;

    private Character _character = null;
    private Direction _direction = Direction.RIGHT;

    private Animator _animator = null;

    public void SetWaterBeam(Character character, Vector3 position, Direction direction)
    {
        transform.position = position;
        _character = character;
        _direction = direction;

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

    }
}
