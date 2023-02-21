using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class UIBaseButton : CustomBehaviour
{
    public TweenCallback ButtonClickTweenCallBack;
    protected UIManager m_UIManager;
    [SerializeField] protected Button m_Button;
    public virtual void Initialize(UIManager _uiManager)
    {
        m_UIManager = _uiManager;
        m_Button.onClick.AddListener(ButtonClick);
    }

    private void OnDestroy()
    {
        m_Button.onClick.RemoveAllListeners();
    }

    protected virtual void ButtonClick()
    {
        ButtonClickTweenCallBack?.Invoke();
    }
}
