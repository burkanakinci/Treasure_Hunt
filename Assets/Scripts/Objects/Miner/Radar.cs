using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : CustomBehaviour
{
    public int CurrentRadarTypeValue;
    [SerializeField] private SpriteRenderer m_SensorRenderer;
    [SerializeField] private List<Sprite> m_RadarSprites;

    public override void Initialize()
    {
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
    }

    public void SetRadar(RadarType _radarType, TriggerType _triggerType)
    {
        if (_triggerType == TriggerType.Enter)
        {
            if ((CurrentRadarTypeValue) < ((int)_radarType))
            {
                CurrentRadarTypeValue = ((int)_radarType);
                SetRadarSprite();

                switch (_radarType)
                {
                    case RadarType.RadarLevel1:
                        GameManager.Instance.VibrationsManager.PlayVibration(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
                        break;
                    case RadarType.RadarLevel2:
                        GameManager.Instance.VibrationsManager.PlayVibration(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                        break;
                    case RadarType.RadarLevel3:
                        GameManager.Instance.VibrationsManager.PlayVibration(MoreMountains.NiceVibrations.HapticTypes.HeavyImpact);
                        break;
                }
            }
        }
        else if (_triggerType == TriggerType.Exit)
        {
            if ((CurrentRadarTypeValue) >= ((int)_radarType))
            {
                CurrentRadarTypeValue = ((int)_radarType - 1);
                SetRadarSprite();
            }
        }
    }

    private void SetRadarSprite()
    {
        m_SensorRenderer.sprite = (CurrentRadarTypeValue < 0) ? (null) : (m_RadarSprites[CurrentRadarTypeValue]);
    }

    #region Events 
    private void OnResetToMainMenu()
    {
        CurrentRadarTypeValue = -1;
        SetRadarSprite();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
    }
    #endregion
}
