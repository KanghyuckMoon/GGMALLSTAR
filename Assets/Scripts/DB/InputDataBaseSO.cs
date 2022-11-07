using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InputDataBase", menuName = "DataBase/InputDB")]
public class InputDataBaseSO : InputSO
{
    public InputMoveSO MoveInput;

    public override string GetInputData(KeyCode keyCode)
    {
        foreach (var inputData in MoveInput.InputData)
        {
            if (inputData.keyCode == keyCode)
            {
                return inputData.actionName;
            }
        }

        return base.GetInputData(keyCode);
    }
}
