using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourBase : StateMachineBehaviour
{
    protected MachineManager manager;
    protected Rigidbody rigidbody;

    public virtual void Initialize(MachineManager manager)
    {
        this.manager = manager;
        rigidbody = manager.GetComponent<Rigidbody>();
    }
}
    
