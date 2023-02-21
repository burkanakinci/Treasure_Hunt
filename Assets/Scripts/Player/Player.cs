using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CustomBehaviour
{
    public PlayerMiner PlayerMiner;
    public override void Initialize()
    {
        PlayerMiner.Initialize();
    }

    #region Events
    #endregion
}
