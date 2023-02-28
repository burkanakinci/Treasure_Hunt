using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Timer : CustomBehaviour
{
    [SerializeField] private Image m_TimerBGRenderer;
    [SerializeField] private Sprite[] m_TimerBG;

    [SerializeField] private TextMeshProUGUI m_TimerText;
    [SerializeField] private Transform m_EliminatedArea;
    [SerializeField] private TextMeshProUGUI m_EliminatedNoticeText;
    private int m_CurrentTime;

    public override void Initialize()
    {
        m_TimerTextTweenID = GetInstanceID() + "m_TimerTextTweenID";
        m_TimerTextStartTweenID = GetInstanceID() + "m_TimerTextStartTweenID";

        m_TimerScaleStartValue = m_TimerText.transform.localScale;
        m_PunchScaleValue = (m_TimerScaleStartValue + (Vector3.one * 0.15f));

        m_EliminatedNoticeText.text = "";
        m_ElimintedAreaScaleTweenID = GetInstanceID() + "m_ElimintedAreaScaleTweenID";
        m_EliminatedSreaScaleDownTweenID = GetInstanceID() + "m_EliminatedSreaScaleDownTweenID";

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnCountdownFinished += OnCountdownFinished;
    }

    private Coroutine m_TimerCoroutine;
    private void StartTimerCountDown()
    {
        if (m_TimerCoroutine != null)
        {
            StopCoroutine(m_TimerCoroutine);
        }
        m_TimerCoroutine = StartCoroutine(TimerCountDownCoroutine());
    }

    private IEnumerator TimerCountDownCoroutine()
    {
        m_TimerText.text = m_CurrentTime.ToString();
        if (m_CurrentTime < 4)
        {
            TimerTextTween();
            m_TimerBGRenderer.sprite = m_TimerBG[(int)TimerBackgrounds.Red];
        }
        yield return new WaitForSecondsRealtime(1.0f);
        TimeDecrease();
        if (m_CurrentTime >= 0)
        {
            StartTimerCountDown();
        }
        else
        {
            m_EliminatedNoticeText.text = GameManager.Instance.Entities.GetLastMiner().MinerName;
            EliminatedAreaScale();
            GameManager.Instance.Entities.GetLastMiner().EliminatedMiner();
            ResetTimer();
            StartTimerCountDown();
            if (GameManager.Instance.Entities.RemainingMiner == 1)
            {
                GameManager.Instance.LevelCompleted();
            }
        }
    }

    #region TimerArea
    private void TimeDecrease()
    {
        m_CurrentTime--;
    }

    private string m_TimerTextTweenID, m_TimerTextStartTweenID;
    private Vector3 m_TimerScaleStartValue, m_PunchScaleValue;
    private void TimerTextTween()
    {
        DOTween.Kill(m_TimerTextTweenID);

        m_TimerText.transform.DOScale(
            (m_PunchScaleValue),
            (0.5f)
        ).
        OnComplete(() =>
        {
            TimerTextStartTween();
        }
        ).
        SetId(m_TimerTextTweenID);
    }
    private void TimerTextStartTween()
    {
        DOTween.Kill(m_TimerTextStartTweenID);

        m_TimerText.transform.DOScale(
            (m_TimerScaleStartValue),
            (0.25f)
        ).
        SetId(m_TimerTextStartTweenID);
    }
    private void ResetTimer()
    {
        m_TimerText.transform.localScale = m_TimerScaleStartValue;
        m_TimerBGRenderer.sprite = m_TimerBG[(int)TimerBackgrounds.Grey];
        m_CurrentTime = 10;
        m_TimerText.text = m_CurrentTime.ToString();
        if (m_TimerCoroutine != null)
        {
            StopCoroutine(m_TimerCoroutine);
        }
    }
    #endregion

    #region EliminatedNoticeArea
    private string m_ElimintedAreaScaleTweenID;
    private string m_EliminatedSreaScaleDownTweenID;

    private void EliminatedAreaScale()
    {
        DOTween.Kill(m_ElimintedAreaScaleTweenID);
        m_EliminatedArea.transform.localScale = Vector3.zero;

        m_EliminatedArea.transform.DOScale(
            (Vector3.one),
            (0.5f)
        ).
        OnComplete(() =>
        {
            DOTween.Kill(m_EliminatedSreaScaleDownTweenID);
            DOVirtual.DelayedCall((2.0f),
            () =>
            {
                m_EliminatedArea.transform.DOScale(
                    (Vector3.zero),
                    (0.25f));
            }).SetId(m_EliminatedSreaScaleDownTweenID);
        }).
        SetId(m_ElimintedAreaScaleTweenID);
    }
    #endregion
    #region Events 
    private void OnResetToMainMenu()
    {
        ResetTimer();
        m_EliminatedArea.localScale = Vector3.zero;
    }
    private void OnCountdownFinished()
    {
        StartTimerCountDown();
    }
    private void OnLevelCompleted()
    {
    }
    private void OnLevelFailed()
    {
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnCountdownFinished -= OnCountdownFinished;
    }
    #endregion
}
