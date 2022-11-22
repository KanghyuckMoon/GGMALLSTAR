using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoldNode : INode
{
    private float originTime = 0f;
    private float currentTime = 0f;
    private Action<KeyCode> holdAction;
    private Action<KeyCode> upAction;
    private KeyCode keyCode;

    public HoldNode(float time, Action<KeyCode> holdAction, Action<KeyCode> upAction, KeyCode keyCode)
    {
        this.originTime = time;
        this.holdAction = holdAction;
        this.upAction = upAction;
        this.keyCode = keyCode;
    }

    public bool Run()
    {
        if (currentTime > 0f)
        {
            holdAction.Invoke(keyCode);
            currentTime -= Time.deltaTime;
            return false;
        }
        else
        {
            upAction.Invoke(keyCode);
            currentTime = originTime;
            return true;
        }
    }
}
