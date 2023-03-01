using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI m_Level;
    [SerializeField] private float m_CountdownStartDelay = 1.0f;
    [SerializeField] private StartGameButton m_StartGameButton;
    [SerializeField] private Animator m_CountDownAnimator;
    [SerializeField] private Image m_CountDownImage;
    [SerializeField] private Sprite m_TransparencySprite;
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;


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

             });
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
        yield return new WaitForSeconds(m_CountdownStartDelay);
        GameManager.Instance.CountdownFinished();
    }
    #endregion

    #region Events 
    private void OnResetToMainMenu()
    {
        ShowPanel();
        m_CountDownAnimator.enabled = false;
        m_CountDownImage.sprite=m_TransparencySprite;
        m_Level.text = "Level : " + GameManager.Instance.PlayerManager.GetLevelNumber();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
    }
    #endregion
}
