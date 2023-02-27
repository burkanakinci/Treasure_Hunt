using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FreezePlayerMinerState : IMinerState
{
    private PlayerMiner m_Miner;
    public FreezePlayerMinerState(PlayerMiner _miner)
    {
        m_Miner = _miner;
        m_DissolveMinerTweenID = _miner.GetInstanceID() + "m_DissolveMinerTweenID";
    }

    public void Enter()
    {
        GameManager.Instance.InputManager.OnSwipeJoystick -= m_Miner.MoveMinerByJoystick;

        StartDissolveMinerTweenCallback();
    }
    private string m_DissolveMinerTweenID;
    private void StartDissolveMinerTweenCallback()
    {
        DOTween.Kill(m_DissolveMinerTweenID);
        DOVirtual.DelayedCall(m_Miner.MinerData.FreezeDuration,
        () =>
        {
            m_Miner.DissolveMiner();
        }
        )
        .SetId(m_DissolveMinerTweenID);
    }
    public void LogicalUpdate()
    {
    }
    public void PhysicalUpdate()
    {
        m_Miner.SetMinerVelocity(Vector2.zero);
    }
    public void Exit()
    {
        DOTween.Kill(m_DissolveMinerTweenID);
        GameManager.Instance.InputManager.OnSwipeJoystick += m_Miner.MoveMinerByJoystick;
    }
}
