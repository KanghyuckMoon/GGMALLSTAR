using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvent : CharacterComponent
{
    public CharacterEvent(Character character) : base(character)
    {
    }

    private Dictionary<string, System.Action> characterEvent = new();

    public void AddEvent(string actionName, System.Action action)
    {
        if (characterEvent.ContainsKey(actionName))
        {
            characterEvent[actionName] += action;
        }
        else
        {
            characterEvent.Add(actionName, action);
        }
    }

    public void AddEvent(string actionName)
    {
        if (characterEvent.ContainsKey(actionName))
        {
            characterEvent[actionName] += null;
        }
        else
        {
            characterEvent.Add(actionName, null);
        }
    }

    public void EventTrigger(string actionName)
    {
        if (characterEvent.ContainsKey(actionName))
        {
            characterEvent[actionName]?.Invoke();
        }
    }

    public void RemoveEvent(string actionName, System.Action action = null)
    {
        if (characterEvent.ContainsKey(actionName))
        {
            if (action != null)
            {
                characterEvent[actionName] -= action;
            }
            else
            {
                characterEvent.Remove(actionName);
            }
        }
    }
}
