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
    /// <summary>
    /// CharacterAnimation Getter
    /// </summary>
    /// <value></value>
    private CharacterAnimation CharacterAnimation
    {
        get
        {
            // CharacterAnimation�� null�� ��� Character�κ��� CharacterAnimation�� ������
            _characterAnimation ??= Character.GetCharacterComponent<CharacterAnimation>(ComponentType.Animation);
            // CharacterAnimation�� ��ȯ
            return _characterAnimation;
        }
    }

    // �޺� ī��Ʈ
    private int _comboCount = 0;

    // CharacterStat ĳ��
    private CharacterStat _characterStat;

    // CharacterAnimation ĳ��
    private CharacterAnimation _characterAnimation;

    // ���� �ð�
    private float _sturnTime;

    // �¾��� �� �� Action
    private System.Action _damagedAction;

    // �޺� ī��Ʈ Getter
    public int HitCount
    {
        get
        {
            return _comboCount;
        }
    }

    // ���� �ð� Getter
    public float SturnTime
    {
        get
        {
            return _sturnTime;
        }
    }

    // �¾��� �� �� Action property
    public System.Action DamagedAction
    {
        get
        {
            return _damagedAction;
        }
        set
        {
            _damagedAction = value;
        }
    }

    /// <summary>
    /// CharacterDamage ������
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public CharacterDamage(Character character) : base(character)
    {

    }

    /// <summary>
    /// ���� �Ҵ�
    /// </summary>
    protected override void Awake()
    {
        // CharacterStat ĳ��
        _characterStat = Character.GetCharacterComponent<CharacterStat>(ComponentType.Stat);
    }

    /// <summary>
    /// Event ���
    /// </summary>
    protected override void SetEvent()
    {
        // CharacterEvent�� �¾��� �� �̺�Ʈ ���
        CharacterEvent.AddEvent(EventKeyWord.DAMAGE, OnDamage, EventType.DEFAULT);
    }

    /// <summary>
    /// Update �Լ�
    /// </summary>
    public override void Update()
    {
        base.Update();

        // ���� �ð��� 0���� ũ��
        if (_sturnTime > 0f)
        {
            // ĳ���� ������ �ִϸ��̼� Hash true
            CharacterAnimation.SetAnimationBool(AnimationType.Damage, true);
            // ���� �ð� ����
            _sturnTime -= Time.deltaTime;
        }
        else
        {
            // �޺� �ʱ�ȭ
            _comboCount = 0;
            // ĳ���� ������ �ִϸ��̼� Hash false
            CharacterAnimation.SetAnimationBool(AnimationType.Damage, false);
        }
    }

    /// <summary>
    /// ������ �Ծ�����
    /// </summary>
    private void OnDamage()
    {

    }

    /// <summary>
    /// ���� �������� ��ó��
    /// </summary>
    /// <param name="hitBox"></param>
    /// <param name="hitBoxData"></param>
    /// <param name="collistionPoint"></param>
    /// <param name="isRight"></param>
    public void OnAttacked(HitBox hitBox, HitBoxData hitBoxData, Vector3 collistionPoint, bool isRight)
    {
        // ĳ���� �׾��ų� ���� ���� ���̸� ����
        if (!_characterStat.IsAlive || !RoundManager.ReturnIsSetting())
        {
            // ĳ���� ������ٵ� �ӵ� 0���� �ʱ�ȭ
            Character.Rigidbody.velocity = Vector3.zero;
            return;
        }

        // Hp ó��
        _characterStat.AddHP(-hitBoxData.damage);
        // CharacterDebug�� ������ �߰�
        Character.GetCharacterComponent<CharacterDebug>(ComponentType.Debug).AddDamaged(hitBoxData.damage);

        // Exp ó��
        Character.GetCharacterComponent<CharacterLevel>(ComponentType.Level).AddExp(hitBoxData.addExp / 2);

        // Die
        if (!_characterStat.IsAlive)
        {
            // ���� ���� ó��
            RoundManager.RoundEnd(Character);

            //Effect & Sound
            SoundManager.Instance.PlayEFF("se_common_finishhit");
            EffectManager.Instance.SetEffect(EffectType.FinalHit, collistionPoint);

            return;
        }

        // StunTime
        float stunHitTime = hitBoxData.sturnTime + hitBoxData.hitTime;

        // CameraShake
        CameraManager.SetShake(hitBoxData.shakeTime, hitBoxData.shakePower);
        hitBox?.OwnerHitTime(hitBoxData.hitTime);

        // Effect & Sound
        EffectManager.Instance.SetEffect(hitBoxData.hitEffectType, collistionPoint, EffectDirectionType.ReverseDirection, hitBoxData.atkEffectOffset, (collistionPoint.x < Character.transform.position.x ? true : false));

        SoundManager.Instance.PlayEFF(hitBoxData.hitEffSoundName);

        // ComboCount
        _comboCount++;
        _sturnTime = stunHitTime;
        CharacterAnimation.SetAnimationBool(AnimationType.Damage, true);
        _damagedAction?.Invoke();

        // Set HitTime & StunTime
        Vector3 vector = Character.Rigidbody.velocity;
        CharacterGravity characterGravity = Character.GetCharacterComponent<CharacterGravity>(ComponentType.Gravity);
        CharacterMove characterMove = Character.GetCharacterComponent<CharacterMove>(ComponentType.Move);
        CharacterDodge characterDodge = Character.GetCharacterComponent<CharacterDodge>(ComponentType.Dodge);
        characterMove.SetStunTime(stunHitTime);
        characterDodge.SetStunTime(stunHitTime);
        characterGravity.SetHitTime(stunHitTime);
        Character.Rigidbody.velocity = Vector3.zero;
        CharacterInput characterInput = Character.GetCharacterComponent<CharacterInput>(ComponentType.Input);

        if (characterInput is not null)
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

        // CharacterShake
        CharacterSprite characterSprite = Character.GetCharacterComponent<CharacterSprite>(ComponentType.Sprite);
        characterSprite.Model.transform.DOKill();
        characterSprite.Model.transform.DOShakePosition(hitBoxData.hitTime, 0.05f, 20).OnComplete(() =>
        {
            characterSprite.ResetModelPosition();

            if (_characterStat.IsAlive || !RoundManager.ReturnIsSetting())
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

    /// <summary>
    /// ������ ���� ���󰡴� ���⿡ ���� Vector3�� ��ȯ
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    private Vector3 DegreeToVector3(float degree)
    {
        return new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad), 0);
    }
}
