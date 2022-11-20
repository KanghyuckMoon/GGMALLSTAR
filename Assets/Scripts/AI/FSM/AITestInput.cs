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

    private Character opponentCharacter;
    private KeyCode inputKeyCode = KeyCode.A;
    private bool _isLoop = false;
    private float _delay = 0.1f;

    public override void Update()
    {
        if (_wasInput[inputKeyCode] && _previousInput[inputKeyCode])
        {
            CharacterEvent.EventTrigger(GetActionName(inputKeyCode), EventType.KEY_HOLD);
        }
        else if (_wasInput[inputKeyCode] && !_previousInput[inputKeyCode])
        {
            if (_isLoop)
            {
                _wasInput[inputKeyCode] = true;
                _previousInput[inputKeyCode] = true;
            }
            else
			{
                _delay -= Time.deltaTime;
                if (_delay < 0f)
				{
                    _delay = 0.1f;
				}
                else
				{
                    return;
				}
			}
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

            if(opponentCharacter.transform.position.x > Character.transform.position.x)
			{
                if(Vector2.Distance(opponentCharacter.transform.position, Character.transform.position) < 0.2f)
                {
                    inputKeyCode = KeyCode.J;
                    _isLoop = false;
                }
                else
                {
                    inputKeyCode = KeyCode.D;
                    _isLoop = true;
                }
            }
            else if (opponentCharacter.transform.position.x < Character.transform.position.x)
            {
                if (Vector2.Distance(opponentCharacter.transform.position, Character.transform.position) < 0.2f)
                {
                    inputKeyCode = KeyCode.J;
                    _isLoop = false;
                }
                else
                {
                    inputKeyCode = KeyCode.A;
                    _isLoop = true;
                }
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

