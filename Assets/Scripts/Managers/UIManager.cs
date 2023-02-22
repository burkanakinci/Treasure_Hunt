using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : CustomBehaviour
{
    #region Fields
    private UIPanel m_CurrentUIPanel;
    [SerializeField] private List<UIPanel> m_UIPanels;
    #endregion
    public override void Initialize()
    {
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnCountdownFinished += OnCountdownFinished;

        m_UIPanels.ForEach(x =>
        {
            x.Initialize(this);
            x.gameObject.SetActive(true);
        });
    }
    public void HideAllPanels()
    {
        m_UIPanels.ForEach(x =>
        {
            x.HidePanel();
        });
    }
    #region GetterSetter
    public UIPanel GetPanel(UIPanelType _panel)
    {
        return m_UIPanels[(int)_panel];
    }
    public void SetCurrentUIPanel(UIPanel _panel)
    {
        m_CurrentUIPanel = _panel;
    }
    public void SetCurrentUIPanel(UIPanelType _panel)
    {
        m_CurrentUIPanel = m_UIPanels[(int)_panel];
    }
    #endregion

    #region Events
    private void OnResetToMainMenu()
    {
        GetPanel(UIPanelType.MainMenuPanel).ShowPanel();
    }

    private void OnCountdownFinished()
    {
        GetPanel(UIPanelType.HudPanel).ShowPanel();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnCountdownFinished -= OnCountdownFinished;
    }
    #endregion

}
