using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/New Character")]
public class CharacterSO : ScriptableObject
{
    public CollaborationGame CollaborationGame;
    public string Name;
    public Sprite CharacterImage;
    public InputDataBaseSO InputDataBaseSO;
}
