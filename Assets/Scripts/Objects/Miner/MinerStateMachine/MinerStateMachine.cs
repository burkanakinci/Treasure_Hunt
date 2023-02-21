using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerStateMachine : BaseStateMachine
{
    public MinerStateMachine(List<IMinerState> _states)
    {
        m_States = _states;
    }
}
