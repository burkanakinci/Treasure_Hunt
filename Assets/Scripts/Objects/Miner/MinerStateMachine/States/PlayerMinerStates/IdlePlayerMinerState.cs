using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerMinerState : IMinerState
{
    private PlayerMiner m_Miner;
    public IdlePlayerMinerState(PlayerMiner _miner)
    {
        m_Miner = _miner;
    }

    public void Enter()
    {
    }
    public void LogicalUpdate()
    {
    }
    public void PhysicalUpdate()
    {

    }
    public void Exit()
    {

    }
}
