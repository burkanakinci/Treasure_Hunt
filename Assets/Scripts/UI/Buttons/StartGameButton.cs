using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : UIBaseButton
{
    [SerializeField] private CanvasGroup m_StartGameButtonCanvasGroup;

    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
    }
    protected override void ButtonClick()
    {
        base.ButtonClick();
        m_StartGameButtonCanvasGroup.Close();
    }
    #region Events 
    private void OnResetToMainMenu()
    {
        m_StartGameButtonCanvasGroup.Open();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
    }
    #endregion
}


