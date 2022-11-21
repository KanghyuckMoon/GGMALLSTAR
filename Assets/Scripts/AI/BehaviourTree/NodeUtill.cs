using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class NodeUtill
{
    public static SelectorNode Selector(params INode[] nodes) => new SelectorNode(nodes);
    public static SequenceNode Sequence(params INode[] nodes) => new SequenceNode(nodes);
    public static ParallelNode Parallel(params INode[] nodes) => new ParallelNode(nodes);
    public static RandomChoiceNode RandomChoice(params INode[] nodes) => new RandomChoiceNode(nodes);
    public static ActionNode Action(Action action) => new ActionNode(action);

    public static IfActionNode IfAction(Func<bool> condition, Action action)
        => new IfActionNode(condition, action);
    public static IfElseActionNode IfElseAction(Func<bool> condition, Action ifAction, Action ifElseAction)
        => new IfElseActionNode(condition, ifAction, ifElseAction);
}