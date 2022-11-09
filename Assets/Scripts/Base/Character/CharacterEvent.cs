using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType : uint
{
    DEFAULT,
    KEY_DOWN,
    KEY_UP,
    KEY_HOLD,
}

public class CharacterEvent : CharacterComponent
{
    public CharacterEvent(Character character) : base(character)
    {
    }

    protected override void Awake()
    {
        uint eventTypeLength = (uint)Enum.GetValues(typeof(EventType)).Length;
        _characterEvent = new Dictionary<string, System.Action>[eventTypeLength];
        for (uint i = 0; i < eventTypeLength; i++)
        {
            _characterEvent[i] = new Dictionary<string, System.Action>();
        }
    }

    private Dictionary<string, System.Action>[] _characterEvent = null;

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

    public void EventTrigger(string actionName, EventType eventType = EventType.DEFAULT)
    {
        if (_characterEvent[(uint)eventType].ContainsKey(actionName))
        {
            _characterEvent[(uint)eventType][actionName]?.Invoke();
        }
    }

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
