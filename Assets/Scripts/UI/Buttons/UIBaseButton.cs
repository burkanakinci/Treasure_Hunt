using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class UIBaseButton : CustomBehaviour
{
    public TweenCallback ButtonClickTweenCallBack;
    public string ButtonClickTweenCallBackID;
    protected UIManager m_UIManager;
    [SerializeField] protected Button m_Button;
    public virtual void Initialize(UIManager _uiManager)
    {
        m_UIManager = _uiManager;
        m_Button.onClick.AddListener(ButtonClick);
        ButtonClickTweenCallBackID = GetInstanceID() + "ButtonClickTweenCallBackID";
    }

    private void OnDestroy()
    {
        m_Button.onClick.RemoveAllListeners();
    }

    protected virtual void ButtonClick()
    {
        DOTween.Kill(ButtonClickTweenCallBackID);
        ButtonClickTweenCallBack?.Invoke();
    }
}
