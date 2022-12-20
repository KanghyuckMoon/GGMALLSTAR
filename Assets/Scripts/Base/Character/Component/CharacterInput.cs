using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : CharacterComponent
{
    /// <summary>
    /// InputDataBaseSO�� �ִ� InputData�� �������� ������
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public CharacterInput(Character character) : base(character)
    {
        _inputData = Character.InputDataBaseSO.GetInputData();

        _wasInput = new();

        // �����Ϳ� �°� ���� �ʱ�ȭ
        foreach (var input in _inputData)
        {
            _wasInput.Add(input.keyCode, false);
            for (uint i = 0; i < Enum.GetValues(typeof(EventType)).Length; i++)
            {
                CharacterEvent.AddEvent(input.actionName, (EventType)i);
            }
        }
    }

    // �Է� ������ �����ϴ� ���� ������
    protected InputData[] _inputData = null;
    // �Էµ� ������ Ȯ���ϴ� ���� ������
    protected Dictionary<KeyCode, bool> _wasInput = null;
    // ���� �ð�
    private float _stunTime = 0f;
    // �Է� ���� �ð�
    private float _inputDelayTime = 0f;
    private InputData fastInputData = null;


    public override void Update()
    {
        // ���� ������ äũ
        if (_stunTime > 0f)
        {
            // ���� ó��
            _stunTime -= Time.deltaTime;
            _inputDelayTime = 0;
        }

        // �Է� ���� äũ
        if (_stunTime <= 0f && _inputDelayTime > 0f)
        {
            _inputDelayTime -= Time.deltaTime;
        }

        // �Է� ó��
        if (_stunTime <= 0f && _inputDelayTime <= 0f && fastInputData != null)
        {
            //InputAction(fastInputData);
            _wasInput[fastInputData.keyCode] = true;
            CharacterEvent.EventTrigger(fastInputData.actionName, EventType.KEY_DOWN);
            fastInputData = null;
        }

        // �Է� �����Ϳ� �°� �Է� ó��
        foreach (var input in _inputData)
        {
            KeyCode keyCode = input.keyCode;
            string actionName = input.actionName;

            if (_stunTime > 0f || _inputDelayTime > 0f)
            {
                if (Input.GetKeyDown(input.keyCode))
                {
                    fastInputData = input;
                }
            }
            else
            {
                InputAction(input);
            }
        }
    }

    /// <summary>
    /// �Է� Action ���� �Լ�
    /// </summary>
    /// <param name="inputData"></param>
    private void InputAction(InputData inputData)
    {
        if (Input.GetKey(inputData.keyCode))
        {
            if (_wasInput[inputData.keyCode] is false)
            {
                _wasInput[inputData.keyCode] = true;
                CharacterEvent.EventTrigger(inputData.actionName, EventType.KEY_DOWN);
            }
            else
            {
                CharacterEvent.EventTrigger(inputData.actionName, EventType.KEY_HOLD);
            }
        }
        else if (Input.GetKeyUp(inputData.keyCode))
        {
            _wasInput[inputData.keyCode] = false;
            CharacterEvent.EventTrigger(inputData.actionName, EventType.KEY_UP);
        }
        else
        {
            _wasInput[inputData.keyCode] = false;
        }
    }

    // ���� �ð� ����
    public void SetStunTime(float time)
    {
        _stunTime = time;
    }

    // �Է� ���� �ð� ����
    public void SetInputDelayTime(float time)
    {
        _inputDelayTime = time;
    }
}
