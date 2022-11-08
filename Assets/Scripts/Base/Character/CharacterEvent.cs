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

    private Dictionary<string, System.Action>[] characterEvent = new Dictionary<string, System.Action>[Enum.GetValues(typeof(EventType)).Length];

    public void AddEvent(string actionName, System.Action action, EventType eventType = EventType.DEFAULT)
    {
        if (characterEvent[(uint)eventType].ContainsKey(actionName))
        {
            characterEvent[(uint)eventType][actionName] += action;
        }
        else
        {
            characterEvent[(uint)eventType].Add(actionName, action);
        }
    }

    public void AddEvent(string actionName, EventType eventType = EventType.DEFAULT)
    {
        if (characterEvent[(uint)eventType].ContainsKey(actionName))
        {
            characterEvent[(uint)eventType][actionName] += null;
        }
        else
        {
            characterEvent[(uint)eventType].Add(actionName, null);
        }
    }

    public void EventTrigger(string actionName, EventType eventType = EventType.DEFAULT)
    {
        if (characterEvent[(uint)eventType].ContainsKey(actionName))
        {
            characterEvent[(uint)eventType][actionName]?.Invoke();
        }
    }

    public void RemoveEvent(string actionName, System.Action action = null, EventType eventType = EventType.DEFAULT)
    {
        if (characterEvent[(uint)eventType].ContainsKey(actionName))
        {
            if (action != null)
            {
                characterEvent[(uint)eventType][actionName] -= action;
            }
            else
            {
                characterEvent[(uint)eventType].Remove(actionName);
            }
        }
    }
}
