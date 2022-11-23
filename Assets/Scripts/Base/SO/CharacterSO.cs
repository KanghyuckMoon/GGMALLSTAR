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
    public float GravityScale;

    [Header("Character's HitBox Collision")]
    public Vector3 HitBoxOffset;
    public Vector3 HitBoxSize;
}
