using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuPanel : UIPanel
{
    [SerializeField] private StartGameButton m_StartGameButton;
    [SerializeField] private Animator m_CountDownAnimator;
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        m_CountDownAnimator.enabled = false;

        m_StartGameButton.Initialize(uiManager);
        m_StartGameButton.ButtonClickTweenCallBack = (() =>
        {
            m_CountDownAnimator.enabled = true;
            m_CountDownAnimator.Play(UIAnimationStates.COUNTDOWN_ANIMATION, 0, 0.0f);
        });
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }
}
