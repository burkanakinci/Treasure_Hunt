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
        SetSearchTargetPos();
    }

    private Vector2 m_SearcTargetPos, m_TargetValue;
    public void LogicalUpdate()
    {
        m_Miner.SetRayTransform(m_SearcTargetPos);
        if (!m_Miner.CheckForward(m_SearcTargetPos))
        {
            Enter();
            return;
        }

        m_Miner.MoveOpponentToTarget(m_SearcTargetPos, ref m_TargetValue);

        if ((Vector2.Distance((new Vector2(m_Miner.transform.position.x, m_Miner.transform.position.y)), m_SearcTargetPos)) <= 1.25f)
        {
            m_Miner.TempEnteredRadarPos = m_Miner.transform.position;
            Enter();
        }
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
        m_SearcTargetPos = m_Miner.TempEnteredRadarPos +
        (Utilities.RandomPosInsideCircle(1.0f) * ((m_Miner.LastTriggedTreasureRadar.TreasureRadarType == RadarType.RadarLevel2) ? (3.0f) : (6.0f)));
    }
}
