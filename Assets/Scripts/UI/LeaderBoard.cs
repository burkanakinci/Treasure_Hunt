using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoard : CustomBehaviour
{
    [SerializeField] private TextMeshProUGUI m_MinerCollectedTreasureText;
    [SerializeField] private TextMeshProUGUI m_MinerNameText;
    public override void Initialize()
    {
        m_MinerCollectedTreasureText.text = "";
        m_MinerNameText.text = "";
    }

    public void SetLeaderBoard(int _collectedCount, string _name)
    {
        m_MinerCollectedTreasureText.text = (_collectedCount >= 0) ? (_collectedCount.ToString()) : ("");
        m_MinerNameText.text = _name;
    }
}
