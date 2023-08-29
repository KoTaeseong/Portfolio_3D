using UnityEngine;
using System.Collections;

[System.Serializable]   // 직렬화가 적용됨

//행동트리에 구성되는 모든 노드들의 부모클래스
//abstract 추상클래스
public abstract class Node 
{
    /* Delegate that returns the state of the node.*/
    public delegate NodeStates NodeReturn();
    //<- 행동트리의 모든 노드는 리턴값을 가진다.
    //<- 델리게이트로 작성되어 있다.

    /* The current state of the node */
    protected NodeStates m_nodeState;   //노드의 상태

    public NodeStates nodeState 
    {
        get { return m_nodeState; }
    }

    /* The constructor for the node */
    public Node() {}

    /* Implementing classes use this method to valuate the desired set of conditions */
    public abstract NodeStates Evaluate();  //추상 메소드 <-- 함수의 형태만 제공하고 있다.

}
