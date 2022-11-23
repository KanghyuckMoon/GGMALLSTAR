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
        //_hp = Character.GetCharacterComponent<CharacterStat>().MaxHP;
        characterStat = Character.GetCharacterComponent<CharacterStat>();
    }

    protected override void SetEvent()
    {
        CharacterEvent.AddEvent(EventKeyWord.DAMAGE, OnDamage, EventType.DEFAULT);
    }

	public override void Update()
	{
		base.Update();

        if (_stunTime > 0f)
		{
            _stunTime -= Time.deltaTime;
        }
        else
		{
            _comboCount = 0;
        }
	}

	//private float _hp = 0;
    private int _comboCount = 0;
    private CharacterStat characterStat;

    private void OnDamage()
    {
        //_hp -= 10;
        //if (_hp <= 0)
        //{
        //    Debug.Log("Die");
        //}
    }

    private float _stunTime;

    public void OnAttcked(HitBoxData hitBoxData, Vector3 collistionPoint, bool isRight)
    {

        //ComboCount
        _comboCount++;
        _stunTime = hitBoxData.sturnTime + hitBoxData.hitTime;
        EffectManager.Instance.SetComboCountEffect(_comboCount, hitBoxData.sturnTime + hitBoxData.hitTime, collistionPoint);

        //Effect & Sound
        EffectManager.Instance.SetEffect(hitBoxData.effectType, collistionPoint);
        SoundManager.Instance.PlayEFF(hitBoxData.hitEffSoundName);

        //Set HitTime & StunTime
        Vector3 vector = Character.Rigidbody.velocity;
        CharacterGravity characterGravity = Character.GetCharacterComponent<CharacterGravity>();
        characterGravity.SetHitTime(hitBoxData.sturnTime + hitBoxData.hitTime);
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

        //CharacterShake
        CharacterSprite characterSprite = Character.GetCharacterComponent<CharacterSprite>();
        characterSprite.SpriteRenderer.transform.DOKill();
        characterSprite.SpriteRenderer.transform.DOShakePosition(hitBoxData.hitTime, 0.05f, 20).OnComplete(() => 
        {
            characterSprite.ResetModelPosition();
            Vector3 knockBackVector3 = DegreeToVector3(isRight ? hitBoxData.knockAngle : (-hitBoxData.knockAngle + 180)) * hitBoxData.knockBack;
            Character.Rigidbody.AddForce(knockBackVector3, ForceMode.Impulse);
        });


        //CameraShake
        CameraManager.SetShake(hitBoxData.shakeTime, hitBoxData.shakePower);

        //Hp
        characterStat.AddHP(-hitBoxData.damage);

        //Die
        if (characterStat.HP <= 0)
        {
            RoundManager.RoundEnd(Character);
        }
    }


    private Vector3 DegreeToVector3(float degree)
    {
        return new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad), 0);
    }
}
