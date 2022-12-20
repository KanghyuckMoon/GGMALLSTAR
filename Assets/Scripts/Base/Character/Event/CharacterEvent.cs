using System;
using System.Collections;
using System.Collections.Generic;


public class CharacterEvent
{
    // 이벤트가 실행 가능한 상황인지 체크하는 변수
    public bool _canEvent = true;

    public CharacterEvent()
    {
        // 이벤트 타입의 갯수만큼 Dictionary를 생성
        uint eventTypeLength = (uint)Enum.GetValues(typeof(EventType)).Length;
        _characterEvent = new Dictionary<string, System.Action>[eventTypeLength];
        for (uint i = 0; i < eventTypeLength; i++)
        {
            _characterEvent[i] = new Dictionary<string, System.Action>();
        }
    }

    // 이벤트를 저장할 Dictionary
    private Dictionary<string, System.Action>[] _characterEvent = null;

    /// <summary>
    /// 이벤트 추가 함수
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="action"></param>
    /// <param name="eventType"></param>
    public void AddEvent(string actionName, System.Action action, EventType eventType = EventType.DEFAULT)
    {
        if (_characterEvent[(uint)eventType].ContainsKey(actionName))
        {
            _characterEvent[(uint)eventType][actionName] += action;
        }
        else
        {
            _characterEvent[(uint)eventType].Add(actionName, action);
        }
    }

    /// <summary>
    /// 이벤트 추가 함수
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="eventType"></param>
    public void AddEvent(string actionName, EventType eventType = EventType.DEFAULT)
    {
        if (_characterEvent[(uint)eventType].ContainsKey(actionName))
        {
            _characterEvent[(uint)eventType][actionName] += null;
        }
        else
        {
            _characterEvent[(uint)eventType].Add(actionName, null);
        }
    }

    /// <summary>
    /// 이벤트 실행 함수
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="eventType"></param>
    public void EventTrigger(string actionName, EventType eventType = EventType.DEFAULT)
    {
        if (_characterEvent[(uint)eventType].ContainsKey(actionName) && _canEvent)
        {
            _characterEvent[(uint)eventType][actionName]?.Invoke();
        }
    }

    /// <summary>
    /// 이벤트 삭제 함수
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="action"></param>
    /// <param name="eventType"></param>
    public void RemoveEvent(string actionName, System.Action action = null, EventType eventType = EventType.DEFAULT)
    {
        if (_characterEvent[(uint)eventType].ContainsKey(actionName))
        {
            if (action != null)
            {
                _characterEvent[(uint)eventType][actionName] -= action;
            }
            else
            {
                _characterEvent[(uint)eventType].Remove(actionName);
            }
        }
    }
}
