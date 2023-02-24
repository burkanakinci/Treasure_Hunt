using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entities : CustomBehaviour
{
    [System.Serializable]
    public class StoneValue
    {
        public Sprite StoneSprite;
        public Vector2 StoneColliderOffset;
        public Vector2 StoneColliderSize;
    }
    #region Fields
    [SerializeField] private Transform[] m_ActiveParents;
    [SerializeField] private Sprite[] m_TreeSprites;
    [SerializeField] private StoneValue[] m_StoneValues;
    private List<GameObject> m_GroundOnScene;
    private List<TreasureGenerator> m_TreasureGeneratorOnScene;
    #endregion

    #region ExternalAccess
    #endregion

    public override void Initialize()
    {
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;

        m_GroundOnScene = new List<GameObject>();
        m_TreasureGeneratorOnScene = new List<TreasureGenerator>();
    }

    #region ManageList
    public void ManageTreasureGeneratorList(TreasureGenerator _treasureGenerator, ListOperation _operation)
    {
        if (_operation == ListOperation.Adding)
        {
            m_TreasureGeneratorOnScene.Add(_treasureGenerator);
        }
        else if (_operation == ListOperation.Subtraction)
        {
            if (m_TreasureGeneratorOnScene.Contains(_treasureGenerator))
            {
                m_TreasureGeneratorOnScene.Remove(_treasureGenerator);
            }
        }
    }
    public void ManageGroundList(GameObject _ground, ListOperation _operation)
    {
        if (_operation == ListOperation.Adding)
        {
            m_GroundOnScene.Add(_ground);
        }
        else if (_operation == ListOperation.Subtraction)
        {
            if (m_GroundOnScene.Contains(_ground))
            {
                m_GroundOnScene.Remove(_ground);
            }
        }
    }
    #endregion


    public Transform GetActiveParent(ActiveParents _activeParent)
    {
        return m_ActiveParents[(int)_activeParent];
    }
    public Sprite GetTreeSprite(TreeType _treeType)
    {
        return m_TreeSprites[(int)_treeType];
    }

    public StoneValue GetStoneValue(StoneType _stoneType)
    {
        return m_StoneValues[(int)_stoneType];
    }

    #region Events 
    private void OnResetToMainMenu()
    {
        m_GroundOnScene.Clear();
        m_TreasureGeneratorOnScene.Clear();
    }
    private void OnCountdownFinished()
    {
    }
    private void OnLevelCompleted()
    {
    }
    private void OnLevelFailed()
    {
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
    }
    #endregion

}