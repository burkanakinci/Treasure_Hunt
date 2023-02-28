using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinishPanel : UIPanel
{
    [Header("Fail")]
    public CanvasGroup FailCanvas;

    [Header("Success")]
    public CanvasGroup SuccessCanvas;
    [SerializeField] private RectTransform m_NextLevelButton;
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);

        GameManager.Instance.OnLevelFailed += OnLevelFailed;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += ShowPanel;
        GameManager.Instance.OnLevelCompleted += ShowPanel;
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;

        m_NextLevelButtonTweenID = GetInstanceID() + "m_NextLEvelButtonTweenID";

    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        m_NextLevelButton.gameObject.SetActive(true);
        NextLevelButtonTween();
    }
    private string m_NextLevelButtonTweenID;
    private void NextLevelButtonTween()
    {
        DOTween.Kill(m_NextLevelButtonTweenID);

        m_NextLevelButton.DOAnchorPosX(-25.0f, 2.0f)
        .SetEase(Ease.InExpo)
        .SetId(m_NextLevelButtonTweenID);
    }
    public void ContinueButton(bool _isRestart)
    {
        if (!_isRestart)
        {
            GameManager.Instance.PlayerManager.UpdateLevelData((GameManager.Instance.PlayerManager.GetLevelNumber() + 1));
        }

        GameManager.Instance.ResetToMainMenu();
    }


    #region Events
    private void OnResetToMainMenu()
    {
        m_NextLevelButton.gameObject.SetActive(false);
        m_NextLevelButton.anchoredPosition = new Vector3(500.0f, 25.0f, 0f);
        DOTween.Kill(m_NextLevelButtonTweenID);
    }
    private void OnLevelCompleted()
    {
        FailCanvas.Close();
        SuccessCanvas.Open();
    }
    private void OnLevelFailed()
    {
        FailCanvas.Open();
        SuccessCanvas.Close();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnLevelCompleted -= ShowPanel;
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
    }
    #endregion

}
