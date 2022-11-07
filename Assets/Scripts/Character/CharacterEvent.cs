using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvent : MonoBehaviour
{
    private Dictionary<string, System.Action> characterEvent = new();

    private readonly string[] MOVE_ACTION = { "UP", "DOWN", "LEFT", "RIGHT" };

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

    public void RegistrationToDictionary(InputDataBaseSO inputDataBaseSO)
    {
        foreach (var actionName in inputDataBaseSO.MoveInput.InputData)
        {
            switch (actionName.actionName)
            {
                case "UP":
                case "DOWN":
                case "LEFT":
                case "RIGHT":
                    AddEvent(actionName.actionName, () => Debug.Log(actionName.actionName));
                    AddEvent(actionName.actionName, GetComponent<CharacterMove>().Move);
                    break;
            }
        }
        foreach (var inputData in inputDataBaseSO.InputData)
        {
            switch (inputData.actionName)
            {
                default:
                    AddEvent(inputData.actionName, () => Debug.Log(inputData.actionName));
                    break;
            }
        }
    }

    public void EventTrigger(string actionName)
    {
        if (characterEvent.ContainsKey(actionName))
        {
            characterEvent[actionName]?.Invoke();
        }
    }
}
