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
        m_Miner.LastTestedDirection = -1;
        m_Miner.CurrentRadarLevel = -1;
    }
    public void LogicalUpdate()
    {
        m_Miner.SetRayTransform(m_TargetPos);
        if (!m_Miner.CheckForward(m_TargetPos))
        {
            Enter();
            return;
        }

        m_Miner.SetOpponentAnimatorByTarget(m_TargetPos, ref m_TargetValue);

        if ((Vector2.Distance(m_Miner.transform.position, m_TargetPos)) <= 0.5f)
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
        if (m_TempPercentValue <= m_TempMaxPercentValue)
        {
            return ((Random.insideUnitCircle * 6.0f) + new Vector2(m_NearestGenerator.transform.position.x, m_NearestGenerator.transform.position.y));
        }
        else
        {
            return GameManager.Instance.Entities.GetRandomPointOnGrounds();
        }

    }
}
