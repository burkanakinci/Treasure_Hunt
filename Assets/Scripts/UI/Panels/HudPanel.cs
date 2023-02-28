using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudPanel : UIPanel
{
    [SerializeField] private LeaderBoard[] m_LeaderBoards;
    [SerializeField] private Timer m_Timer;
    public override void Initialize(UIManager uiManager)
    {
        base.Initialize(uiManager);
        GameManager.Instance.Entities.OnOrderMiner += ShowLeaderBoard;

        m_Timer.Initialize();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        for (int _leaderCount = 0; _leaderCount < 3; _leaderCount++)
        {
            m_LeaderBoards.Initialize();
        }
    }

    private void ShowLeaderBoard(List<BaseMiner> _miners)
    {
        for (int _leaderCount = 0; _leaderCount < 3; _leaderCount++)
        {
            m_LeaderBoards[_leaderCount].SetLeaderBoard(
                ((_miners[_leaderCount] == null ? (0) : (_miners[_leaderCount].MinerCollectedTreasure))),
                ((_miners[_leaderCount] == null ? ("ELIMINATED") : (_miners[_leaderCount].MinerName)))
            );
        }
    }
}
