using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOpponentMinerState : IMinerState
{
    private OpponentMiner m_Miner;
    public RunOpponentMinerState(OpponentMiner _miner)
    {
        m_Miner = _miner;
    }

    private Vector3 m_TargetPos;
    private Vector2 m_TargetValue;
    public void Enter()
    {
        m_TargetPos = GameManager.Instance.Entities.GetRandomPointOnGrounds();
    }
    public void LogicalUpdate()
    {
        m_TargetValue = (m_TargetPos - m_Miner.transform.position).normalized;
        m_Miner.SetMinerAnimatorValues(m_TargetValue.x, m_TargetValue.y);
        m_Miner.SetMinerAnimatorSpeedValue(m_TargetValue.sqrMagnitude);
    }
    public void PhysicalUpdate()
    {
        m_Miner.SetMinerVelocity(m_TargetValue);
    }
    public void Exit()
    {

    }
}
