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
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
        m_CountDownAnimator.enabled = false;

        m_StartGameButton.Initialize(_uiManager);
        SetStartGameButton();
    }


    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    #region StartGameButton
    private void SetStartGameButton()
    {
        m_StartGameButton.ButtonClickTweenCallBack = new TweenCallback(() =>
   {
       StartCountDownFinishedCoroutine();
       DOVirtual.DelayedCall(1.0f, () =>
             {
                 m_CountDownAnimator.enabled = true;
                 m_CountDownAnimator.Play(AnimationStates.COUNTDOWN_ANIMATION, 0, 0.0f);
             }).SetId(m_StartGameButton.ButtonClickTweenCallBackID);
   });
    }

    private Coroutine m_CountDownFinishedCoroutine;
    private void StartCountDownFinishedCoroutine()
    {
        if (m_CountDownFinishedCoroutine != null)
        {
            StopCoroutine(m_CountDownFinishedCoroutine);
        }
        m_CountDownFinishedCoroutine = StartCoroutine(CountDownFinishedCoroutine());
    }
    private IEnumerator CountDownFinishedCoroutine()
    {
        yield return new WaitUntil(() => m_CountDownAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        GameManager.Instance.CountdownFinished();
    }
    #endregion
}
