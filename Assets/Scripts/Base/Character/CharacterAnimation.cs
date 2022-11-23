using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : CharacterComponent
{
    public CharacterAnimation(Character character) : base(character)
    {

    }

    private Animator _animator = null;
    private Dictionary<AnimationType, int> _animationHash = null;
    protected Dictionary<AnimationType, int> AnimationHash => _animationHash;

    protected override void Awake()
    {
        _animator = Character.Animator;
        _animationHash = new();
    }

    protected void AddAnimationHash(AnimationType animationType, string animationName)
    {
        _animationHash.Add(animationType, Animator.StringToHash(animationName));
    }

    public void SetAnimationTrigger(AnimationType animationType)
    {
        _animator.SetTrigger(AnimationHash[animationType]);
    }

    protected void SetAnimationTrigger(int animationHash)
    {
        _animator.SetTrigger(animationHash);
    }

    protected void SetAnimationTrigger(string animationName)
    {
        _animator.SetTrigger(Animator.StringToHash(animationName));
    }

    public void SetAnimationBool(AnimationType animationType, bool value)
    {
        _animator.SetBool(AnimationHash[animationType], value);
    }

    protected void SetAnimationBool(int animationHash, bool value)
    {
        _animator.SetBool(animationHash, value);
    }

    protected void SetAnimationBool(string animationName, bool value)
    {
        _animator.SetBool(Animator.StringToHash(animationName), value);
    }

    public void SetAnimationFloat(AnimationType animationType, float value)
    {
        _animator.SetFloat(AnimationHash[animationType], value);
    }

    protected void SetAnimationFloat(int animationHash, float value)
    {
        _animator.SetFloat(animationHash, value);
    }

    protected void SetAnimationFloat(string animationName, float value)
    {
        _animator.SetFloat(Animator.StringToHash(animationName), value);
    }

    public void SetAnimationInt(AnimationType animationType, int value)
    {
        _animator.SetInteger(AnimationHash[animationType], value);
    }

    protected void SetAnimationInt(int animationHash, int value)
    {
        _animator.SetInteger(animationHash, value);
    }

    protected void SetAnimationInt(string animationName, int value)
    {
        _animator.SetInteger(Animator.StringToHash(animationName), value);
    }

    public void SetHitTime(float hitTime)
	{
        _animator.speed = 0;
        Utill.StaticCoroutine.Instance.StartCoroutine(EndHitTime(hitTime));

    }

    private IEnumerator EndHitTime(float hitTime)
	{
        yield return new WaitForSeconds(hitTime);
        _animator.speed = 1;
	}
}
