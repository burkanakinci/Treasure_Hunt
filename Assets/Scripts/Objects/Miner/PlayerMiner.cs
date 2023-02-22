using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMiner : BaseMiner
{
    [SerializeField] private Rigidbody2D m_PlayerMinerRB;
    [SerializeField] private Radar m_PlayerMinerRadar;
    public override void Initialize()
    {
        GameManager.Instance.OnCountdownFinished += OnCountdownFinished;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;
        base.Initialize();

        m_PlayerMinerRadar.Initialize();
    }

    private void MoveMinerByJoystick(float _speed, float _horizontalValue, float _verticalValue)
    {
        m_MinerAnimator.SetFloat("Speed", _speed);
        SetMinerVelocity(new Vector2(_horizontalValue, _verticalValue));
        if ((_horizontalValue == 0.0f) && (_verticalValue == 0.0f))
        {
            return;
        }

        m_MinerAnimator.SetFloat("Horizontal", _horizontalValue);
        m_MinerAnimator.SetFloat("Vertical", _verticalValue);

    }

    private void SetMinerVelocity(Vector2 _targetVelocity)
    {
        m_PlayerMinerRB.velocity = _targetVelocity * m_MinerData.MinerDefaultSpeed;
    }

    #region Events 
    private void OnResetToMainMenu()
    {

    }
    private void OnCountdownFinished()
    {
        GameManager.Instance.InputManager.OnSwipeJoystick += MoveMinerByJoystick;
    }
    private void OnLevelCompleted()
    {
        GameManager.Instance.InputManager.OnSwipeJoystick -= MoveMinerByJoystick;
    }
    private void OnLevelFailed()
    {
        GameManager.Instance.InputManager.OnSwipeJoystick -= MoveMinerByJoystick;
    }
    private void OnDestroy()
    {

        GameManager.Instance.OnCountdownFinished -= OnCountdownFinished;
    }
    #endregion
}
