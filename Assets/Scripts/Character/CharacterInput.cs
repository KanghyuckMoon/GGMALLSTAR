using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : CharacterComponent
{
    public CharacterInput(Character character) : base(character)
    {
        _inputDataBaseSO = Character.InputDataBaseSO;
        _characterEvent = Character.GetCharacterComponent<CharacterEvent>();

        InputData[] inputData = _inputDataBaseSO.GetInputData();

        foreach (var input in inputData)
        {
            _characterEvent.AddEvent(input.actionName);
        }
    }

    private InputDataBaseSO _inputDataBaseSO = null;
    private CharacterEvent _characterEvent = null;

    public override void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode))
                {
                    string actionName = _inputDataBaseSO.GetInputData(keyCode);
                    _characterEvent.EventTrigger(actionName);
                }
            }
        }
    }
}
