using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComboNode : INode
{
    public Action<KeyCode> HoldAction { get; protected set; }
    public Action<KeyCode> KeyUpAction { get; protected set; }
    public Func<bool> Condition { get; private set; }
    public ComboSO comboSO;

    public ComboNode(Func<bool> condition, ComboSO comboSO, Action<KeyCode> holdAction, Action<KeyCode> keyUpAction)
    {
        HoldAction = holdAction;
        KeyUpAction = keyUpAction;
        this.comboSO = comboSO;
        Condition = condition;
    }

    private int index = 0;
    private float hold = 0f;
    private float delay = 0f;
    private bool isInput = false;

    public bool Run()
    {
        if (!Condition.Invoke())
		{
            return false;
		}

        if (!isInput)
		{
            isInput = true;
            HoldAction(comboSO.comboInputDatas[index].keyCode);
            hold = comboSO.comboInputDatas[index].holdTime;
            delay = comboSO.comboInputDatas[index].delay;
        }

        hold -= Time.deltaTime;
		if (hold > 0f)
		{
			return true;
        }
        KeyUpAction(comboSO.comboInputDatas[index].keyCode);

        delay -= Time.deltaTime;
        if (delay > 0f)
        {
            return true;
        }

        index += 1;
        if(index == comboSO.comboInputDatas.Length)
		{
            index = 0;
            return false;
		}
        else
		{
            isInput = false;
            return true;
		}
    }
}
