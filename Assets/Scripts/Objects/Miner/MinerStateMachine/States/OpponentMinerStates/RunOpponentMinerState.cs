using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOpponentMinerState : IMinerState
{
    public RunOpponentMinerState(OpponentMiner _miner)
    {
        m_Miner = _miner;
    }
    private OpponentMiner m_Miner;

    private Vector2 m_TargetValue;
    public void Enter()
    {
        m_TargetPos = GetTargetPos();
    }
    public void LogicalUpdate()
    {
        m_TargetValue = (m_TargetPos - m_Miner.transform.position).normalized;
        m_Miner.SetMinerAnimatorValues(m_TargetValue.x, m_TargetValue.y);
        m_Miner.SetMinerAnimatorSpeedValue(m_TargetValue.sqrMagnitude);

        if ((Vector2.Distance((new Vector2(m_Miner.transform.position.x, m_Miner.transform.position.y)), m_TargetPos)) <= 1.25f)
        {
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

    private TreasureGenerator m_NearestGenerator;
    private Vector3 m_TargetPos;
    private int m_TempPercentValue, m_TempMaxPercentValue;
    private Vector2 GetTargetPos()
    {
        m_NearestGenerator = GameManager.Instance.Entities.GetNearestGenerator(m_Miner.transform.position);
        m_TempPercentValue = Random.Range(0, 10);
        switch (m_Miner.OpponentCurrentDifficulty)
        {
            case (OpponentDifficulty.Easy):
                m_TempMaxPercentValue = 2;

                break;
            case (OpponentDifficulty.Normal):
                m_TempMaxPercentValue = 4;
                break;
            case (OpponentDifficulty.Hard):
                m_TempMaxPercentValue = 6;
                break;
        }
        if (m_TempPercentValue < m_TempMaxPercentValue)
        {
            return ((Random.insideUnitCircle * 6.0f) + new Vector2(m_NearestGenerator.transform.position.x, m_NearestGenerator.transform.position.y));
        }
        else
        {
            return GameManager.Instance.Entities.GetRandomPointOnGrounds();
        }
    }
}
