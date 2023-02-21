using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiner : BaseMiner
{
    public override void Initialize()
    {

        base.Initialize();
    }

    private void MoveMinerByJoystick(float _speed, float _horizontalValue, float _verticalValue)
    {
        m_MinerAnimator.SetFloat("Speed", _speed);
        m_MinerAnimator.SetFloat("Horizontal", _horizontalValue);
        m_MinerAnimator.SetFloat("Vertical", _verticalValue);
    }

    #region Events 
    private void OnResetToMainMenu()
    {

    }
    private void OnCountdownFinished()
    {
        GameManager.Instance.InputManager.OnSwipeJoystick += MoveMinerByJoystick;
    }
    private void OnDestroy()
    {
        GameManager.Instance.InputManager.OnSwipeJoystick -= MoveMinerByJoystick;
    }
    #endregion
}
