using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class UIBaseButton : CustomBehaviour<UIManager>
{
    public TweenCallback ButtonClickTweenCallBack;
    [SerializeField] protected Button m_Button;
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
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
