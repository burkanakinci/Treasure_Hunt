using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entities : CustomBehaviour
{
    #region Fields
    public OpponentMiner OpponentMiner;
    #endregion

    #region ExternalAccess
    #endregion

    public override void Initialize()
    {
        OpponentMiner.Initialize();
    }
}