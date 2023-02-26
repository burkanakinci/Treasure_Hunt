using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnOpponentMinerState : IMinerState
{
    private OpponentMiner m_Miner;
    public ReturnOpponentMinerState(OpponentMiner _miner)
    {
        m_Miner = _miner;
    }

    public void Enter()
    {
        m_ReturnPos = m_Miner.LastEnteredRadarPos;
    }

    private Vector2 m_ReturnPos, m_TargetValue;
    public void LogicalUpdate()
    {
        m_Miner.SetOpponentAnimatorByTarget(m_ReturnPos, ref m_TargetValue);

        if (!m_Miner.CheckForward(m_ReturnPos))
        {
            m_Miner.OpponentStateMachine.ChangeState((int)OpponentMinerStates.RunOpponentMinerState);
        }

        if (Vector2.Distance(m_Miner.transform.position, m_ReturnPos) <= 0.15f)
        {
            m_Miner.OpponentStateMachine.ChangeState((int)OpponentMinerStates.SearchTreasureOpponentMinerState);
        }
    }
    public void PhysicalUpdate()
    {
        m_Miner.SetMinerVelocity(m_TargetValue);
    }
    public void Exit()
    {

    }
}
