using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CustomBehaviour
{
    [SerializeField] private CameraManager m_CameraManager;
    public PlayerMiner PlayerMiner;
    public override void Initialize()
    {
        PlayerMiner.Initialize();
        m_CameraManager.Initialize(PlayerMiner);
    }

    #region Events
    #endregion
}
