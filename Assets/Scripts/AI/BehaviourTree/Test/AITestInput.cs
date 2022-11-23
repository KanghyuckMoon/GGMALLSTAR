using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class AITestInput : CharacterComponent
{
    public AITestInput(Character character) : base(character)
    {
        var characterSpawner = GameObject.FindObjectOfType<CharacterSpawner>();

        if (characterSpawner.Player1 == Character.gameObject)
        {
            opponentCharacter = characterSpawner.Player2.GetComponent<Character>();
        }
        else
        {
            opponentCharacter = characterSpawner.Player1.GetComponent<Character>();
        }

        behaviourTest = new BehaviourTest(opponentCharacter, Character, this);

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
    }

    protected InputData[] _inputData = null;
    protected Dictionary<KeyCode, bool> _wasInput = null;
    protected Dictionary<KeyCode, bool> _previousInput = null;

    private Character opponentCharacter;
    private KeyCode inputKeyCode = KeyCode.A;
    private bool _isLoop = false;
    private float _delay = 0.1f;
    private BehaviourTest behaviourTest;
    private float _stunTime = 0f;
    private float _inputDelayTime = 0f;

    public void SetStunTime(float time)
    {
        _stunTime = time;
    }

    public void SetInputDelayTime(float time)
    {
        _inputDelayTime = time;
    }
    public override void Update()
    {
        if (_stunTime > 0f)
        {
            _stunTime -= Time.deltaTime;
            return;
        }

        if (_inputDelayTime > 0f)
        {
            _inputDelayTime -= Time.deltaTime;
            return;
        }


        behaviourTest.Update();

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

    public void HoldInputKey(KeyCode keyCode)
    {
        _wasInput[inputKeyCode] = false;
        inputKeyCode = keyCode;
        _wasInput[inputKeyCode] = true;
    }
    public void FalseInputKey(KeyCode keyCode)
    {
        _wasInput[keyCode] = false;
    }
    public void TapInputKey(KeyCode keyCode)
    {
        CharacterEvent.EventTrigger(GetActionName(keyCode), EventType.KEY_DOWN);
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

