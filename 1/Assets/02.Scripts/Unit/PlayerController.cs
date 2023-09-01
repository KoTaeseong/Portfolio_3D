using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MachineManager _machineManager;
    [SerializeField] private MouseMgr _mouseMgr;

    private void Awake()
    {
        _machineManager= GetComponent<MachineManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 우클릭시
        if(Input.GetMouseButton(1))
        {
            _machineManager.target = _mouseMgr.MouseClick();
            _machineManager.ChangeState(StateType.Move);
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            _machineManager.ChangeAttackState(AttackState.WaitUntilTargeting);
        }

        if(_machineManager.attackState == AttackState.WaitUntilTargeting)
        {
            if (Input.anyKeyDown)
            {
                if(Input.GetMouseButton(0))
                {
                    _machineManager.target = _mouseMgr.MouseClick();

                    if(_machineManager.target != null)
                    {
                        if (_machineManager.target.gameObject.layer == 13)
                        {
                            _machineManager.ChangeActionState(ActionState.EnemyAttack);
                        }
                    }
                    
                    //_machineManager.ChangeAttackState(AttackState.TargetTracking);
                }
                else
                {
                    _machineManager.ChangeAttackState(AttackState.None);
                }
            }
        }
    }






    //===========================
    private void FootL() { }
    private void FootR() { }
    private void Land() { }

    private void Hit() { }
}
