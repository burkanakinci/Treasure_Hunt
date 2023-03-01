using MoreMountains.NiceVibrations;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VibrationsManager : CustomBehaviour
{
    public override void Initialize()
    {
    }

    public void PlayVibration(HapticTypes hapticType)
    {
        MMVibrationManager.Haptic(hapticType);
    }
}
