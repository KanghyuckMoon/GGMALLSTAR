using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/New Character")]
public class CharacterSO : ScriptableObject
{
    public string Name;
    public Sprite CharacterImage;

    [Header("Character Stats")]
    public float MaxHP;

    [Header("Character's HitBox Collision")]
    public Vector3 HitBoxSize;
    public Vector3 HitBoxOffset;
}
