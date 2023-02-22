using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMiner : CustomBehaviour
{
    [SerializeField] protected MinerData m_MinerData;
    #region Fields
    public Animator m_MinerAnimator;
    protected int m_MinerCollectedTreasure;
    #endregion

    public void SetMinerAnimatorValues(float _horizontalValue, float _verticalValue)
    {
        if ((_horizontalValue == 0.0f) && (_verticalValue == 0.0f))
        {
            return;
        }
        m_MinerAnimator.SetFloat("Horizontal", _horizontalValue);
        m_MinerAnimator.SetFloat("Vertical", _verticalValue);
    }

    public void SetMinerAnimatorSpeedValue(float _speed)
    {
        m_MinerAnimator.SetFloat("Speed", _speed);
    }
}
