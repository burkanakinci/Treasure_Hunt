using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : CustomBehaviour
{
    #region Fields
    [SerializeField]private  Player m_Player;
    #endregion
    public override void Initialize()
    {

        m_Player.Initialize();

    }
    public void UpdateLevelData(int _levelNumber)
    {
        GameManager.Instance.JsonConverter.PlayerData.LevelNumber = _levelNumber;
        GameManager.Instance.JsonConverter.SavePlayerData();
    }
    public int GetLevelNumber()
    {
        return GameManager.Instance.JsonConverter.PlayerData.LevelNumber;
    }
}
