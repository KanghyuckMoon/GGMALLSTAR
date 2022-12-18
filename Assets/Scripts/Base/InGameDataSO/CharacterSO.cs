using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/New Character")]
public class CharacterSO : ScriptableObject
{
    public string Name;
    public Sprite CharacterImage;

    [Header("Character Stats")]
    public float MaxHP;
    public float FirstJumpPower;
    public float SecondJumpPower;
    public float MoveSpeed;
    public float DodgeSpeed;
    public float AirMoveSpeed;
    public float GravityScale;

    [Header("Attack Stats")]
    public float attackDelay;
    public float skill1Delay;
    public float skill2Delay;

    [Header("Character's HitBox Collision")]
    public Vector3 HitBoxOffset;
    public Vector3 HitBoxSize;

    public void Copy(ref CharacterSO characterSO)
	{
        characterSO.Name = this.Name;
        characterSO.CharacterImage = this.CharacterImage;
        characterSO.MaxHP = this.MaxHP;
        characterSO.FirstJumpPower = this.FirstJumpPower;
        characterSO.SecondJumpPower = this.SecondJumpPower;
        characterSO.MoveSpeed = this.MoveSpeed;
        characterSO.DodgeSpeed = this.DodgeSpeed;
        characterSO.AirMoveSpeed = this.AirMoveSpeed;
        characterSO.GravityScale = this.GravityScale;
        characterSO.attackDelay = this.attackDelay;
        characterSO.skill1Delay = this.skill1Delay;
        characterSO.skill2Delay = this.skill2Delay;
        characterSO.HitBoxOffset = this.HitBoxOffset;
        characterSO.HitBoxSize = this.HitBoxSize;
    }
}
