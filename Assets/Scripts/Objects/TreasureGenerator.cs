using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureGenerator : PooledObject
{
    public float GenerateRefreshRate;
    public float GenerateDelay;
    public Transform RefreshPosition;
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        base.OnObjectDeactive();
    }


}
