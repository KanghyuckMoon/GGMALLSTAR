using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : CharacterComponent
{
    public CharacterInput(Character character) : base(character)
    {
        _inputData = Character.InputDataBaseSO.GetInputData();

        _wasInput = new();

        foreach (var input in _inputData)
        {
            _wasInput.Add(input.keyCode, false);
            for (uint i = 0; i < Enum.GetValues(typeof(EventType)).Length; i++)
            {
                CharacterEvent.AddEvent(input.actionName, (EventType)i);
            }
        }
    }

    private InputData[] _inputData = null;
    private Dictionary<KeyCode, bool> _wasInput = null;

    public override void Update()
    {
        foreach (var input in _inputData)
        {
            KeyCode keyCode = input.keyCode;
            string actionName = input.actionName;

            if (_wasInput[keyCode])
            {
                CharacterEvent.EventTrigger(actionName, EventType.KEY_HOLD);
            }
            else if (Input.GetKeyDown(keyCode))
            {
                _wasInput[keyCode] = true;
                CharacterEvent.EventTrigger(actionName, EventType.KEY_DOWN);
            }

            if (Input.GetKeyUp(keyCode))
            {
                _wasInput[keyCode] = false;
                CharacterEvent.EventTrigger(actionName, EventType.KEY_UP);
            }
        }
    }
}
