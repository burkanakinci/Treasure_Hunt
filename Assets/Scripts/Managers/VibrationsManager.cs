using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class VibrationsManager : CustomBehaviour
{
    public override void Initialize()
    {

    }
    public void PlayVibration(HapticTypeOnPlayer _hapticType)
    {
        switch (_hapticType)
        {
            case (HapticTypeOnPlayer.Light):

                Taptic.Light();

                break;
            case (HapticTypeOnPlayer.Medium):

                Taptic.Medium();

                break;
            case (HapticTypeOnPlayer.Heavy):

                Taptic.Heavy();

                break;
        }
    }
}
