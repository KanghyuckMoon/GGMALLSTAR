using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class AITestInput : CharacterComponent
{
    public AITestInput(Character character) : base(character)
    {
        _inputData = Character.InputDataBaseSO.GetInputData();

        _wasInput = new();
        _previousInput = new();

        foreach (var input in _inputData)
        {
            _wasInput.Add(input.keyCode, false);
            _previousInput.Add(input.keyCode, false);
            for (uint i = 0; i < Enum.GetValues(typeof(EventType)).Length; i++)
            {
                CharacterEvent.AddEvent(input.actionName, (EventType)i);
            }
        }
        StaticCoroutine.Instance.StartCoroutine(RandomInput());
    }

    protected InputData[] _inputData = null;
    protected Dictionary<KeyCode, bool> _wasInput = null;
    protected Dictionary<KeyCode, bool> _previousInput = null;

    private KeyCode inputKeyCode = KeyCode.A;
    private int randomInput = 0;

    public override void Update()
    {
        if (_wasInput[inputKeyCode] && _previousInput[inputKeyCode])
        {
            CharacterEvent.EventTrigger(GetActionName(inputKeyCode), EventType.KEY_HOLD);
        }
        else if (_wasInput[inputKeyCode] && !_previousInput[inputKeyCode])
        {
            _wasInput[inputKeyCode] = true;
            _previousInput[inputKeyCode] = true;
            CharacterEvent.EventTrigger(GetActionName(inputKeyCode), EventType.KEY_DOWN);
        }
        else if (!_wasInput[inputKeyCode] && _previousInput[inputKeyCode])
        {
            _wasInput[inputKeyCode] = false;
            _previousInput[inputKeyCode] = false;
            CharacterEvent.EventTrigger(GetActionName(inputKeyCode), EventType.KEY_UP);
        }
    }

    public IEnumerator RandomInput()
	{
        while (true)
        {
            _wasInput[inputKeyCode] = false;
            randomInput = UnityEngine.Random.Range(0, 2);
            if (randomInput == 0)
            {
                inputKeyCode = KeyCode.A;
            }
            else
            {
                inputKeyCode = KeyCode.D;
            }
            _wasInput[inputKeyCode] = true;
            yield return new WaitForSeconds(1f);
        }
    }

    private string GetActionName(KeyCode keyCode)
	{
        for (int i = 0; i < _inputData.Length; ++i)
		{
            if (_inputData[i].keyCode == keyCode)
			{
                return _inputData[i].actionName;
            }
		}
        return null;
    }
}

