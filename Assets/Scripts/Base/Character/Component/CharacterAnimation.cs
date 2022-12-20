using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : CharacterComponent
{
    /// <summary>
    /// CharacterAnimation ������
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public CharacterAnimation(Character character) : base(character)
    {

    }

    // Animator ĳ���� ����
    private Animator _animator = null;

    // �ִϸ��̼� �ؽð��� ������ Dictionary
    private Dictionary<AnimationType, int> _animationHash = null;

    // �ִϸ��̼� �ؽð��� ������ HashSet
    private HashSet<int> validParameters = null;

    // �ִϸ��̼� �ؽ� Getter
    protected Dictionary<AnimationType, int> AnimationHash => _animationHash;

    /// <summary>
    /// CharacterAnimation ���� �ʱ�ȭ
    /// </summary>
    protected override void Awake()
    {
        _animator = Character.Animator;
        _animationHash = new();
        validParameters = new();
    }

    /// <summary>
    /// Animation Hash ���
    /// </summary>
    /// <param name="animationType"></param>
    /// <param name="animationName"></param>
    protected void AddAnimationHash(AnimationType animationType, string animationName)
    {
        _animationHash.Add(animationType, Animator.StringToHash(animationName));
        validParameters.Add(Animator.StringToHash(animationName));
    }

    /// <summary>
    /// Animation Hash Trigger
    /// </summary>
    /// <param name="animationType"></param>
    public void SetAnimationTrigger(AnimationType animationType)
    {
        if (CheckHasParam(animationType))
        {
            _animator.SetTrigger(AnimationHash[animationType]);
        }
    }

    /// <summary>
    /// Animation Hash Trigger
    /// </summary>
    /// <param name="animationHash"></param>
    protected void SetAnimationTrigger(int animationHash)
    {
        _animator.SetTrigger(animationHash);
    }

    /// <summary>
    /// Animation Hash Trigger
    /// </summary>
    /// <param name="animationName"></param>
    protected void SetAnimationTrigger(string animationName)
    {
        _animator.SetTrigger(Animator.StringToHash(animationName));
    }

    /// <summary>
    /// Animation Bool �� ����
    /// </summary>
    /// <param name="animationType"></param>
    /// <param name="value"></param>
    public void SetAnimationBool(AnimationType animationType, bool value)
    {
        if (CheckHasParam(animationType))
        {
            _animator.SetBool(AnimationHash[animationType], value);
        }
    }

    /// <summary>
    /// Animation Bool �� ����
    /// </summary>
    /// <param name="animationHash"></param>
    /// <param name="value"></param>
    protected void SetAnimationBool(int animationHash, bool value)
    {
        _animator.SetBool(animationHash, value);
    }

    /// <summary>
    /// Animation Bool �� ����
    /// </summary>
    /// <param name="animationName"></param>
    /// <param name="value"></param>
    protected void SetAnimationBool(string animationName, bool value)
    {
        _animator.SetBool(Animator.StringToHash(animationName), value);
    }

    /// <summary>
    /// Animation Float �� ����
    /// </summary>
    /// <param name="animationType"></param>
    /// <param name="value"></param>
    public void SetAnimationFloat(AnimationType animationType, float value)
    {
        if (CheckHasParam(animationType))
        {
            _animator.SetFloat(AnimationHash[animationType], value);
        }
    }

    /// <summary>
    /// Animation Float �� ����
    /// </summary>
    /// <param name="animationHash"></param>
    /// <param name="value"></param>
    protected void SetAnimationFloat(int animationHash, float value)
    {
        _animator.SetFloat(animationHash, value);
    }

    /// <summary>
    /// Animation Float �� ����
    /// </summary>
    /// <param name="animationName"></param>
    /// <param name="value"></param>
    protected void SetAnimationFloat(string animationName, float value)
    {
        _animator.SetFloat(Animator.StringToHash(animationName), value);
    }

    /// <summary>
    /// Animation Int �� ����
    /// </summary>
    /// <param name="animationType"></param>
    /// <param name="value"></param>
    public void SetAnimationInt(AnimationType animationType, int value)
    {
        if (CheckHasParam(animationType))
        {
            _animator.SetInteger(AnimationHash[animationType], value);
        }
    }

    /// <summary>
    /// Animation Int �� ����
    /// </summary>
    /// <param name="animationHash"></param>
    /// <param name="value"></param>
    protected void SetAnimationInt(int animationHash, int value)
    {
        _animator.SetInteger(animationHash, value);
    }

    /// <summary>
    /// Animation Int �� ����
    /// </summary>
    /// <param name="animationName"></param>
    /// <param name="value"></param>
    protected void SetAnimationInt(string animationName, int value)
    {
        _animator.SetInteger(Animator.StringToHash(animationName), value);
    }

    /// <summary>
    /// �ǰ� ȿ�� ����
    /// </summary>
    /// <param name="hitTime"></param>
    public void SetHitTime(float hitTime)
    {
        _animator.speed = 0;
        Character.StartCoroutine(EndHitTime(hitTime));
    }

    /// <summary>
    /// Animation Hash �� Ȯ��
    /// </summary>
    /// <param name="animationType"></param>
    /// <returns></returns>
    protected bool CheckHasParam(AnimationType animationType)
    {
        return _animationHash.TryGetValue(animationType, out int value);
    }

    /// <summary>
    /// �ǰ� ȿ�� ����
    /// </summary>
    /// <param name="hitTime"></param>
    /// <returns></returns>
    private IEnumerator EndHitTime(float hitTime)
    {
        yield return new WaitForSeconds(hitTime);
        _animator.speed = 1;
    }
}
