using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMiner : CustomBehaviour
{
    #region Fields
    protected MinerStateMachine m_MinerStateMachine;
    public Animator m_MinerAnimator;
    protected int m_MinerCollectedTreasure;
    #endregion
    public new virtual void Initialize()
    {
        m_MinerStateMachine=new MinerStateMachine(
            new List<IMinerState>()
        );
        base.Initialize();
    }

    private void FixedUpdate()
    {
        m_MinerStateMachine.PhysicalUpdate();
    }
    private void Update()
    {
        m_MinerStateMachine.LogicalUpdate();
    }
}
