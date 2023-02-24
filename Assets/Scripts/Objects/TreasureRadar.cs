using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRadar : CustomBehaviour
{
    public RadarType TreasureRadarType;
    public TreasureGenerator RadarTreasureGenerator;

    public bool CanHunt;

    public void Initialize(TreasureGenerator _treasureGenerator)
    {
        RadarTreasureGenerator=_treasureGenerator;
    }
}
