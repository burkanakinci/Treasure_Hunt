using UnityEngine;
using System.Collections.Generic;

public class BaseStateMachine
{
    protected IMinerState m_CurrentState;
    protected List<IMinerState> m_States;

    public BaseStateMachine(List<IMinerState> _states)
    {
        m_States = _states;
    }

    public void LogicalUpdate()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.LogicalUpdate();
        }
    }
    public void PhysicalUpdate()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.PhysicalUpdate();
        }
    }
    public void ChangeState(int _state, bool _changeForce = false)
    {
        if (m_States[_state] != m_CurrentState || _changeForce)
        {
            if (m_CurrentState != null)
            {
                m_CurrentState.Exit();
            }

            m_CurrentState = m_States[_state];
            m_CurrentState.Enter();
        }
    }

    public bool EqualCurrentState(int _state)
    {
        return (m_CurrentState == m_States[_state]);
    }
}
