using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOpponentMinerState : IMinerState
{
    public RunOpponentMinerState(OpponentMiner _miner, Transform _rayTransform)
    {
        m_Miner = _miner;
        m_RayTransform = _rayTransform;
    }
    private OpponentMiner m_Miner;

    private Vector2 m_TargetValue;
    public void Enter()
    {
        m_TargetPos = GetTargetPos();
    }
    public void LogicalUpdate()
    {
        CheckForward();

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
    private RaycastHit2D m_ObstacleHit;
    private Transform m_RayTransform;
#if UNITY_EDITOR
    private Color m_RayColor;
#endif
    private void SetRayTransform()
    {
        m_RayTransform.position = m_Miner.transform.position;
        m_RayTransform.LookAt(m_TargetPos);
    }
    private void CheckForward()
    {
        SetRayTransform();

        if ((Physics2D.Raycast(
                 (m_RayTransform.position + m_RayTransform.up*0.5f),
                 (m_TargetPos - m_Miner.transform.position),
                 (m_Miner.MinerData.ObstacleRayDistance),
                 (m_Miner.MinerData.ObstacleLayers))) ||
            (Physics2D.Raycast(
                 (m_RayTransform.position - m_RayTransform.up*0.5f),
                 (m_TargetPos - m_Miner.transform.position),
                 (m_Miner.MinerData.ObstacleRayDistance),
                 (m_Miner.MinerData.ObstacleLayers)))
             )
        {
            Enter();
#if UNITY_EDITOR
            m_RayColor = Color.white;
#endif
        }
        else
        {
#if UNITY_EDITOR
            m_RayColor = Color.black;
#endif
        }
#if UNITY_EDITOR
        Debug.DrawRay
        (
            (m_RayTransform.position + m_RayTransform.up*0.5f),
            ((m_TargetPos - m_Miner.transform.position).normalized * m_Miner.MinerData.ObstacleRayDistance),
            (m_RayColor)
        );
        Debug.DrawRay
        (
            (m_RayTransform.position - m_RayTransform.up*0.5f),
            ((m_TargetPos - m_Miner.transform.position).normalized * m_Miner.MinerData.ObstacleRayDistance),
            (m_RayColor)
        );
#endif
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
