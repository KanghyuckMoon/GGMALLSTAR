using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDB", menuName = "DataBase/CharacterDB")]
public class CharacterDataBaseSO : ScriptableObject
{
    public CharacterSO[] CharacterScriptableObjects;
}
