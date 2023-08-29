using UnityEngine;
using System.Collections;

//Not연산에 해당하는 기능을 수행하는 행동트리의 노드 종류이다.
public class Inverter : Node 
{
    //자식노드가 단 하나이다.
    /* Child node to evaluate */
    private Node m_node;

    public Node node 
    {
        get { return m_node; }
    }

    /* The constructor requires the child node that this inverter  decorator
     * wraps*/
    public Inverter(Node node) 
    {
        m_node = node;
    }

    /* Reports a success if the child fails and
     * a failure if the child succeeeds. Running will report
     * as running */
    public override NodeStates Evaluate() 
    {
        switch (m_node.Evaluate()) 
        {
            case NodeStates.FAILURE:
                m_nodeState = NodeStates.SUCCESS;
                return m_nodeState;
            case NodeStates.SUCCESS:
                m_nodeState = NodeStates.FAILURE;
                return m_nodeState;
            case NodeStates.RUNNING:
                m_nodeState = NodeStates.RUNNING;
                return m_nodeState;
        }
        m_nodeState = NodeStates.SUCCESS;
        return m_nodeState;
    }
}
