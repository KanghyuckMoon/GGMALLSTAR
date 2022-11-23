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
    public float AirMoveSpeed;
    public float GravityScale;

    [Header("Attack Stats")]
    public float attackDelay;
    public float skill1Delay;
    public float skill2Delay;

    [Header("Character's HitBox Collision")]
    public Vector3 HitBoxOffset;
    public Vector3 HitBoxSize;
}
