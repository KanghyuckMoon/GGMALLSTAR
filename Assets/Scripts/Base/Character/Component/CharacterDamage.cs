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
    private CharacterAnimation CharacterAnimation
	{
        get
		{
            characterAnimation ??= Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
            return characterAnimation;
		}
	}


    private int _comboCount = 0;
    private CharacterStat characterStat;
    private CharacterAnimation characterAnimation;
    private float _sturnTime;
    private System.Action damagedAction;

    public int HitCount
	{
        get
		{
            return _comboCount;
		}
	}

    public float SturnTime
	{
        get
		{
            return _sturnTime;
		}
    }
    public System.Action DamagedAction
    {
        get
        {
            return damagedAction;
        }
        set
		{
            damagedAction = value;

        }
    }

    public CharacterDamage(Character character) : base(character)
    {

    }

    protected override void Awake()
    {
        characterStat = Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
    }

    protected override void SetEvent()
    {
        CharacterEvent.AddEvent(EventKeyWord.DAMAGE, OnDamage, EventType.DEFAULT);
    }

	public override void Update()
	{
		base.Update();

        if (_sturnTime > 0f)
		{
            CharacterAnimation.SetAnimationBool(AnimationType.Damage, true);
            _sturnTime -= Time.deltaTime;
        }
        else
		{
            _comboCount = 0;
            CharacterAnimation.SetAnimationBool(AnimationType.Damage, false);
        }
	}


    private void OnDamage()
    {

    }


    public void OnAttcked(HitBox hitBox, HitBoxData hitBoxData, Vector3 collistionPoint, bool isRight)
    {
        if(!characterStat.IsAlive || !RoundManager.ReturnIsSetting())
        {
            Character.Rigidbody.velocity = Vector3.zero;
            return;
		}

        //Hp
        characterStat.AddHP(-hitBoxData.damage);
        Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddDamaged(hitBoxData.damage);

        //Exp
        Character.GetCharacterComponent<CharacterLevel>(ComponentType.Level).AddExp(hitBoxData.addExp / 2);

        //Die
        if (!characterStat.IsAlive)
        {
            RoundManager.RoundEnd(Character);

            //Effect & Sound
            SoundManager.Instance.PlayEFF("se_common_finishhit");
            EffectManager.Instance.SetEffect(EffectType.FinalHit, collistionPoint);

            return;
        }

        float stunHitTime = hitBoxData.sturnTime + hitBoxData.hitTime;

        //CameraShake
        CameraManager.SetShake(hitBoxData.shakeTime, hitBoxData.shakePower);
        hitBox?.OwnerHitTime(hitBoxData.hitTime);

        //Effect & Sound
        EffectManager.Instance.SetEffect(hitBoxData.hitEffectType, collistionPoint, EffectDirectionType.ReverseDirection, hitBoxData.atkEffectOffset , (collistionPoint.x < Character.transform.position.x ? true : false));

        SoundManager.Instance.PlayEFF(hitBoxData.hitEffSoundName);

        //ComboCount
        _comboCount++;
        _sturnTime = stunHitTime;
        CharacterAnimation.SetAnimationBool(AnimationType.Damage, true);
        damagedAction?.Invoke();

        //Set HitTime & StunTime
        Vector3 vector = Character.Rigidbody.velocity;
        CharacterGravity characterGravity = Character.GetCharacterComponent<CharacterGravity>(ComponentType.Gravity);
        CharacterMove characterMove = Character.GetCharacterComponent<CharacterMove>(ComponentType.Move);
        CharacterDodge characterDodge = Character.GetCharacterComponent<CharacterDodge>(ComponentType.Dodge);
        characterMove.SetSturnTime(stunHitTime);
        characterDodge.SetSturnTime(stunHitTime);
        characterGravity.SetHitTime(stunHitTime);
        Character.Rigidbody.velocity = Vector3.zero;
        CharacterInput characterInput = Character.GetCharacterComponent<CharacterInput>(ComponentType.Input);
        if(characterInput is not null)
		{
            characterInput.SetStunTime(stunHitTime);
        }
        else
        {
            CharacterAIInput aITestInput = Character.GetCharacterComponent<CharacterAIInput>(ComponentType.Input);
            if (aITestInput is not null)
            {
                aITestInput.SetStunTime(stunHitTime);
            }
		}

        //CharacterShake
        CharacterSprite characterSprite = Character.GetCharacterComponent<CharacterSprite>(ComponentType.Sprite);
        characterSprite.Model.transform.DOKill();
        characterSprite.Model.transform.DOShakePosition(hitBoxData.hitTime, 0.05f, 20).OnComplete(() => 
        {
            characterSprite.ResetModelPosition();

            if(characterStat.IsAlive || !RoundManager.ReturnIsSetting())
            {
                Vector3 knockBackVector3 = DegreeToVector3(isRight ? hitBoxData.knockAngle : (-hitBoxData.knockAngle + 180));
                Character.Rigidbody.AddForce(knockBackVector3 * (hitBoxData.knockBack + _comboCount * 0.08f), ForceMode.Impulse);
            }
            else
			{
                Character.Rigidbody.velocity = Vector3.zero;
            }
        });

    }


    private Vector3 DegreeToVector3(float degree)
    {
        return new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad), 0);
    }
}
