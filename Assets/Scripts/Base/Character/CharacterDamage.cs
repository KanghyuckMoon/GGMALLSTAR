using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyWord;
using Effect;
using Sound;
using Utill;
using DG.Tweening;

public class CharacterDamage : CharacterComponent
{
    public CharacterDamage(Character character) : base(character)
    {

    }

    protected override void Awake()
    {
        _hp = Character.GetCharacterComponent<CharacterStat>().MaxHP;
    }

    protected override void SetEvent()
    {
        CharacterEvent.AddEvent(EventKeyWord.DAMAGE, OnDamage, EventType.DEFAULT);
    }

    private float _hp = 0;

    private void OnDamage()
    {
        _hp -= 10;
        if (_hp <= 0)
        {
            Debug.Log("Die");
        }
    }

    public void OnAttcked(HitBoxData hitBoxData, Vector3 collistionPoint, bool isRight)
    {
        _hp -= hitBoxData.damage;
        EffectManager.Instance.SetEffect(hitBoxData.effectType, collistionPoint);
        SoundManager.Instance.PlayEFF(hitBoxData.effSoundName);
        Vector3 vector = Character.Rigidbody.velocity;
        CharacterGravity characterGravity = Character.GetCharacterComponent<CharacterGravity>();
        characterGravity.SetHitTime(hitBoxData.hitTime);
        Character.Rigidbody.velocity = Vector3.zero;

        CharacterInput characterInput = Character.GetCharacterComponent<CharacterInput>();
        if(characterInput is not null)
		{
            characterInput.SetStunTime(hitBoxData.sturnTime + hitBoxData.hitTime);
        }
        else
        {
            AITestInput aITestInput = Character.GetCharacterComponent<AITestInput>();
            if (aITestInput is not null)
            {
                aITestInput.SetStunTime(hitBoxData.sturnTime + hitBoxData.hitTime);
            }
		}

        CharacterSprite characterSprite = Character.GetCharacterComponent<CharacterSprite>();
        characterSprite.SpriteRenderer.transform.DOKill();
        characterSprite.SpriteRenderer.transform.DOShakePosition(hitBoxData.hitTime, 0.05f, 20).OnComplete(() => 
        {
            characterSprite.ResetModelPosition();
            Character.Rigidbody.AddForce(DegreeToVector3(isRight ? hitBoxData.knockAngle : (-hitBoxData.knockAngle + 180)) * hitBoxData.knockBack, ForceMode.Impulse);
        });

        if (_hp <= 0)
        {
            Debug.Log("Die");
        }
    }


    private Vector3 DegreeToVector3(float degree)
    {
        return new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad), 0);
    }
}
