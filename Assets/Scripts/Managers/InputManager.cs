using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputManager : CustomBehaviour
{
    [SerializeField] private FloatingJoystick m_FloatingJoystick;
    [SerializeField] private float m_MinimumSwipeDistanceOnJoystick;


    #region Action
    public event Action<float, float, float> OnSwipeJoystick;
    #endregion

    public override void Initialize()
    {
        GameManager.Instance.OnResetToMainMenu += ResetToMainMenu;
        GameManager.Instance.OnCountdownFinished += OnCountdownFinished;
    }
    #region Joystick
    private float m_Speed;
    private void Update()
    {
        m_Speed = Vector2.SqrMagnitude(new Vector2(m_FloatingJoystick.Horizontal, m_FloatingJoystick.Vertical));
        OnSwipeJoystick?.Invoke(m_Speed, m_FloatingJoystick.Horizontal, m_FloatingJoystick.Vertical);
    }
    #endregion
    #region Events
    private void ResetToMainMenu()
    {
        m_FloatingJoystick.gameObject.SetActive(false);
    }
    private void OnCountdownFinished()
    {
        m_FloatingJoystick.gameObject.SetActive(true);
    }
    #endregion
}
