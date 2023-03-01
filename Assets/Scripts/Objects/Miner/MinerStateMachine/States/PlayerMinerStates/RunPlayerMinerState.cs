using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerMinerState : IMinerState
{
    private PlayerMiner m_Miner;
    public RunPlayerMinerState(PlayerMiner _miner)
    {
        m_Miner = _miner;
    }

    public void Enter()
    {
        GameManager.Instance.InputManager.OnSwipeJoystick += m_Miner.MoveMinerByJoystick;
    }
    public void LogicalUpdate()
    {
    }
    public void PhysicalUpdate()
    {

    }
    public void Exit()
    {
        GameManager.Instance.InputManager.OnSwipeJoystick -= m_Miner.MoveMinerByJoystick;
        m_Miner.MoveMinerByJoystick(0.0f, 0.0f, 0.0f);
    }
}
