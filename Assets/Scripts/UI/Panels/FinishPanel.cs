using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishPanel : UIPanel
{
    [Header("Fail")]
    [SerializeField] private CanvasGroup m_FailCanvas;
    [SerializeField] private NextLevelButton m_FailNextLevelButton;

    [Header("Success")]
    [SerializeField] private CanvasGroup m_SuccessCanvas;
    [SerializeField] private NextLevelButton m_SuccessNextLevelButton;

    
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);

        GameManager.Instance.OnLevelFailed += OnLevelFailed;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += ShowPanel;
        GameManager.Instance.OnLevelCompleted += ShowPanel;
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;

        m_FailNextLevelButton.Initialize(_uiManager);
        m_FailNextLevelButton.ButtonClickTweenCallBack = CachedComponent.NextLevelCallBack(false);
        m_SuccessNextLevelButton.Initialize(_uiManager);
        m_SuccessNextLevelButton.ButtonClickTweenCallBack = CachedComponent.NextLevelCallBack(false);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    #region Events
    private void OnResetToMainMenu()
    {
    }
    private void OnLevelCompleted()
    {
        m_FailCanvas.Close();
        m_SuccessCanvas.Open();
    }
    private void OnLevelFailed()
    {
        m_FailCanvas.Open();
        m_SuccessCanvas.Close();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnLevelCompleted -= ShowPanel;
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
    }
    #endregion

}
