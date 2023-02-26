using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTreasureOpponentState : IMinerState
{
    private OpponentMiner m_Miner;
    public SearchTreasureOpponentState(OpponentMiner _miner)
    {
        m_Miner = _miner;
    }

    public void Enter()
    {
        m_Miner.LastTestedDirection++;
        SetSearchTargetPos();
    }

    private Vector2 m_SearchTargetPos, m_TargetValue;
    public void LogicalUpdate()
    {
        m_Miner.SetRayTransform(m_SearchTargetPos);
        if ((!m_Miner.CheckForward(m_SearchTargetPos)))
        {
            m_Miner.OpponentStateMachine.ChangeState((int)OpponentMinerStates.RunOpponentMinerState);
        }
        if (Vector2.Distance(m_Miner.transform.position, m_SearchTargetPos) <= 0.2f)
        {
            m_Miner.OpponentStateMachine.ChangeState((int)OpponentMinerStates.ReturnOpponentMinerState);
        }

        m_Miner.SetOpponentAnimatorByTarget(m_SearchTargetPos, ref m_TargetValue);
    }
    public void PhysicalUpdate()
    {
        m_Miner.SetMinerVelocity(m_TargetValue);
    }
    public void Exit()
    {

    }
    public void SetSearchTargetPos()
    {
        try
        {

            m_SearchTargetPos = m_Miner.LastEnteredRadarPos + (GameManager.Instance.Entities.GetDirection(m_Miner.LastTestedDirection) * 15.0f);
        }
        catch
        {
            m_Miner.OpponentStateMachine.ChangeState((int)OpponentMinerStates.RunOpponentMinerState);
        }
    }
}
