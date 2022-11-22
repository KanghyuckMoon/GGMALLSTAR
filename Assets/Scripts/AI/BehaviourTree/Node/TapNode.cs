using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TapNode : INode
{
    private Action<KeyCode> tapAction;
    private KeyCode keyCode;

    public TapNode(Action<KeyCode> tapAction, KeyCode keyCode)
    {
        this.tapAction = tapAction;
        this.keyCode = keyCode;
    }

    public bool Run()
    {
        tapAction.Invoke(keyCode);
        return true;
    }
}
