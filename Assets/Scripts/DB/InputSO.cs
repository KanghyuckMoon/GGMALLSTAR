using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class InputSO : ScriptableObject
{
    public InputData[] InputData;

    public virtual string GetInputData(KeyCode keyCode)
    {
        foreach (var inputData in InputData)
        {
            if (inputData.keyCode == keyCode)
            {
                return inputData.actionName;
            }
        }
        Debug.LogError("KeyCode not found");
        return null;
    }
}
