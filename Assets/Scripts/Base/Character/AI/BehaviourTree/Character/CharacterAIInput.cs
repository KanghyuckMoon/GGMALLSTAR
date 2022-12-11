using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class CharacterAIInput : CharacterComponent
{
    public CharacterAIInput(Character character) : base(character)
    {
        Init();
    }

    protected InputData[] _inputData = null;
    protected Dictionary<KeyCode, bool> _wasInput = null;
    protected Dictionary<KeyCode, bool> _previousInput = null;

    protected Character opponentCharacter;
    protected KeyCode inputKeyCode = KeyCode.A;
    protected bool _isLoop = false;
    protected float _delay = 0.1f;
    protected BehaviourTree _behaviourTree;
    protected float _stunTime = 0f;
    protected float _inputDelayTime = 0f;
    protected CharacterSkill characterSkill;

    protected virtual void Init()
    {
        var characterSpawner = GameObject.FindObjectOfType<CharacterSpawner>();
        bool isPlayer1 = false;
        if (characterSpawner.Player1 == Character.gameObject)
        {
            opponentCharacter = characterSpawner.Player2.GetComponent<Character>();
            isPlayer1 = true;
        }
        else
        {
            opponentCharacter = characterSpawner.Player1.GetComponent<Character>();
        }
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
        SetBehaviourTree(isPlayer1);
    }

    public void IsHit(string actionName)
	{
        _behaviourTree.IsHit(actionName);
	}

    protected virtual void SetBehaviourTree(bool isPlayer1)
    {
        _behaviourTree = new BehaviourTree();
        _behaviourTree.Init(opponentCharacter, Character, this);
    }

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
            _inputDelayTime = 0f;
            return;
        }

        if (_inputDelayTime > 0f)
        {
            _inputDelayTime -= Time.deltaTime;
            return;
        }


        _behaviourTree.Update();

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

    public void MultipleHoldInputKey(KeyCode keyCode)
    {
        if (_wasInput[keyCode])
        {
            CharacterEvent.EventTrigger(GetActionName(keyCode), EventType.KEY_HOLD);
        }
        else
        {
            _wasInput[keyCode] = true;
            _previousInput[keyCode] = true;
            CharacterEvent.EventTrigger(GetActionName(keyCode), EventType.KEY_DOWN);
        }
    }
    public void SingleHoldInputKey(KeyCode keyCode)
    {
        inputKeyCode = keyCode;
        _wasInput[inputKeyCode] = true;
    }

    public void FalseInputKey(KeyCode keyCode)
    {
        _wasInput[keyCode] = false;
        _previousInput[keyCode] = false;
        CharacterEvent.EventTrigger(GetActionName(keyCode), EventType.KEY_UP);
    }

    public void TapInputKey(KeyCode keyCode)
    {
        _wasInput[keyCode] = true;
        _previousInput[keyCode] = true;
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

