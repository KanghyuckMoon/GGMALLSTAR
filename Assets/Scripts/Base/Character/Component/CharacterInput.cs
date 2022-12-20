using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : CharacterComponent
{
    /// <summary>
    /// InputDataBaseSO에 있는 InputData를 가져오는 생성자
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public CharacterInput(Character character) : base(character)
    {
        _inputData = Character.InputDataBaseSO.GetInputData();

        _wasInput = new();

        // 데이터에 맞게 변수 초기화
        foreach (var input in _inputData)
        {
            _wasInput.Add(input.keyCode, false);
            for (uint i = 0; i < Enum.GetValues(typeof(EventType)).Length; i++)
            {
                CharacterEvent.AddEvent(input.actionName, (EventType)i);
            }
        }
    }

    // 입력 데이터 저장하는 복합 데이터
    protected InputData[] _inputData = null;
    // 입력된 데이터 확인하는 복합 데이터
    protected Dictionary<KeyCode, bool> _wasInput = null;
    // 기절 시간
    private float _stunTime = 0f;
    // 입력 지연 시간
    private float _inputDelayTime = 0f;
    private InputData fastInputData = null;


    public override void Update()
    {
        // 스턴 당한지 채크
        if (_stunTime > 0f)
        {
            // 스턴 처리
            _stunTime -= Time.deltaTime;
            _inputDelayTime = 0;
        }

        // 입력 지연 채크
        if (_stunTime <= 0f && _inputDelayTime > 0f)
        {
            _inputDelayTime -= Time.deltaTime;
        }

        // 입력 처리
        if (_stunTime <= 0f && _inputDelayTime <= 0f && fastInputData != null)
        {
            //InputAction(fastInputData);
            _wasInput[fastInputData.keyCode] = true;
            CharacterEvent.EventTrigger(fastInputData.actionName, EventType.KEY_DOWN);
            fastInputData = null;
        }

        // 입력 데이터에 맞게 입력 처리
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
    /// 입력 Action 실행 함수
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

    // 스턴 시간 설정
    public void SetStunTime(float time)
    {
        _stunTime = time;
    }

    // 입력 지연 시간 설정
    public void SetInputDelayTime(float time)
    {
        _inputDelayTime = time;
    }
}
