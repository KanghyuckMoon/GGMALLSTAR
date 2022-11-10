using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/New Character")]
public class CharacterSO : ScriptableObject
{
    public CollaborationGame CollaborationGame;
    public string Name;
    public Sprite CharacterImage;

    [Header("Character Stats")]
    public float MaxHP;
}
