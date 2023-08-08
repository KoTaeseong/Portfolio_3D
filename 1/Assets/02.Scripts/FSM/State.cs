using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,
    Move,
    Attack,
    Skill,
    CC,
    Die,
}


public class State : IStateEnumerator<StateType>
{
    public IStateEnumerator<StateType>.Step current { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool canExcute { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public StateType MoveNext()
    {
        throw new System.NotImplementedException();
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
